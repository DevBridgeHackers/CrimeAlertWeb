using System.Collections.Generic;

namespace CrimeAlert.Web.Models
{
    public class AdminViewModel
    {
        public IList<ReportViewModel> Reports { get; set; }
    }
}