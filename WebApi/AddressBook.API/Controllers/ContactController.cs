using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AddressBook.Business.Model;
using Humans.Business.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ContactController : ControllerBase
	{
		private readonly IContactService _contactS;

		public ContactController(IContactService contactS)
		{
			_contactS = contactS;
		}

		[HttpGet("{id}")]
		public IActionResult GetPerson(int id)
		{
			var contact = _contactS.GetContact(id);
			if (contact == null)
			{
				return NotFound();
			}
			return new JsonResult(contact);

		}

		[HttpPost]
		public void Post([FromBody] Contact contact)
		{
			_contactS.InsertContact(contact);
		}

		[HttpPut]
		public void Put([FromBody] Contact contact)
		{
			_contactS.UpdateContact(contact);
		}


		[HttpDelete("{id}")]
		public void DeletePerson(int id)
		{
			_contactS.DeleteContact(id);
		}
	}
}