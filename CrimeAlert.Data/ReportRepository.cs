using System.Collections.Generic;
using System.Linq;
using CrimeAlert.DataContracts;
using CrimeAlert.DataEntities.Entities;

namespace CrimeAlert.Data
{
    public class ReportRepository : RepositoryBase<Report>, IReportRepository
    {
        public ReportRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        public IList<Report> RetrievePublicReports(int count)
        {
            return AsQueryable().Where(r => r.IsPublic).ToList();
        }

        public Report GetReport(int reportId)
        {
            return AsQueryable().FirstOrDefault(r => r.Id == reportId);
        }

        public IList<Report> GetAllReports()
        {
            return AsQueryable().ToList();
        }
    }
}
