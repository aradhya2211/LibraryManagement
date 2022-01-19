using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Services
{
    interface ISubscriberServices
    {
        public Task<List<Subscribers>> GetAllSubscribers();
        public Task<Subscribers> AddNewSubscriber(Subscribers NewSubscriber);
        public Task<List<Subscribers>> GetSubscribersByName(String Name);
        public Task<Subscribers> GetSubscribersByBsonId(Subscribers subscriber);
        public Task<Subscribers> DeleteSubscriber(Subscribers SubscriberToBeDeleted);
        public Task<Subscribers> UpdateSubscriber(Subscribers SubscriberToBeUpdated);
    }
}
