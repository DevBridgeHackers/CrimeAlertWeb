using System;
using System.Data;
using CrimeAlert.DataContracts;
using NHibernate;

namespace CrimeAlert.Data.DataContext
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ISessionFactoryProvider sessionFactoryProvider;

        private volatile ISession session;
        private ITransaction transaction;
        private bool disposed;

        public ISession Session
        {
            get
            {
                this.CheckDisposed();
                if (this.session == null)
                {
                    lock (this)
                    {
                        if (this.session == null)
                        {
                            ISession newSession = this.sessionFactoryProvider.SessionFactory.OpenSession();
                            newSession.FlushMode = FlushMode.Auto;

                            this.session = newSession;
                        }
                    }
                }
                return this.session;
            }
        }

        public bool IsActiveTransaction
        {
            get { return this.transaction != null && this.transaction.IsActive; }
        }

        ~UnitOfWork()
        {
            this.Dispose(false);
        }

        public UnitOfWork(ISessionFactoryProvider sessionFactoryProvider)
        {
            this.sessionFactoryProvider = sessionFactoryProvider;
            this.session = null;
            this.transaction = null;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (this.transaction != null && this.transaction.IsActive && !this.transaction.WasRolledBack)
                    {
                        this.transaction.Rollback();
                        this.transaction.Dispose();
                    }

                    if (this.session != null)
                    {
                        this.session.Close();
                        this.session.Dispose();
                    }
                }

                this.disposed = true;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Commit()
        {
            this.CheckDisposed();

            if (this.transaction != null)
            {
                if (!this.transaction.IsActive)
                {
                    throw new InvalidOperationException("No active transaction.");
                }
                this.transaction.Commit();
                this.transaction = null;
            }
            else if (this.session != null)
            {
                this.session.Flush();
            }
        }

        public void BeginTransaction()
        {
            this.CheckDisposed();

            this.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            this.CheckDisposed();

            if (this.transaction == null)
            {
                this.transaction = this.Session.BeginTransaction(isolationLevel);
            }
            else
            {
                throw new DataException("Transaction already created for this unit of work.");
            }
        }

        public void Rollback()
        {
            this.CheckDisposed();

            if (this.transaction != null)
            {
                if (this.transaction.IsActive)
                {
                    this.transaction.Rollback();
                    this.transaction = null;
                }
            }
            else
            {
                throw new DataException("No transaction created for this unit of work.");
            }
        }

        private void CheckDisposed()
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException("UnitOfWork");
            }
        }
    }
}
