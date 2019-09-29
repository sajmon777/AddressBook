using AddressBook.Business.Infrastructure;
using AddressBook.Business.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Humans.Business.RepositoryInterfaces
{
	public interface IContactRepository
	{
		List<Contact> GetContactList();
		GridResult<Contact> GetContactGrid(GridInfo<ContactFilter> gridInfo); 
		Contact GetContact(int id);
		Contact GetContactByTelephoneNumber(string telephoneNumber);
		Contact InsertContact(Contact contact);
		void UpdateContact(Contact contact);
		void DeleteContact(int id);
	}
}
