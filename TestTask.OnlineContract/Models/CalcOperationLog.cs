using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestTask.OnlineContract.Models
{
    public class CalcOperationLog
    {
        public Guid Id { get; set; }
        public double FirstOperand { get; set; }
        public double SecondOperand { get; set; }
        public double Result { get; set; }
        public string Operation { get; set; }
        public string ClientIp { get; set; }
    }
}