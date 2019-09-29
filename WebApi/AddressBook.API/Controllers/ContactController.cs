using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AddressBook.Business.Infrastructure;
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
		private readonly IContactService _contactService;

		public ContactController(IContactService contactS)
		{
			_contactService = contactS;
		}

		[HttpGet]
		public IActionResult GetContactList()
		{
			return new JsonResult(_contactService.GetContactList());
		}

		[HttpPost]
		[ActionName("grid")]
		[Route("grid")]
		public IActionResult GetContactGrid([FromBody]GridInfo<ContactFilter> gridInfo)
		{
			return new JsonResult(_contactService.GetContactGrid(gridInfo));
		}

		[HttpGet("{id}")]
		public IActionResult GetPerson(int id)
		{
			return new JsonResult(_contactService.GetContact(id));
		}

		[HttpPost]
		public IActionResult Post([FromBody] Contact contact)
		{
			return new JsonResult(_contactService.InsertContact(contact));
		}

		[HttpPut]
		public void Put([FromBody] Contact contact)
		{
			_contactService.UpdateContact(contact);
		}

		[HttpDelete("{id}")]
		public void DeletePerson(int id)
		{
			_contactService.DeleteContact(id);
		}
	}
}