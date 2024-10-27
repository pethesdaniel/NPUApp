using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPUApp.BLL.Model.DTOs
{
    public class PartDto
    {
        public long PartNumber { get; set; }
        public string FriendlyName { get; set; } = null!;
    }
}
