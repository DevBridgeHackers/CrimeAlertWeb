
namespace CrimeAlert.DataEntities.Entities
{
    public class Report : EntityBase
    {
        public virtual int UserId { get; set; }
        public virtual string FileName { get; set; }
        public virtual string Comment { get; set; }
        public virtual string AdminComment { get; set; }
        public virtual string LocationLatitude { get; set; }
        public virtual string LocationLongtitude { get; set; }
        public virtual bool IsApproved { get; set; }
        public virtual int FileType { get; set; }
        public virtual bool IsPublic { get; set; }
    }
}