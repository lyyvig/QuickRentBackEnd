using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs {
    public class ChangePasswordDto {
        //email
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
         
    }
}
