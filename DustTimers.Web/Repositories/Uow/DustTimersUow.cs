using System;
using System.Collections.Generic;
using System.Linq;
using CrestParser.Models;
using DustTimers.Web.Data;

namespace DustTimers.Web.Repositories.Uow
{
    public class DustTimersUow
    {
        private DustTimersDbContext DbContext { get; set; }

        public IEFRepository<District> DistrictRepository { get; set; }

        public DustTimersUow()
        {
            CreateDbContext();

            // Should be IOC
            DistrictRepository = new EFRepository<District>(DbContext);
        }

        protected void CreateDbContext()
        {
            DbContext = new DustTimersDbContext();

            DbContext.Configuration.ProxyCreationEnabled = false;
            DbContext.Configuration.LazyLoadingEnabled = false;
            DbContext.Configuration.ValidateOnSaveEnabled = false;
        }

        public void Commit()
        {
            DbContext.SaveChanges();
        }
    }
}