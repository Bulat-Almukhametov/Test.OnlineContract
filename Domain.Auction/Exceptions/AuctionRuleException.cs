using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Auction.Exceptions
{
    public class AuctionRuleException : Exception
    {
        public AuctionRuleException(string message) : base(message)
        {
        }
    }
}
