﻿using AddressBook.Business.Infrastructure;
using AddressBook.Business.Model;
using Humans.Business.RepositoryInterfaces;
using Humans.Business.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Humans.Business.Service
{
	public class ContactService : IContactService
	{
		private readonly IContactRepository _contactRepository;

		public ContactService(IContactRepository contactR)
		{
			_contactRepository = contactR;
		}
		public virtual Contact GetContact(int id)
		{
			return _contactRepository.GetContact(id);
		}

		public List<Contact> GetContactList()
		{
			return _contactRepository.GetContactList();
		}

		public GridResult<Contact> GetContactGrid(GridInfo<ContactFilter> gridInfo)
		{
			return _contactRepository.GetContactGrid(gridInfo);
		}

		public virtual Contact InsertContact(Contact contact)
		{
			ContactValidation(contact);
			return _contactRepository.InsertContact(contact);
		}

		public virtual void UpdateContact(Contact contact)
		{
			ContactValidation(contact);
			_contactRepository.UpdateContact(contact);
		}

		public virtual void DeleteContact(int id)
		{
			_contactRepository.DeleteContact(id);
		}

		private void ContactValidation(Contact contact)
		{
			var errs = new List<RuleViolation>();
			if (string.IsNullOrWhiteSpace(contact.FirstName))
				errs.Add(new RuleViolation(() => contact.FirstName, "Required"));
			if (string.IsNullOrWhiteSpace(contact.LastName))
				errs.Add(new RuleViolation(() => contact.LastName, "Required"));
			if (string.IsNullOrWhiteSpace(contact.Address))
				errs.Add(new RuleViolation(() => contact.Address, "Required"));
			if (string.IsNullOrWhiteSpace(contact.TelephoneNumber))
				errs.Add(new RuleViolation(() => contact.TelephoneNumber, "Required"));
			else if (!IsValidPhone(contact.TelephoneNumber)) {
				errs.Add(new RuleViolation(() => contact.TelephoneNumber, "Format"));
			}
			else{
				var existingContact = _contactRepository.GetContactByTelephoneNumber(contact.TelephoneNumber);
				if (existingContact != null && (existingContact.Id != contact.Id))
					errs.Add(new RuleViolation(() => contact.TelephoneNumber, "Exists"));
			}
			if (errs.Count > 0)
				throw new RuleViolationException(errs);
		}

		public bool IsValidPhone(string telephoneNumber)
		{
			if (string.IsNullOrEmpty(telephoneNumber))
				return false;
			var r = new Regex(@"^(([0-9]{3})[ \-\/]?([0-9]{3})[ \-\/]?([0-9]{3}))|([0-9]{9})|([\+]?([0-9]{3})[ \-\/]?([0-9]{2})[ \-\/]?([0-9]{3})[ \-\/]?([0-9]{3}))$");
			return r.IsMatch(telephoneNumber);
		}
	}
}
