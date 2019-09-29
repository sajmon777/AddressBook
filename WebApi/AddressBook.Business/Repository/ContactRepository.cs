

using AddressBook.Business.Model;
using Humans.Business.RepositoryInterfaces;
using NHibernate;
using System.Linq;
using AddressBook.Business.Infrastructure;
using System.Collections.Generic;
using NHibernate.Criterion;
using System;

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

		public List<Contact> GetContactList()
		{
			return _session.Query<Contact>().ToList();
		}

		public GridResult<Contact> GetContactGrid(GridInfo<ContactFilter> gridInfo)
		{
			var query = _session.QueryOver<Contact>();
			if (gridInfo.Filter != null)
			{
				if (!string.IsNullOrWhiteSpace(gridInfo.Filter.FirstName))
					query.WhereRestrictionOn(x => x.FirstName).IsLike(gridInfo.Filter.FirstName,MatchMode.Anywhere);
				if (!string.IsNullOrWhiteSpace(gridInfo.Filter.LastName))
					query.WhereRestrictionOn(x => x.LastName).IsLike(gridInfo.Filter.LastName, MatchMode.Anywhere);
				if (!string.IsNullOrWhiteSpace(gridInfo.Filter.Address))
					query.WhereRestrictionOn(x => x.Address).IsLike(gridInfo.Filter.Address, MatchMode.Anywhere);
				if (!string.IsNullOrWhiteSpace(gridInfo.Filter.TelephoneNumber))
					query.WhereRestrictionOn(x => x.TelephoneNumber).IsLike(gridInfo.Filter.TelephoneNumber, MatchMode.Anywhere);
			}
			query.Take(gridInfo.PageSize).Skip(gridInfo.Skip);

			return new GridResult<Contact>(query.List<Contact>().ToList(), query.RowCount());
		}


		public Contact GetContactByTelephoneNumber(string telephoneNumber)
		{
			return _session.Query<Contact>().Where(x => x.TelephoneNumber.Replace(" ", String.Empty) == telephoneNumber.Replace(" ", String.Empty)).FirstOrDefault();
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
