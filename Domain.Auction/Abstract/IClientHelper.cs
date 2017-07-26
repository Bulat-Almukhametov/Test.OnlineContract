using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Auction.Model;

namespace Domain.Auction.Abstract
{
    public interface IClientHelper
    {
        void OnStartAuction(Model.Auction auction);
        void OnNewOffer(int money);
        void OnStopAuction(Model.Auction auction);
        void OnEndAuction(Model.Auction auction);
    }
}
