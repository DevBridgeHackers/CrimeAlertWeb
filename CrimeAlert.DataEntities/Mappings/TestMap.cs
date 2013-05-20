using CrimeAlert.DataEntities.Entities;

namespace CrimeAlert.DataEntities.Mappings
{
    public class TestMap : BaseMap<Test>
    {
        public TestMap()
        {
            Table("Test");

            Map(x => x.Value);
        }
    }
}