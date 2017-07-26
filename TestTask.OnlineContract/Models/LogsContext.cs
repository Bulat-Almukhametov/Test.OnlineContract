using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TestTask.OnlineContract.Models
{
    public class LogsContext : DbContext
    {
        public DbSet<CalcOperationLog> CalcOperationLogs { get; set; }
    }
}