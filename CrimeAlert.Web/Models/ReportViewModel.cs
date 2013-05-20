using System;
using CrimeAlert.DataEntities.Enums;

namespace CrimeAlert.Web.Models
{
    public class ReportViewModel
    {
        public string FileUrl { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public string AdminComment { get; set; }
        public bool IsAproved { get; set; }
        public DateTime CreatedOn { get; set; }
        public FileType FileType { get; set; }
        public bool IsPublic { get; set; }
        public int UserId { get; set; }
        public int Id { get; set; }
        public string Longtitude { get; set; }
        public string Latitude { get; set; }
    }
}