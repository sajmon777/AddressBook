using Autofac;
using Autofac.Extras.DynamicProxy;
using Humans.Business.Repository;
using Humans.Business.RepositoryInterfaces;
using Humans.Business.Service;
using Humans.Business.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AddressBook.Business.Infrastructure
{
	public class AutofacModuleBL: Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<TransactionInterceptor>().InstancePerLifetimeScope();

			builder.RegisterType<ContactRepository>().As<IContactRepository>();

			builder.RegisterType<ContactService>().EnableClassInterceptors().InterceptedBy(typeof(TransactionInterceptor)).As<IContactService>();
		}
	}
}
