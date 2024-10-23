using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPUApp.Database.Models
{
    public class Picture
    {
        public long Id { get; set; }
        [ForeignKey("User")]
        public virtual User Owner { get; set; } = default!;
        [ForeignKey("NpuPost")]
        public virtual NpuPost UsedBy { get; set; } = default!;
        public Guid Identifier { get; set; }
    }
}
