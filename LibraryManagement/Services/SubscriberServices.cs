using LibraryManagement.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Services
{
    public class SubscriberServices : ISubscriberServices
    {
        public String ConnectionString = "mongodb://localhost:27017";
        public String DatabaseName = "Library_Management";
        public String CollectionName = "Subscribers";
        public IMongoDatabase DataBase;
        private IMongoCollection<Subscribers> Collection;
        readonly MongoClient Client;
        public SubscriberServices()
        {
            try
            {
                Client = new MongoClient(ConnectionString);
                DataBase = Client.GetDatabase(DatabaseName);
                Collection = DataBase.GetCollection<Subscribers>(CollectionName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<Subscribers> AddNewSubscriber(Subscribers NewSubscriber)
        {
            try
            {
                    await Collection.InsertOneAsync(NewSubscriber);
                    return NewSubscriber;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        //Add new Subscriber
        public async Task<Subscribers> DeleteSubscriber(Subscribers SubscriberToBeDeleted)
        {
            try
            {
                await Collection.DeleteOneAsync(subs => subs.CustomerId == SubscriberToBeDeleted.CustomerId);
                return SubscriberToBeDeleted;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        //Delete Subscriber
        public async Task<List<Subscribers>> GetAllSubscribers()
        {
            try
            {
                var result = await Collection.Find(_ => true).Limit(100).ToListAsync();
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        //Get All Subscibers
        public async Task<Subscribers> GetSubscribersByBsonId(Subscribers subscriber)
        {
            try
            {
                var result = await Collection.FindAsync(subs => subs.CustomerId == subscriber.CustomerId);
                return result.First(); 
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        //Get Subscriber by Bson ID
        public async Task<List<Subscribers>> GetSubscribersByName(string Name)
        {
            try
            {
                var result = await Collection.Find(subs => subs.Name == Name).ToListAsync();
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        //Get Subscriber by name
        public async Task<Subscribers> UpdateSubscriber(Subscribers SubscriberToBeUpdated)
        {
            try
            {
                await Collection.ReplaceOneAsync(Subs => Subs.CustomerId == SubscriberToBeUpdated.CustomerId, SubscriberToBeUpdated);
                return SubscriberToBeUpdated;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            
        }
        //Update Scriber
    }
}
