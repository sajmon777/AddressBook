

using AddressBook.Business.Model;
using Humans.Business.RepositoryInterfaces;
using NHibernate;
using System.Linq;
using System.Threading.Tasks;
using AddressBook.Business.Infrastructure;

namespace Humans.Business.Repository
{
	public class ContactRepository : IContactRepository
	{
		private readonly ISession _session;

		public ContactRepository(ISession session)
		{
			_session = session;
		}

		public Contact GetContact(int id)
		{
			return _session.Get<Contact>(id);
		}

		public Contact GetContactByTelephoneNumber(string telephoneNumber)
		{
			return _session.Query<Contact>().Where(x => x.TelephoneNumber == telephoneNumber).FirstOrDefault();
		}

		public Contact InsertContact(Contact contact)
		{
			contact.Id = (int)_session.Save(contact);
			return contact;
		}

		public void DeleteContact(int id)
		{
			_session.Delete<Contact>(id);
		}

		public void UpdateContact(Contact contact)
		{
			_session.Merge(contact);
		}
	}
}
