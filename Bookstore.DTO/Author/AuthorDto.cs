using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffProjects.DTO.UserDtos
{
    public class AuthorDto : BaseAdminDtoWithDate
    {
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}