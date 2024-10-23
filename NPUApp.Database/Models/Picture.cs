using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPUApp.Database.Models
{
    public class Picture
    {
        public User Owner { get; set; } = default!;
        public NpuPost UsedBy { get; set; } = default!;
        public Guid Identifier { get; set; }
    }
}
