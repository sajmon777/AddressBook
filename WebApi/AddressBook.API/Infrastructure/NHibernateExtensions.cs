
using Autofac;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;
using System;
using System.Linq;

namespace AddressBook.API.Infrastructure
{
	public static class NHibernateExtensions
	{
		public static ContainerBuilder AddNHibernate(this ContainerBuilder builder, string connectionString)
		{
			var types =  AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.Contains("AddressBook.Business")).FirstOrDefault().ExportedTypes.Where(x=>x.FullName.Contains("AddressBook.Business.Maping"));
			var mapper = new ModelMapper();
			mapper.AddMappings(types);
			HbmMapping domainMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

			var configuration = new Configuration();
			configuration.DataBaseIntegration(db =>
			{
				db.Dialect<MsSql2012Dialect>();
				db.ConnectionString = connectionString;
				db.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
				db.SchemaAction = SchemaAutoAction.Validate;
				db.LogFormattedSql = true;
				db.LogSqlInConsole = true;
				db.BatchSize = 100;
				db.SchemaAction = SchemaAutoAction.Update;
			});

			configuration.AddMapping(domainMapping);
			var sessionFactory = configuration.BuildSessionFactory();
			builder.Register(x => sessionFactory).SingleInstance().As<ISessionFactory>();
			builder.Register(x => sessionFactory.OpenSession()).InstancePerLifetimeScope(); 
			return builder;
		}
	}
}
