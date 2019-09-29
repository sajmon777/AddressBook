using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AddressBook.API.Infrastructure;
using AddressBook.Business.Infrastructure;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AddressBook.API
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public IContainer ApplicationContainer { get; private set; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
			
			var containerBuilder = new ContainerBuilder();
			containerBuilder.RegisterModule(new AutofacModuleBL());
			containerBuilder.AddNHibernate(Configuration.GetSection("ConnectionString").Value);
			containerBuilder.Populate(services);
			ApplicationContainer = containerBuilder.Build();
			return new AutofacServiceProvider(ApplicationContainer);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseMiddleware<ExceptionMiddleware>();
			app.UseMvc();
		}
	}
}
