using CrimeAlert.DataEntities.Entities;

namespace CrimeAlert.DataEntities.Mappings
{
    public class ReportMap : BaseMap<Report>
    {
        public ReportMap()
        {
            Table("Reports");

            Map(x => x.UserId);
            Map(x => x.FileName);
            Map(x => x.Comment);
            Map(x => x.AdminComment);
            Map(x => x.LocationLatitude);
            Map(x => x.LocationLongtitude);
            Map(x => x.IsApproved);
            Map(x => x.FileType);
            Map(x => x.IsPublic);
        }
    }
}