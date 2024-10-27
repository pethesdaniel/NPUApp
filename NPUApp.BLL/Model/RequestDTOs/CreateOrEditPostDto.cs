using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPUApp.BLL.Model.RequestDTOs
{
    public class CreateOrEditPostDto
    {
        public string Title { get; set; } = default!;
        public string PictureUrl { get; set; } = default!;
        public List<long> Parts { get; set; } = new();
        public int CreatorId { get; set; }
    }
}
