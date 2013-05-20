using System.Collections.Generic;
using CrimeAlert.DataEntities.Entities;

namespace CrimeAlert.DataContracts
{
    public interface IReportRepository : IRepository<Report>, IUnitOfWorkRepository
    {
        IList<Report> RetrievePublicReports(int count);
        Report GetReport(int reportId);
        IList<Report> GetAllReports();
    }
}
