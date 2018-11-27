using AutoMapper;
using Medit.PMS.Application.DepartmentApp.Dtos;
using Medit.PMS.Application.MenuApp.Dtos;
using Medit.PMS.Application.RoleApp.Dtos;
using Medit.PMS.Application.UserApp.Dtos;
using Medit.PMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Medit.PMS.Application
{
    public class PMSMapper
    {
        public static void Initialize()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<Department, DepartmentDto>();
                config.CreateMap<Menu, MenuDto>();
                config.CreateMap<Role, RoleDto>();
                config.CreateMap<User, UserDto>();
                config.CreateMap<UserRole, UserRoleDto>();
                config.CreateMap<RoleMenu, RoleMenuDto>();

                config.CreateMap<DepartmentDto,Department>();
                config.CreateMap<MenuDto, Menu>();
                config.CreateMap<RoleDto, Role>();
                config.CreateMap<UserDto, User>();
                config.CreateMap<UserRoleDto, UserRole>();
                config.CreateMap<RoleMenuDto, RoleMenu>();
            });
        }
    }
}
