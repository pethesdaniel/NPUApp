using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPUApp.Database.Models
{
    public class Part
    {
        [Key]
        public long PartNumber { get; set; }
        public string FriendlyName { get; set; } = null!;
    }
}
