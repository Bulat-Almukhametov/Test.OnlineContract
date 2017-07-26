using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Auction.Enum
{
    public enum AuctionState
    {
        Null = 0,
        Active = 1,
        Stopped = 2,
        Positive = 4,
        Negative = 8,
        Success = Stopped | Positive,
        Failed = Stopped | Negative
    }
}
