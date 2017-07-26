using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Auction.Abstract;
using Moq;
using Domain.Auction.Model;
using Domain.Auction.Exceptions;
using System.Threading;
using Domain.Auction.Enum;

namespace TestTask.OnlineContract.Tests
{
    [TestClass]
    public class SalesTest
    {
        Sailer _Sailer;

        [TestInitialize]
        public void Initialize()
        {
            _Sailer = new Mock<Sailer>(new Product()).Object;
        }
    
        

        [TestMethod]
        public void TestAuctionStart()
        {
            _Sailer.AuctionDuringTime = 5;
            int eps = 1;
            _Sailer.StartAuction();
            
            Assert.AreEqual(_Sailer.Auction.Price, 100, "Начальная цена товара 100 руб.");

            var prevAuction = _Sailer.Auction;
            Assert.AreEqual(AuctionState.Active, prevAuction.State & AuctionState.Active, "Неверный статус аукциона.");
            Thread.Sleep((_Sailer.AuctionDuringTime + eps) * 1000);
            Assert.AreEqual(AuctionState.Stopped, prevAuction.State & AuctionState.Stopped, "Аукцион не завершился через указанное время.");
            _Sailer.Stop();
        }

        [TestMethod]
        public void TestSales()
        {
            _Sailer.StartAuction();
            var auction = _Sailer.Auction;

            var user = new User()
            {
                Name = "John",
                Money = 400
            };

            _Sailer.MakeOffer(user, 105);
            try
            {
                _Sailer.MakeOffer(user, 4);
                Assert.Fail("Увеличить цену можно только на сумму не менее 5 руб.");
            }
            catch (AuctionRuleException ex)
            {

            }
            _Sailer.Stop();

            Assert.IsTrue(auction != null && auction.Winner != null && auction.Winner.Name == "John",
                "");
        }

        
    }
}
