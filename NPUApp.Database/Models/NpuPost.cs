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
        public string Title { get; set; } = default!;
        public DateTime CreatedOn { get; set; } = default!;
        public virtual User User { get; set; } = null!;
        public virtual long UserId { get; set; }
        public virtual List<Part> Parts { get; set; } = new();
        public virtual List<Rating> Ratings { get; set; } = new();
        public string PictureUrl { get; set; } = default!;
    }
}
