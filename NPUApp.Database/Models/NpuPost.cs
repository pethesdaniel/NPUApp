using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPUApp.Database.Models
{
    public class NpuPost
    {
        public User User { get; set; } = null!;
        public IEnumerable<Part> LegoParts = default!;
        public Picture Picture { get; set; } = default!;
    }
}
