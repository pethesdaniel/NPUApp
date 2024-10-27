using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPUApp.Database.Models
{
    public class User : IdentityUser<long>
    {
        public virtual List<NpuPost> NpuPosts { get; set; } = default!;
    }
}
