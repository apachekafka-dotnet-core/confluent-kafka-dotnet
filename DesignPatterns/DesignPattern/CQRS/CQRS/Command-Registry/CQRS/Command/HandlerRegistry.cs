using System;
using System.Collections.Generic;
using DI_MessageDispatcher.CQRS.Base;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using DI_MessageDispatcher.CQRS.CommandAttributes;
using DI_MessageDispatcher.CQRS.Decorators;
using System.Reflection;

namespace DI_MessageDispatcher.CQRS.Command
{
	public static class HandlerRegistry
	{
		public static void AddHandlers(this IServiceCollection services)
		{
			//Step 1: Scan all Commands and Query

			List<Type> types = typeof(ICommand).Assembly.GetTypes()
							   .Where(a => a.GetInterfaces().Any(y => IsHandlerInterface(y)))
							   .Where(x => x.Name.EndsWith("Handler"))
							   .ToList();

			foreach (var type in types)
			{
				AddHandler(services, type);
			}
			//services.AddTransient
			}

		//Step 3: Wrap the decorator based on attribute type
		private static void AddHandler(IServiceCollection services, Type type)
		{
			object[] attributes = type.GetCustomAttributes(false);
			List<Type> pipeline = attributes
								.Select(x => ToDecorator(x))
								.Concat(new[] { type })
								.Reverse()
								.ToList();

			Type interfaceType = type.GetInterfaces().Single(y => IsHandlerInterface(y));
			Func<IServiceProvider, object> factory = BuildPipeline(pipeline, interfaceType);

			services.AddTransient(interfaceType,factory);

		}

		//Step 4: Get the specific decorator based on attribute type
		private static Type ToDecorator(object attribute)
		{
			Type type = attribute.GetType();
			if (type == typeof(Retry))
				return typeof(CommandRetryDecorator<,>);

			if (type == typeof(AuditLog))
				return typeof(LogDecorator<,>);

			throw new ArgumentException(attribute.ToString());
		}

		//Step 5: Build pipeline
		private static Func<IServiceProvider, object> BuildPipeline(List<Type> pipeline, Type interfaceType)
		{
			// Getting constructure of each decorator
			List<ConstructorInfo> constructors = pipeline.Select(x => {

				Type type = x.IsGenericType ? x.MakeGenericType(interfaceType.GenericTypeArguments) : x;
				return type.GetConstructors().Single();
			})
			.ToList();

			Func<IServiceProvider, object> func = provider =>
			{
				object current = null;
				foreach (ConstructorInfo constructor in constructors)
				{
					//Run constructure and return instance
					List<ParameterInfo> parameterInfos = constructor.GetParameters().ToList();
					object[] parameters = GetParameters(parameterInfos, current, provider);
					current = constructor.Invoke(parameters);
				}
				return current;
			};

			return func;
		}

		//Step 6: resolve constructor parameters
		private static object[] GetParameters(List<ParameterInfo> parameterInfos, object current, IServiceProvider provider)
		{
			var result = new object[parameterInfos.Count];

			for(int i=0;i<parameterInfos.Count;i++)
			{
				result[i] = GetParameter(parameterInfos[i], current, provider);
			}
			return result;
		}

		//Step 7: resolve parameters from DI
		private static object GetParameter(ParameterInfo parameterInfo, object current, IServiceProvider provider)
		{
			Type parameter = parameterInfo.ParameterType;
			if (IsHandlerInterface(parameter))
				return current;

			object service = provider.GetService(parameter);
			if (service != null)
				return service;

			throw new ArgumentException($"Type {parameter} not found");
		}



		//Step 2: Make sure types must implement ICommandHandler
		private static bool IsHandlerInterface(Type type)
		{
			if (!type.IsGenericType)
				return false;

			Type typeDefinition = type.GetGenericTypeDefinition();

			return typeDefinition == typeof(ICommandHandler<,>);
		}



	}
}
