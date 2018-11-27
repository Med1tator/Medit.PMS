using System;
using System.Collections.Generic;
using System.Text;

namespace Medit.PMS.Application.RoleApp.Dtos
{
    public class RoleDto
    {
        public string Id { get; set; }
        public string Code { get; set; }

        public string Name { get; set; }

        public string CreateUserId { get; set; }

        public DateTime? CreateTime { get; set; }

        public string Remarks { get; set; }

        public List<RoleMenuDto> RoleMenus { get; set; }
    }
}
