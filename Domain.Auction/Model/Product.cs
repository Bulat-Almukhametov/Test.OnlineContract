using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Auction.Model
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Product && ((Product)obj).Name == this.Name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
