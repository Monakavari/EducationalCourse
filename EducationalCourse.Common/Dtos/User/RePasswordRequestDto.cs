using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalCourse.Common.Dtos.User
{
    public class RePasswordRequestDto
    {
        public int UserId { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
    }
}
