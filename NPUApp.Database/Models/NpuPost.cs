using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPUApp.Database.Models
{
    public class NpuPost
    {
        public long Id { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual List<Part> Parts { get; set; } = new();
        public virtual Picture Picture { get; set; } = default!;
    }
}
