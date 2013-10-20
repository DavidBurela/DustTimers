using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CrestParser.Models;
using CrestParser.Resources;
using DustTimers.LegacyApi.Models;
using DustTimers.LegacyApi.Resources;
using DustTimers.Web.Data;

namespace DustTimers.Web.Repositories.Uow
{
    public class DustTimersUow
    {
        private DustTimersDbContext DbContext { get; set; }

        // Repositories
        public IEFRepository<Constellation> ConstellationRepository { get; set; }
        public IEFRepository<District> DistrictRepository { get; set; }
        public IEFRepository<Infrastructure> InfrastructureRepository { get; set; }
        public IEFRepository<Owner> OwnerRepository { get; set; }
        public IEFRepository<Planet> PlanetRepository { get; set; }
        public IEFRepository<Region> RegionRepository { get; set; }
        public IEFRepository<CrestParser.Models.System> SystemRepository { get; set; }

        public IEFRepository<Corporation> CorporationRepository { get; set; }

        public DustTimersUow()
        {
            CreateDbContext();

            // Should be IOC
            ConstellationRepository = new EFRepository<Constellation>(DbContext);
            DistrictRepository = new EFRepository<District>(DbContext);
            InfrastructureRepository = new EFRepository<Infrastructure>(DbContext);
            OwnerRepository = new EFRepository<Owner>(DbContext);
            PlanetRepository = new EFRepository<Planet>(DbContext);
            RegionRepository = new EFRepository<Region>(DbContext);
            SystemRepository = new EFRepository<CrestParser.Models.System>(DbContext);
            CorporationRepository = new EFRepository<Corporation>(DbContext);
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

                // Search for each entity so that we can just link, rather than re-adding
                var constellation = ConstellationRepository.GetById(crestDistrict.Constellation.Id) ??
                                    crestDistrict.Constellation;

                var infrastructure = InfrastructureRepository.GetById(crestDistrict.Infrastructure.Id) ??
                                    crestDistrict.Infrastructure;

                var owner = OwnerRepository.GetById(crestDistrict.Owner.Id) ??
                                    crestDistrict.Owner;

                var planet = PlanetRepository.GetById(crestDistrict.Planet.Id) ??
                                    crestDistrict.Planet;

                var region = RegionRepository.GetById(crestDistrict.Region.Id) ??
                                    crestDistrict.Region;

                var planetSystem = SystemRepository.GetById(crestDistrict.System.Id) ??
                                    crestDistrict.System;
                
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
                currentDistrict.Constellation = constellation;
                currentDistrict.Generating = crestDistrict.Generating;
                currentDistrict.Href = crestDistrict.Href;
                currentDistrict.Id = crestDistrict.Id;
                currentDistrict.Id_str = crestDistrict.Id_str;
                currentDistrict.Index = crestDistrict.Index;
                currentDistrict.Index_str = crestDistrict.Index_str;
                currentDistrict.Infrastructure = infrastructure;
                currentDistrict.Latitude = crestDistrict.Latitude;
                currentDistrict.Locked = crestDistrict.Locked;
                currentDistrict.Longitude = crestDistrict.Longitude;
                currentDistrict.Name = crestDistrict.Name;
                currentDistrict.NextReinforce = crestDistrict.NextReinforce;
                currentDistrict.Owner = owner;
                currentDistrict.Planet = planet;
                currentDistrict.Region = region;
                currentDistrict.Reinforce = crestDistrict.Reinforce;
                currentDistrict.Reinforce_str = crestDistrict.Reinforce_str;
                currentDistrict.System = planetSystem;
            }
            
        }

        public async Task UpdateCorpTickers()
        {
            // Query EVE Api for each corp without a ticker
            // update each corp with ticker details

            var ownerIds = OwnerRepository.GetAll().Select(p => p.Id);
            var currentCorpIds = CorporationRepository.GetAll().Select(p => p.Id);

            var missingCorporationIds = ownerIds.Where(p => currentCorpIds.Contains(p) == false);

            foreach (var missingCorporationId in missingCorporationIds)
            {
                try
                {
                    var corporation = await CorporationResource.GetCorporation(missingCorporationId);
                    CorporationRepository.Add(corporation);
                }
                catch (WebException e)
                {
                    // Just continue
                }
            }
        }
    }
}