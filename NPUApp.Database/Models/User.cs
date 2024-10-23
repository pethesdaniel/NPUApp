using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPUApp.Database.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Username { get; set; } = null!;
        public virtual List<NpuPost> NpuPosts { get; set; } = default!;
        public virtual List<Picture> Pictures { get; set; } = default!;
    }
}
