using AddressBook.Business.Model;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;


namespace AddressBook.Business.Maping
{
	public class ContactMap : ClassMapping<Contact>
	{
		public ContactMap()
		{
			Table("Contact");

			Id(x => x.Id, mapper =>
			{
				mapper.Generator(Generators.Native);
			});

			Property(x => x.FirstName, mapper =>
			{
				mapper.Length(50);
				mapper.Type(NHibernateUtil.StringClob);
				mapper.NotNullable(true);
			});

			Property(x => x.LastName, mapper =>
			{
				mapper.Length(50);
				mapper.Type(NHibernateUtil.StringClob);
				mapper.NotNullable(true);
			});

			Property(x => x.Address, mapper =>
			{
				mapper.Length(50);
				mapper.Type(NHibernateUtil.StringClob);
				mapper.NotNullable(true);
			});

			Property(x => x.TelephoneNumber, mapper =>
			{
				mapper.Length(50);
				mapper.Type(NHibernateUtil.StringClob);
				mapper.NotNullable(true);
			});
		}
	}
}
