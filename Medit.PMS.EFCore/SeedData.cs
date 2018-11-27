using Medit.PMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Medit.PMS.EFCore
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new PMSDbContext(serviceProvider.CreateScope().ServiceProvider.GetRequiredService<DbContextOptions<PMSDbContext>>()))
            {
                if (context.Users.Any())
                {
                    return;
                }

                string departmentId = "DEPT-" + Guid.NewGuid().ToString();

                context.Departments.Add(
                    new Department
                    {
                        Id = departmentId,
                        Name = "集团总部",
                        ParentId = string.Empty
                    }
                );

                context.Users.Add(
                    new User
                    {
                        UserName = "admin",
                        Password = "123456",
                        Name = "管理员",
                        DepartmentId = departmentId
                    }
                );

                context.Menus.AddRange(
                    new Menu
                    {
                        Name = "组织机构管理",
                        Code = "Department",
                        SerialNumber = 0,
                        ParentId = string.Empty,
                        Icon = "fa fa-link",
                        Url="/Department"
                    },
                    new Menu
                    {
                        Name = "角色管理",
                        Code = "Role",
                        SerialNumber = 1,
                        ParentId = string.Empty,
                        Icon = "fa fa-link"
                    },
                   new Menu
                   {
                       Name = "用户管理",
                       Code = "User",
                       SerialNumber = 2,
                       ParentId = string.Empty,
                       Icon = "fa fa-link"
                   },
                   new Menu
                   {
                       Name = "功能管理",
                       Code = "Department",
                       SerialNumber = 3,
                       ParentId = string.Empty,
                       Icon = "fa fa-link"
                   }
                );
                context.SaveChanges();
            }
        }
    }
}