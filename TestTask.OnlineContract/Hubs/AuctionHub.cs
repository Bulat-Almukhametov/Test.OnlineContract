using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Domain.Auction.Model;
using TestTask.OnlineContract.Models;
using Domain.Auction.Abstract;
using Domain.Auction.Enum;
using Ninject;
using TestTask.OnlineContract.Infrastructure;

namespace TestTask.OnlineContract.Hubs
{
    public class AuctionHub : Hub
    {
        readonly IKernel kerner = new StandardKernel(new SailerSingletonModule());
        readonly AuctionContext _Repository = new AuctionContext();
        readonly Dictionary<string, User> _Users = new Dictionary<string, User>();
        public void AddUser(User user)
        {
            _Users.Add(user.Name, user);
        }

        public void DeleteUser(string name)
        {
            _Users.Remove(name);
        }

        private void ChangeUsers()
        {
            Clients.All.changeUserList(GetUserNames());
        }

        public void Connect()
        {
            var currentUser = Context.User.Identity.Name;
            Clients.Caller.onConnected(currentUser, GetUserNames());

            var sailer = kerner.Get<Sailer>();
            Clients.Caller.changePrice(sailer.Auction.Price);
        }

        private IEnumerable<String> GetUserNames()
        {
            return _Users.Select(u => u.Value.Name);
        }

        private User GetCurrentUser()
        {
            var key = Context.User.Identity.Name;

            //if (_Users.ContainsKey(key))
            //    return _Users[key];
            //else
            //    return null;

            return _Repository.Users.FirstOrDefault(u => u.Name == key);
        }

        public void MakeOffer(int price)
        {
            var user = GetCurrentUser();
            if (user == null) return;

            var sailer = kerner.Get<Sailer>();

            sailer.MakeOffer(user, price);
        }
    }
}