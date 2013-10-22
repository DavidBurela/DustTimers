using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CrestParser.Models;
using DustTimers.LegacyApi.Models;

namespace DustTimers.Web.Models
{
    public class DistrictTickerViewModel
    {
        public List<District> Districts { get; set; }
        public List<Corporation> Corporations { get; set; }
    }
}