using Domain.Auction.Enum;
using Domain.Auction.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Auction.Model
{
    public class Auction
    {
        public Guid Id { get; set; }
        private int _OffersCount;
        readonly int MinAddedValue = 5;

        /// <summary>
        /// Время начала аукциона
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Время завершения аукциона
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Цена продажи
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// Победитель
        /// </summary>
        public User Winner { get; set; }

        public int OffersCount { get; set; }

        public AuctionState State { get; set; }
    }
}
