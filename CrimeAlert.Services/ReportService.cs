using System;
using System.Collections.Generic;
using CrimeAlert.DataContracts;
using CrimeAlert.DataEntities.Entities;
using CrimeAlert.DataEntities.Enums;
using CrimeAlert.ServiceContracts;
using CrimeAlert.Services.Exceptions;

namespace CrimeAlert.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository reportRepository;
        private readonly IUserService userService;

        public ReportService(IReportRepository reportRepository, IUserService userService)
        {
            this.reportRepository = reportRepository;
            this.userService = userService;
        }

        public Report AddReportPhoto(User user, string fileName, FileType fileType)
        {
            try
            {
                var report = new Report
                    {
                        UserId = user.Id, 
                        FileName = fileName,
                        FileType = (int)fileType,
                        IsApproved = false,
                        IsPublic = false,
                        LocationLatitude = string.Empty,
                        LocationLongtitude = string.Empty
                    };
                reportRepository.Save(report);
                return report;
            }
            catch (Exception exception)
            {
                throw new ReportServiceException("Failed to add photo", exception); // TODO
            }
        }

        public bool AddUserReport(User user, string fileName, string comment, FileType filyType, string latitude, string longtitude)
        {
            try
            {
                if (user == null)
                {
                    throw new ArgumentNullException("user");
                }
                return false;

            }
            catch (Exception exception)
            {
                throw new ReportServiceException("Failed to create a report.", exception); // TODO list parameters
            }
        }

        public Report GetReport(int id)
        {
            try
            {
                return reportRepository.GetReport(id);
            }
            catch (Exception exception)
            {
                throw new ReportServiceException("failed to ret", exception); // todo
            }
        }

        public IList<Report> GetUserReports(int userId)
        {
            throw new System.NotImplementedException();
        }

        public IList<Report> GetReports()
        {
            return reportRepository.GetAllReports();
        }

        public IList<Report> GetNewestPublicReports()
        {
            try
            {
                return reportRepository.RetrievePublicReports(30);
            }
            catch (Exception exception)
            {
                throw new ReportServiceException("failed to ret", exception); // todo
            }
        }

        public void UpdateReportInfo(int reportId, string securityToken, bool isPublic, string comment, string locationLatitude, string locationLongtitude)
        {
            try
            {
                var user = userService.GetUser(securityToken);
                if(user != null)
                {
                    var report = reportRepository.GetReport(reportId);
                    if(report != null)
                    {
                        report.IsPublic = isPublic;
                        report.Comment = comment;
                        report.LocationLatitude = locationLatitude;
                        report.LocationLongtitude = locationLongtitude;
                        reportRepository.Save(report);
                    }
                }
            }
            catch(Exception exception)
            {
                throw  new ReportServiceException("Failed update", exception); //todo
            }
        }

        public void DeleteReport(int reportId, string securityToken)
        {
            try
            {
                var user = userService.GetUser(securityToken);
                if (user != null)
                {
                    var report = reportRepository.GetReport(reportId);
                    if (report != null)
                    {
                        reportRepository.Delete(report);
                    }
                }
            }
            catch (Exception exception)
            {
                throw new ReportServiceException("Failed delete", exception); //todo
            }
        }

        public void Test()
        {
            var report = reportRepository.GetReport(9);
            report.Comment = "Test";
            reportRepository.Save(report);
        }

        public void UpdateAdminInfo(Report report)
        {
            try
            {
                reportRepository.Save(report);
            }
            catch (Exception exception)
            {
                throw new ReportServiceException("Failed update", exception); //todo
            }
        }
    }
}
