using System.Collections.Generic;
using System.Web.Mvc;
using CrimeAlert.DataEntities.Enums;
using CrimeAlert.ServiceContracts;
using CrimeAlert.Web.Models;

namespace CrimeAlert.Web.Controllers
{
    [Authorize]
    public partial class AdminController : Controller
    {
        private readonly IReportService reportService;
        private readonly IUserService userService;

        public AdminController(IReportService reportService, IUserService userService)
        {
            this.reportService = reportService;
            this.userService = userService;
        }

        public virtual ActionResult Index()
        {
            var model = new AdminViewModel();

            var reports = reportService.GetReports();

            model.Reports = new List<ReportViewModel>();
            foreach(var report in reports)
            {
                var user = userService.GetUser(report.UserId);
                model.Reports.Add(new ReportViewModel
                    {
                        AdminComment = report.AdminComment,
                        Comment = report.Comment,
                        CreatedOn = report.CreatedOn,
                        FileType = (FileType)report.FileType,
                        FileUrl = "http://crimealert.s3.amazonaws.com/" + report.FileName,
                        IsAproved = report.IsApproved,
                        IsPublic = report.IsPublic,
                        UserId = report.UserId,
                        Name = user != null ? string.Format("{0} {1}", user.FirstName, user.LastName) : string.Empty,
                        Id = report.Id
                    });
            }
            return View(model);
        }

        public virtual ActionResult Edit(int reportId)
        {
            var report = reportService.GetReport(reportId);
            var model = new ReportViewModel
                {
                    AdminComment = report.AdminComment,
                    Comment = report.Comment,
                    CreatedOn = report.CreatedOn,
                    FileType = (FileType)report.FileType,
                    FileUrl = "http://crimealert.s3.amazonaws.com/" + report.FileName,
                    IsAproved = report.IsApproved,
                    IsPublic = report.IsPublic,
                    UserId = report.UserId,
                    Id = report.Id,
                    Longtitude = report.LocationLongtitude,
                    Latitude = report.LocationLatitude
                };
            return View(model);
        }

        [HttpPost]
        public virtual ActionResult Edit(ReportViewModel model)
        {
            if (ModelState.IsValid)
            {
                var report = reportService.GetReport(model.Id);
                if (report != null)
                {
                    report.IsApproved = model.IsAproved;
                    report.AdminComment = model.AdminComment;
                    reportService.UpdateAdminInfo(report);
                }
                return RedirectToAction(MVC.Admin.Index());
            }
            ModelState.AddModelError("", "Errror!!");
            return View(model);
        }
    }
}
