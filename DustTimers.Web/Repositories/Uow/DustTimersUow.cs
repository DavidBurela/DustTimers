using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CrestParser.Models;
using CrestParser.Resources;
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

        public async Task UpdateDistrictsWithLatestCrestData()
        {
            // Check when the last time this was executed


            // Get all the districts
            var crestDistricts = await DistrictsResource.GetDistricts();

            // Cache the repository with all the districts
            var currentDistricts = await DistrictRepository.GetAll().ToListAsync();

            // Update districts in the database
            foreach (var crestDistrict in crestDistricts)
            {
                var currentDistrict = currentDistricts.FirstOrDefault(p => p.Id == crestDistrict.Id);
                if (currentDistrict == null)
                {
                    currentDistrict = new District();
                    DistrictRepository.Add(currentDistrict);
                }

                // Set each property manually. 
                // TODO: There must be a nicer way to automater this later
                currentDistrict.Attacked = crestDistrict.Attacked;
                currentDistrict.CloneCapacity = crestDistrict.CloneCapacity;
                currentDistrict.CloneCapacity_str = crestDistrict.CloneCapacity_str;
                currentDistrict.CloneRate = crestDistrict.CloneRate;
                currentDistrict.CloneRate_str = crestDistrict.CloneRate_str;
                currentDistrict.Clones = crestDistrict.Clones;
                currentDistrict.Clones_str = crestDistrict.Clones_str;
                currentDistrict.Conquerable = crestDistrict.Conquerable;
                currentDistrict.Constellation = crestDistrict.Constellation;
                currentDistrict.Generating = crestDistrict.Generating;
                currentDistrict.Href = crestDistrict.Href;
                currentDistrict.Id = crestDistrict.Id;
                currentDistrict.Id_str = crestDistrict.Id_str;
                currentDistrict.Index = crestDistrict.Index;
                currentDistrict.Index_str = crestDistrict.Index_str;
                currentDistrict.Infrastructure = crestDistrict.Infrastructure;
                currentDistrict.Latitude = crestDistrict.Latitude;
                currentDistrict.Locked = crestDistrict.Locked;
                currentDistrict.Longitude = crestDistrict.Longitude;
                currentDistrict.Name = crestDistrict.Name;
                currentDistrict.NextReinforce = crestDistrict.NextReinforce;
                currentDistrict.Owner = crestDistrict.Owner;
                currentDistrict.Planet = crestDistrict.Planet;
                currentDistrict.Region = crestDistrict.Region;
                currentDistrict.Reinforce = crestDistrict.Reinforce;
                currentDistrict.Reinforce_str = crestDistrict.Reinforce_str;
                currentDistrict.System = crestDistrict.System;
            }
            
        }

        public async Task UpdateCorpTickers()
        {
            // Query EVE Api for each corp without a ticker
            // update each corp with ticker details

        }
    }
}