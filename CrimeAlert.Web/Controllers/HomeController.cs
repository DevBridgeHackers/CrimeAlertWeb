using System.Collections.Generic;
using System.Web.Mvc;
using CrimeAlert.ServiceContracts;
using CrimeAlert.Web.Models;

namespace CrimeAlert.Web.Controllers
{
    public partial class HomeController : Controller
    {
        private readonly IUploadService uploadService;
        private readonly IUserService userService;
        private readonly IReportService reportService;


        public HomeController(IUploadService uploadService, IUserService userService, IReportService reportService)
        {
            this.uploadService = uploadService;
            this.userService = userService;
            this.reportService = reportService;
        }

        public virtual ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            var model = new List<ReportViewModel>();
            var reports = reportService.GetNewestPublicReports();
            foreach (var report in reports)
            {
                var user = userService.GetUser(report.UserId);

                model.Add(new ReportViewModel
                    {
                        FileUrl = "http://crimealert.s3.amazonaws.com/" + report.FileName,
                        Name = user != null ? string.Format("{0} {1}", user.FirstName, user.LastName) : string.Empty,
                        CreatedOn = report.CreatedOn,
                        Comment = report.Comment,
                        AdminComment = report.Comment,
                        Longtitude =  report.LocationLongtitude,
                        Latitude = report.LocationLatitude
                    });   
            }

            return View(model);
        }

        public virtual ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            // todo clean
            //reportService.Test();
            //userService.Test();

            return View();
        }

        public virtual ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
