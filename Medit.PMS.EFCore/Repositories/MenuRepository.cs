using Medit.PMS.Domain.Entities;
using Medit.PMS.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Medit.PMS.EFCore.Repositories
{
    public class MenuRepository : PMSRepositoryBase<Menu>, IMenuRepository
    {
        public MenuRepository(PMSDbContext dbContext)
            : base(dbContext) { }
    }
}
