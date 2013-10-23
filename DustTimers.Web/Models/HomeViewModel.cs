using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CrestParser.Models;
using DustTimers.LegacyApi.Models;

namespace DustTimers.Web.Models
{
    public class HomeViewModel
    {
        public List<CorpDistrictDetail> CorpDistrictDetails { get; set; }
    }

    public class CorpDistrictDetail
    {
        public int CorporationId { get; set; }
        public string CorporationName { get; set; }
        public string Ticker { get; set; }
        public int DistrictsTotal { get; set; }
        public int DistrictsUnderAttack { get; set; }
    }
}