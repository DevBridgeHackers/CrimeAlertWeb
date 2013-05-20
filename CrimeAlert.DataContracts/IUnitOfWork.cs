using System;
using System.Data;
using NHibernate;

namespace CrimeAlert.DataContracts
{
    public interface IUnitOfWork : IDisposable
    {
        ISession Session { get; }
        void Commit();
        bool IsActiveTransaction { get; }
        void BeginTransaction();
        void BeginTransaction(IsolationLevel isolationLevel);
        void Rollback();
    }
}
