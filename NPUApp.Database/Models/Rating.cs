using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPUApp.Database.Models
{
    public class Rating
    {
        public long Id { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual NpuPost Post { get; set; } = null!;
        public int CreativityScore { get; set; }
        public int UniquenessScore { get; set; }
    }
}
