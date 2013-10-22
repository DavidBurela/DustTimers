using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CrestParser.Models;

namespace DustTimers.Web.Repositories
{
    public class DistrictRepository : EFRepository<District>
    {
        public DistrictRepository(DbContext dbContext)
            : base(dbContext)
        {
        }

        public IQueryable<District> GetAllWithDetails()
        {
            return GetAll().Include(p => p.Owner).Include(p => p.Infrastructure);
        }
    }
}