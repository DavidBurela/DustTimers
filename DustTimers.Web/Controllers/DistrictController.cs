using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CrestParser.Models;
using DustTimers.LegacyApi.Models;
using DustTimers.Web.Data;
using DustTimers.Web.Models;
using DustTimers.Web.Repositories.Uow;

namespace DustTimers.Web.Controllers
{
    public class DistrictController : Controller
    {
        public DustTimersUow DustTimersUow { get; set; }

        public DistrictController()
        {
            DustTimersUow = new DustTimersUow();
        }

        // GET: /District/
        public async Task<ActionResult> Index()
        {
            var corporations = await DustTimersUow.CorporationRepository.GetAll().ToListAsync();
            
            var viewModel = new DistrictTickerViewModel();
            viewModel.Districts = await DustTimersUow.DistrictRepository.GetAllWithDetails().ToListAsync();
            viewModel.Corporations = corporations;
            return View("Index", viewModel);
        }

        // GET: /District/Ticker/5
        public async Task<ActionResult> Ticker(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var corporation = await DustTimersUow.CorporationRepository.GetAll().Where(p => string.Compare(p.Ticker, id, StringComparison.OrdinalIgnoreCase) == 0).FirstOrDefaultAsync();
            if (corporation == null)
            {
                return HttpNotFound();
            }

            var districts = await DustTimersUow.DistrictRepository.GetAllWithDetails().Where(p => p.Owner.Id == corporation.Id).ToListAsync();
            if (districts == null)
            {
                return HttpNotFound();
            }

            var viewModel = new DistrictTickerViewModel();
            viewModel.Ticker = id;
            viewModel.Districts = districts;
            viewModel.Corporations = new List<Corporation> {corporation};
            return View("Index", viewModel);
        }

    }
}
