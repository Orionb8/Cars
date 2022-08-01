using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Interfaces {
    public interface IRecoverable {
        bool IsDeleted { get; set; }
        string DeletedByUser { get; set; }
        DateTime? DeletedAtTime { get; set; }
    }
}
