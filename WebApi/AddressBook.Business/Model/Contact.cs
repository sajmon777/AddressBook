using AddressBook.Business.Infrastructure;

namespace AddressBook.Business.Model
{
	public class Contact :Entity
	{
		public virtual string FirstName { get; set; }
		public virtual string LastName { get; set; }
		public virtual string Address { get; set; }
		public virtual string TelephoneNumber { get; set; }
	}
}
