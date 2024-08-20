using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.DAL.Models;

namespace Bookstore.DAL.Models
{
    public class UserSession : BaseModel
    {
        public long UserId { get; set; }
        public string Token { get; set; }
        public bool IsExpired { get; set; }

        public User User { get; set; }
    }
}