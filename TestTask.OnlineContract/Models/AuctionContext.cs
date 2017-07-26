using Domain.Auction.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TestTask.OnlineContract.Models
{
    public class AuctionContext: DbContext
    {
        public AuctionContext() : base("LogsContext") { }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Auction> Auctions { get; set; }
    }
}