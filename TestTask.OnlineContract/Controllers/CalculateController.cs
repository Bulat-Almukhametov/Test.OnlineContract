using TestTask.OnlineContract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace TestTask.OnlineContract.Controllers
{
    public class CalculateController : ApiController
    {
        public double PostCalculate(CalcParameters parameters)
        {
            double firstOperand = parameters.firstOperand;
            double secondOperand = parameters.secondOperand;
            string operatorName = parameters.operatorName;

            double result = HandleOperation(firstOperand, secondOperand, operatorName);
            string clientIp = String.Empty;
            try
            {
                clientIp = ((HttpContextWrapper)Request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            }
            catch (Exception ex)
            {

            }

            var db = new LogsContext();
            db.CalcOperationLogs.Add(new CalcOperationLog()
            {
                Id = Guid.NewGuid(),
                ClientIp = clientIp,
                FirstOperand = firstOperand,
                SecondOperand = secondOperand,
                Operation = operatorName,
                Result = result
            });
            db.SaveChanges();

            return result;
        }

        private double HandleOperation(double firstOperand, double secondOperand, string operatorName)
        {
            double result = 0;
            switch (operatorName.ToLower())
            {
                case "add":
                    {
                        result = firstOperand + secondOperand;
                        break;
                    }

                case "subtract":
                    {
                        result = firstOperand - secondOperand;
                        break;
                    }

                case "multiple":
                    {
                        result = firstOperand * secondOperand;
                        break;
                    }

                case "divide":
                    {
                        result = firstOperand / secondOperand;
                        break;
                    }
            }

            return result;
        }

    }
}
