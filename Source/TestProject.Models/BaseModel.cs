using System;
using TestProject.Interfaces;

namespace TestProject.Models {
    public class BaseModel : ITrackable, IRecoverable, IHasId<int> {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CreatedByUser { get; set; }
        public DateTime CreatedAtTime { get; set; }
        public string UpdatedByUser { get; set; }
        public DateTime UpdatedAtTime { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedByUser { get; set; }
        public DateTime? DeletedAtTime { get; set; }
    }
}
