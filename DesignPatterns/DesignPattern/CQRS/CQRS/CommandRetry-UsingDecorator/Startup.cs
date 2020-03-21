using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DI_MessageDispatcher.CQRS.Base;
using DI_MessageDispatcher.CQRS.Command;
using DI_MessageDispatcher.CQRS.Decorators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;


namespace DI_MessageDispatcher
{
    public class Startup
    {
        private ILoggerFactory _loggerFactory;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
			
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CQRS", Version = "v1" });
            });

            

            services.AddTransient<ICommandResult, CommandResult>();

            //services.AddTransient<ICommandHandler<CreateUserCommand, CommandResult>,CreateUserCommandHandler>();
            services.AddTransient<ICommandHandler<CreateUserCommand, CommandResult>> ((provider =>
				new LogDecorator<CreateUserCommand, CommandResult>(
					new CommandRetryDecorator<CreateUserCommand, CommandResult>(
						new CreateUserCommandHandler())
				)));


            services.AddTransient<IMessageDispatcher, MessageDispatcher>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
