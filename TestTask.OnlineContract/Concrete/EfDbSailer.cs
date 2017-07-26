using Domain.Auction.Abstract;
using Domain.Auction.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestTask.OnlineContract.Models;

namespace TestTask.OnlineContract.Concrete
{
    public class EfDbSailer:Sailer
    {
        AuctionContext _Repository;
        public EfDbSailer(Product prod, AuctionContext repository, IClientHelper clientHelper):
            base(prod, clientHelper)
        {
            _Repository = repository;
        }

        protected override void OnEndAuction()
        {
            this.Auction.Id = Guid.NewGuid();
            _Repository.Auctions.Add(this.Auction);
            _Repository.SaveChanges();
        }

        protected override void OnSailed()
        {
            var winner = _Repository.Users.First(u => u.Name == Auction.Winner.Name);
            winner.Money -= Auction.Price;
            _Repository.SaveChanges();
        }
    }
}