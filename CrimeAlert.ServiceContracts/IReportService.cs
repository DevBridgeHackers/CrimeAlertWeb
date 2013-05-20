using System.Collections.Generic;
using CrimeAlert.DataEntities.Entities;
using CrimeAlert.DataEntities.Enums;

namespace CrimeAlert.ServiceContracts
{
    public interface IReportService
    {
        Report AddReportPhoto(User user, string fileName, FileType fileType);
        bool AddUserReport(User user, string fileName, string comment, FileType filyType, string latitude, string longtitude);
        Report GetReport(int id);
        IList<Report> GetUserReports(int userId);
        IList<Report> GetReports();
        IList<Report> GetNewestPublicReports();
        void UpdateReportInfo(int reportId, string securityToken, bool isPublic, string comment, string locationLatitude, string locationLongtitude);
        void DeleteReport(int reportId, string securityToken);
        void Test();
        void UpdateAdminInfo(Report report);
    }
}
