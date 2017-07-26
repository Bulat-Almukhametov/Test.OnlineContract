using Domain.Auction.Abstract;
using Domain.Auction.Model;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestTask.OnlineContract.Concrete;
using TestTask.OnlineContract.Models;

namespace TestTask.OnlineContract.Infrastructure
{
    public class SailerSingletonModule: NinjectModule
    {
        public override void Load()
        {
            this.Bind<Product>().ToSelf().WithPropertyValue("Id", Guid.NewGuid()).WithPropertyValue("Name", "Продукт А");
            this.Bind<IClientHelper>().To<SrClientHelper>().InSingletonScope();
            this.Bind<Sailer>().To<EfDbSailer>().InSingletonScope();
            
        }
    }
}