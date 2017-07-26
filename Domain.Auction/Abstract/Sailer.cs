using Domain.Auction.Concrete;
using Domain.Auction.Enum;
using Domain.Auction.Exceptions;
using Domain.Auction.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Auction.Abstract
{
    public abstract class Sailer
    {
        Product _SaleProduct;
        static Model.Auction _Auction;
        Thread _AuctionCloser;
        static User _CurrentUser;
        int _MinAddValue = 5;
        int OverTime = 60;
        IClientHelper _ClientHelper;

        DealVerificator _Verificator;

        Mutex _RestTimeMutex = new Mutex();
        private int _RestTime;

        public int AuctionDuringTime = 5 * 60;

        public Sailer(Product product, IClientHelper clientHelper = null)
        {
            _SaleProduct = product;
            _Verificator = new DealVerificator();
            _ClientHelper = clientHelper;
        }

        public Model.Auction Auction
        {
            get { return _Auction; }
        }

        public void StartAuction()
        {
            if (_Auction != null && (_Auction.State & AuctionState.Active) == AuctionState.Active)
            {
                throw new AuctionRuleException("Нельзя начать новый аукцион, пока не закончится предыдущий.");
            }

            _Auction = new Model.Auction()
            {
                Price = 100,
                StartTime = DateTime.Now,
                State = AuctionState.Active
            };
            _RestTime = AuctionDuringTime;

            if (_AuctionCloser == null)
            {
                _AuctionCloser = new Thread(() => CheckAuctionForClosing());
                _AuctionCloser.Start();
            }

            if (_ClientHelper != null)
            {
                _ClientHelper.OnStartAuction(_Auction);
            }
        }

        public void MakeOffer(User user, int money)
        {
            if (Auction == null)
                throw new Exception("Аукцион не начался.");

            bool userCanMakeOffer = _Verificator.CheckUser(user, money);
            if (!userCanMakeOffer)
                throw new AuctionRuleException("Не удается сделать предложение.");

            if (money - _Auction.Price < _MinAddValue)
                throw new AuctionRuleException("Увеличить цену можно только на сумму не менее 5 руб.");

            _Auction.Price = money;
            _CurrentUser = user;

            if (_RestTime <= OverTime)
            {
                _RestTimeMutex.WaitOne();
                _RestTime += OverTime;
                _RestTimeMutex.ReleaseMutex();
            }

            if (_ClientHelper != null)
            {
                _ClientHelper.OnNewOffer(money);
            }
        }

        public void Stop()
        {
            if (_AuctionCloser != null)
            {
                _AuctionCloser.Abort();
            }

            if (_ClientHelper != null)
            {
                _ClientHelper.OnStopAuction(_Auction);
            }
        }

        private void CheckAuctionForClosing()
        {
            while (true)
            {
                do
                {
                    Thread.Sleep(1000);

                    _RestTimeMutex.WaitOne();
                    _RestTime--;
                    _RestTimeMutex.ReleaseMutex();
                }
                while (_RestTime > 0
                && (Auction.State & AuctionState.Active) == AuctionState.Active);
                {
                    EndAuction();
                    StartAuction();
                }
            }
        }

        protected void EndAuction()
        {
            _Auction.EndTime = DateTime.Now;
            if (_CurrentUser != null)
            {
                Auction.State = AuctionState.Success;
                Auction.Winner = _CurrentUser;
                OnSailed();
            }
            else
            {
                _Auction.State = AuctionState.Failed;
            }
            OnEndAuction();

            if (_ClientHelper != null)
            {
                _ClientHelper.OnEndAuction(_Auction);
            }
        }

        protected abstract void OnSailed();
        protected abstract void OnEndAuction();

        ~Sailer()
        {
            if (_AuctionCloser != null && _AuctionCloser.IsAlive)
                _AuctionCloser.Abort();
        }
    }
}
