using System;
using System.Collections.Generic;
using System.Text;

namespace Medit.PMS.Domain.Entities
{
   public class RoleMenu
    {
        public string RoleId { get; set; }
        public string MenuId { get; set; }
        public Role Role { get; set; }
        public Menu Menu { get; set; }
    }
}
