using System.Collections.Generic;

namespace WebAppProfilersSample.Shared.Data.Entities
{
    public class SomeEntity
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public ICollection<SomeOtherEntity> Others { get; set; } = new HashSet<SomeOtherEntity>();
    }
}
