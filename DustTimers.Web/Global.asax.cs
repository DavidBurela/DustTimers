using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using DustTimers.Web.Data;
using DustTimers.Web.Migrations;
using DustTimers.Web.Models;

namespace DustTimers.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DustTimersDbContext, DustTimersDbConfiguration>());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, ApplicationDbConfiguration>());
        }
    }
}
