using System;
using System.Collections.Generic;
using System.Text;

namespace Medit.PMS.Domain.Entities
{
   public class UserRole
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
