using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPUApp.BLL.Model.DTOs
{
    public class PostDto
    {
        public string Title { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public DateTime CreatedOn { get; set; } = default!;
    }
}
