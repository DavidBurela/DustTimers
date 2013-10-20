using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CrestParser.Models;
using DustTimers.Web.Repositories;
using DustTimers.Web.Repositories.Uow;

namespace DustTimers.Web.Controllers
{
    public class MaintenanceController : Controller
    {
        public DustTimersUow DustTimersUow { get; set; }

        public MaintenanceController()
        {
            //TODO: This should be IOC
            DustTimersUow = new DustTimersUow();
        }

        
        //
        // GET: /Maintenance/UpdateDistricts
        public async Task<ActionResult> UpdateDistricts()
        {
            try
            {
                await DustTimersUow.UpdateDistrictsWithLatestCrestData();
                
                DustTimersUow.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return View("UpdateDistricts");
        }

        //
        // GET: /Maintenance/UpdateDistrictsAndTickers
        public async Task<ActionResult> UpdateDistrictsAndTickers()
        {
            try
            {
                await DustTimersUow.UpdateDistrictsWithLatestCrestData();
                await DustTimersUow.UpdateCorpTickers();

                DustTimersUow.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return View("UpdateDistricts");
        }
	}
}