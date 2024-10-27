using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPUApp.BLL.Model.DTOs
{
    public class LoginResponseDto
    {
        public string Email { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}
