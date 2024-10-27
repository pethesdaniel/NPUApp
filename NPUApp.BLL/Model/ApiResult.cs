using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPUApp.BLL.Model
{
    public class ApiResult<T>
    {
        public bool Success { get; set; } = false;
        public T Result { get; set; } = default!;
        public List<string> Errors { get; set; } = new();
    }
}
