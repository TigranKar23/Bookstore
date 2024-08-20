using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffProjects.DTO.UserDtos
{
    public class UserSessionDto
    {
        public long UserId { get; set; }
        public string Token { get; set; }
        public bool IsExpired { get; set; }
    }
}
