using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPUApp.Database.Models
{
    public class User
    {
        public string Username { get; set; } = null!;
        public IEnumerable<NpuPost> NpuPosts { get; set; } = default!;
        public IEnumerable<Picture> Pictures { get; set; } = default!;
    }
}
