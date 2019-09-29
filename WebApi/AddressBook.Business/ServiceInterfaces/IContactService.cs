

using AddressBook.Business.Infrastructure;
using AddressBook.Business.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Humans.Business.ServiceInterfaces
{
	public interface IContactService
	{
		Contact GetContact(int id);
		List<Contact> GetContactList();
		GridResult<Contact> GetContactGrid(GridInfo<ContactFilter> gridInfo); 
		Contact InsertContact(Contact contact);
		void UpdateContact(Contact contact);
		void DeleteContact(int id);
	}
}
