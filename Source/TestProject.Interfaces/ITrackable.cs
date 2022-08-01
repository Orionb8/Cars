using System;

namespace TestProject.Interfaces {
    public interface ITrackable {
        string CreatedByUser { get; set; }
        DateTime CreatedAtTime { get; set; }
        string UpdatedByUser { get; set; }
        DateTime UpdatedAtTime { get; set; }
    }
}
