using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;

namespace AddressBook.Business.Infrastructure
{
	public static class SessionExtensions

	{
		public static void Delete<TEntity>(this ISession session, object id)
		{
			var queryString = string.Format("delete {0} where id = :id",
											typeof(TEntity));
			session.CreateQuery(queryString)
				   .SetParameter("id", id)
				   .ExecuteUpdate();
		}
	}
}
