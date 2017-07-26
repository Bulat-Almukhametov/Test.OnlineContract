using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Auction.Model;

namespace Domain.Auction.Concrete
{
    public class DealVerificator
    {
        public bool CheckUser(User user, int money)
        {
            return user.Money > money;
        }
    }
}
