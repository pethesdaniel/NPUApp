using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPUApp.BLL.Model.RequestDTOs
{
    public class SearchPostDto
    {
        public string? TitleContains { get; set; }
        public long? FromUser { get; set; }
        public List<SearchPostPartDto>? Parts { get; set; }
    }

    public class SearchPostPartDto
    {
        public string? NameContains { get; set; }
        public long? PartNumber { get; set; }
    }
}
