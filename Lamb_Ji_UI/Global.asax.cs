﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using Lamb_Ji_DAL;
using Lamb_Ji_ViewModel;

namespace Lamb_Ji_UI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

              Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Lutteur, LutteurViewModel>();
                cfg.CreateMap<LutteurViewModel, Lutteur>();


            });
        }
    }
}
