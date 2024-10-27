using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPUApp.BLL.Model.DTOs
{
    public class PostDto
    {
        public long Id { get; set; }
        public string Title { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string PictureUrl { get; set; } = default!;
        public DateTime CreatedOn { get; set; } = default!;
        public List<long> Parts { get; set; } = new();
        public double AvgCreativityScore { get; set; }
        public double AvgUniquenessScore { get; set; }
    }
}
