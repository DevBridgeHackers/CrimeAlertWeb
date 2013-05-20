using CrimeAlert.DataEntities.Entities;
using FluentNHibernate.Mapping;

namespace CrimeAlert.DataEntities.Mappings
{
    public class BaseMap<T> : ClassMap<T> where T : EntityBase
    {
        protected BaseMap()
        {
            LazyLoad();

            Id(f => f.Id).GeneratedBy.Identity();
            Map(f => f.DeletedOn).Nullable();
            Map(f => f.CreatedOn).ReadOnly().Generated.Insert();
        }
    }
}