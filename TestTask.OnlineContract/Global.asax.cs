using Domain.Auction.Abstract;
using Domain.Auction.Model;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TestTask.OnlineContract.Concrete;
using TestTask.OnlineContract.Hubs;
using TestTask.OnlineContract.Infrastructure;
using TestTask.OnlineContract.Models;

namespace TestTask.OnlineContract
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

                      
            var kerner = new StandardKernel(new SailerSingletonModule());
            var sailer = kerner.Get<Sailer>();
            sailer.StartAuction();
        }
    }
}
