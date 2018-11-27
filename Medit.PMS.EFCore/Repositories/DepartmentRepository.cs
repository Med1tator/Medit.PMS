using Medit.PMS.Domain.Entities;
using Medit.PMS.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Medit.PMS.EFCore.Repositories
{
    public class DepartmentRepository : PMSRepositoryBase<Department>, IDepartmentRepository
    {
        public DepartmentRepository(PMSDbContext dbcontext)
            : base(dbcontext) { }
    }
}