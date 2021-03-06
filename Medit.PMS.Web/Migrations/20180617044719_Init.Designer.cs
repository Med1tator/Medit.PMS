﻿// <auto-generated />
using System;
using Medit.PMS.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Medit.PMS.Web.Migrations
{
    [DbContext(typeof(PMSDbContext))]
    [Migration("20180617044719_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-rtm-30799")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Medit.PMS.Domain.Entities.Department", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<string>("ContactNumber");

                    b.Property<DateTime?>("CreateTime");

                    b.Property<string>("CreateUserId");

                    b.Property<int>("IsDeleted");

                    b.Property<string>("Manager");

                    b.Property<string>("Name");

                    b.Property<string>("ParentId");

                    b.Property<string>("Remarks");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("Medit.PMS.Domain.Entities.Menu", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<string>("Icon");

                    b.Property<string>("Name");

                    b.Property<string>("ParentId");

                    b.Property<string>("Remarks");

                    b.Property<int>("SerialNumber");

                    b.Property<int>("Type");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("Medit.PMS.Domain.Entities.Role", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<DateTime?>("CreateTime");

                    b.Property<string>("CreateUserId");

                    b.Property<string>("Name");

                    b.Property<string>("Remarks");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Medit.PMS.Domain.Entities.RoleMenu", b =>
                {
                    b.Property<string>("RoleId");

                    b.Property<string>("MenuId");

                    b.HasKey("RoleId", "MenuId");

                    b.HasIndex("MenuId");

                    b.ToTable("RoleMenus");
                });

            modelBuilder.Entity("Medit.PMS.Domain.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreateTime");

                    b.Property<string>("CreateUserId");

                    b.Property<string>("DepartmentId");

                    b.Property<string>("EMail");

                    b.Property<int>("IsDeleted");

                    b.Property<DateTime>("LastLoginTime");

                    b.Property<int>("LoginTimes");

                    b.Property<string>("MobileNumber");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<string>("Remarks");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Medit.PMS.Domain.Entities.UserRole", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Medit.PMS.Domain.Entities.RoleMenu", b =>
                {
                    b.HasOne("Medit.PMS.Domain.Entities.Menu", "Menu")
                        .WithMany()
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Medit.PMS.Domain.Entities.Role", "Role")
                        .WithMany("RoleMenus")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Medit.PMS.Domain.Entities.User", b =>
                {
                    b.HasOne("Medit.PMS.Domain.Entities.Department", "Department")
                        .WithMany("Users")
                        .HasForeignKey("DepartmentId");
                });

            modelBuilder.Entity("Medit.PMS.Domain.Entities.UserRole", b =>
                {
                    b.HasOne("Medit.PMS.Domain.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Medit.PMS.Domain.Entities.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
