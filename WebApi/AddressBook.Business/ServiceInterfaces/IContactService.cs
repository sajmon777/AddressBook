

using AddressBook.Business.Model;
using System.Threading.Tasks;

namespace Humans.Business.ServiceInterfaces
{
	public interface IContactService
	{
		Contact GetContact(int id);
		Contact InsertContact(Contact contact);
		void UpdateContact(Contact contact);
		void DeleteContact(int id);
	}
}
