using System;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using CrimeAlert.DataContracts;
using CrimeAlert.DataContracts.Exceptions;
using CrimeAlert.DataEntities;
using NHibernate;
using NHibernate.Exceptions;
using NHibernate.Linq;
using DataException = CrimeAlert.DataContracts.Exceptions.DataException;

namespace CrimeAlert.Data
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity>, IUnitOfWorkRepository
        where TEntity : class, IEntity
    {
        private const int UniqueIndexViolationExceptionNumber1 = 2601;
        private const int UniqueIndexViolationExceptionNumber2 = 2627;
        private IUnitOfWork unitOfWork;

        private IUnitOfWork UnitOfWork
        {
            get
            {
                if (unitOfWork == null)
                {
                    throw new DataException(string.Format("Repository {0} has no assigned unit of work.", GetType().Name));
                }
                return unitOfWork;
            }
        }

        protected RepositoryBase(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void Use(IUnitOfWork unitOfWorkToUse)
        {
            unitOfWork = unitOfWorkToUse;
        }

        public TEntity First(int id)
        {
            TEntity entity = FirstOrDefault(id);
            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(TEntity), id);
            }
            return entity;
        }

        public TEntity First(Expression<Func<TEntity, bool>> filter)
        {
            TEntity entity = FirstOrDefault(filter);

            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(TEntity), filter.ToString());
            }

            return entity;
        }

        public TEntity FirstOrDefault(int id)
        {
            return UnitOfWork.Session.Query<TEntity>().FirstOrDefault(f => f.Id == id);
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> filter)
        {
            return UnitOfWork.Session.Query<TEntity>().Where(filter).FirstOrDefault();
        }

        public IQueryable<TEntity> AsQueryable(Expression<Func<TEntity, bool>> filter)
        {
            return AsQueryable().Where(filter);
        }

        public IQueryable<TEntity> AsQueryable()
        {
            return UnitOfWork.Session.Query<TEntity>().Where(entity => entity.DeletedOn == null);
        }

        public bool Any(Expression<Func<TEntity, bool>> filter)
        {
            return AsQueryable().Where(filter).Any();
        }

        public void Save(TEntity entity)
        {
            try
            {
                var newEnt = false;
                if (entity.Id == 0)
                {
                    entity.CreatedOn = DateTime.Now;
                    newEnt = true;
                }

                UnitOfWork.Session.SaveOrUpdate(entity);
                if (!newEnt)
                {
                    UnitOfWork.Commit();
                }
            }
            catch (GenericADOException ex)
            {
                var innerEx = ex.InnerException;

                if (innerEx != null && (((SqlException)innerEx).Number == UniqueIndexViolationExceptionNumber1 || ((SqlException)innerEx).Number == UniqueIndexViolationExceptionNumber2))
                {
                    throw new RecordAlreadyExistsException(typeof(TEntity), entity.Id, ex);
                }

                throw;
            }
        }

        public void Delete(TEntity entity)
        {
            entity.DeletedOn = DateTime.Now;

            UnitOfWork.Session.SaveOrUpdate(entity);
        }

        public void Delete(int id)
        {
            TEntity entity = First(id);
            Delete(entity);
        }

        public void Destroy(TEntity entity)
        {
            UnitOfWork.Session.Delete(entity);
        }

        public void Refresh(TEntity entity)
        {
            UnitOfWork.Session.Refresh(entity, LockMode.UpgradeNoWait);
        }
    }
}
