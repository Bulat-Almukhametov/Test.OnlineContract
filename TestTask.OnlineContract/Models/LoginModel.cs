using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestTask.OnlineContract.Models
{
    public class LoginModel
    {
        public string Login { get; set; }
        public string HashValue { get; set; }
        public Guid BigNumber { get; set; }
    }
}