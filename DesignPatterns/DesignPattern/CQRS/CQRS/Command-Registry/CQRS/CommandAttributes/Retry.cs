using System;
namespace DI_MessageDispatcher.CQRS.CommandAttributes
{
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
	public class Retry : Attribute
	{
		public Retry()
		{
		}
	}
}
 