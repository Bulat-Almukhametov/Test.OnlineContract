using Domain.Auction.Abstract;
using Domain.Auction.Enum;
using Domain.Auction.Model;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestTask.OnlineContract.Hubs;

namespace TestTask.OnlineContract.Concrete
{
    public class SrClientHelper: IClientHelper
    {
        IHubContext _Context = GlobalHost.ConnectionManager.GetHubContext<AuctionHub>();


        public void OnStartAuction(Auction auction)
        {
            _Context.Clients.All.startAuction(auction.Price);
        }

        public void OnNewOffer(int money)
        {
            _Context.Clients.All.changePrice(money);
        }

        public void OnStopAuction(Auction auction)
        {
            EndAuction(auction);
        }

        public void OnEndAuction(Auction auction)
        {
            EndAuction(auction);
        }

        private void EndAuction(Auction auction)
        {
            if ((auction.State & AuctionState.Positive) == AuctionState.Positive)
            {
                _Context.Clients.AllExcept(auction.Winner.Name).announceTheWinner(auction.Price, auction.Winner.Name);
                _Context.Clients.Client(auction.Winner.Name).cangratulate(auction.Price);
            }
            else
            {
                _Context.Clients.All.stopAuction(auction.State);
            }
        }
    }
}