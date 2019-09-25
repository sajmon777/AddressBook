
using Castle.DynamicProxy;
using NHibernate;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AddressBook.Business.Infrastructure
{
	public class TransactionInterceptor : Castle.DynamicProxy.IInterceptor
	{
		private readonly ISession _session;
		private ITransaction _transaction;
		public TransactionInterceptor(ISession session)
		{
			_session = session;
		}

		public void Intercept(IInvocation invocation)
		{
			try
			{
				_transaction = _session.BeginTransaction();
				invocation.Proceed();
				_transaction.Commit();
			}
			catch
			{
				_transaction.Rollback();
				throw;
			}
			finally
			{
				if (_transaction != null)
				{
					_transaction.Dispose();
					_transaction = null;
				}
				_session.Close();
			}

		}
	}
}
