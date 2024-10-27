using NPUApp.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPUApp.BLL.Model.RequestDTOs
{
    public class CreateOrEditRatingDto
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        public int CreativityScore { get; set; }
        public int UniquenessScore { get; set; }
    }
}
