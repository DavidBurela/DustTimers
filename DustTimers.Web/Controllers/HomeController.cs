using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CrestParser.Models;
using DustTimers.Web.Models;
using DustTimers.Web.Repositories.Uow;
using WebGrease.Css.Extensions;

namespace DustTimers.Web.Controllers
{
    public class HomeController : Controller
    {
        public DustTimersUow DustTimersUow { get; set; }

        public HomeController()
        {
            DustTimersUow = new DustTimersUow();
        }

        public async Task<ActionResult> Index()
        {
            var corpDistrictDetails = await DustTimersUow.DistrictRepository.GetAllWithDetails().Where(p => p.Owner != null).GroupBy(p => p.Owner.Id)
                .Select(p => new CorpDistrictDetail{CorporationId = p.Key, CorporationName = p.FirstOrDefault().Owner.Name, DistrictsTotal = p.Count(), DistrictsUnderAttack = p.Count(q => q.Attacked)}).ToListAsync();
            var corpIds = corpDistrictDetails.Select(p => p.CorporationId);
            var corporations = await DustTimersUow.CorporationRepository.GetAll().Where(p => corpIds.Contains(p.Id)).ToListAsync();

            foreach (var corpDistrictDetail in corpDistrictDetails)
            {
                CorpDistrictDetail detail = corpDistrictDetail;
                var corporation = corporations.FirstOrDefault(p => p.Id == detail.CorporationId);
                if (corporation != null)
                    corpDistrictDetail.Ticker = corporation.Ticker;
            }

            var viewModel = new HomeViewModel();
            viewModel.CorpDistrictDetails = corpDistrictDetails;

            return View(viewModel);
        }

        public ActionResult About()
        {
            return View();
        }

    }
}