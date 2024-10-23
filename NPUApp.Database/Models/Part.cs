using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPUApp.Database.Models
{
    public class Part
    {
        public int PartNumber { get; set; }
        public string FriendlyName { get; set; } = null!;
    }
}
