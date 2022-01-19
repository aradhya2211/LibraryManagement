using LibraryManagement.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Services
{
    public class BookServices : IBookServices
    {
        public String ConnectionString = "mongodb://localhost:27017";
        public String DatabaseName = "Library_Management";
        public String CollectionName = "Books";
        public IMongoDatabase DataBase;
        private IMongoCollection<Book> Collection;
        readonly MongoClient Client;

        public BookServices()
        {
            try
            {

                Client = new MongoClient(ConnectionString);
                DataBase = Client.GetDatabase(DatabaseName);
                Collection = DataBase.GetCollection<Book>(CollectionName);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        } 

        public async Task<Book> DeleteBook(Book BookToBeDeleted)
        {
            try
            {
                var result = await Collection.DeleteOneAsync(book => book._id == BookToBeDeleted._id);
                return BookToBeDeleted;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            throw new NotImplementedException();
        }

        public async Task<List<Book>> GetAllBooks()
        {
            try
            {
                return await Collection.Find(_ => true).Limit(100).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<List<Book>> GetBookByName(string Name)
        {
            try
            {
                return await Collection.Find(book => book.Name.Contains(Name)).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            throw new NotImplementedException();
        }

        public async Task<Book> InsertNewBook(Book NewBook)
        {
            try
            {
               await Collection.InsertOneAsync(NewBook);
               return await GetBookById(NewBook._id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<Book> UpdateBook(Book BookToBeUpdated)
        {
            try
            {
                await Collection.ReplaceOneAsync(book => book._id == BookToBeUpdated._id, BookToBeUpdated);
                return await GetBookById(BookToBeUpdated._id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            throw new NotImplementedException();
        }

        public async Task<Book> GetBookById(String BookId)
        {
            try
            {
                var result = await Collection.FindAsync(book => book._id == BookId);
                return result.FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            throw new NotImplementedException();
        }

        public async Task<List<Book>> GetBooksBySubscriber(String CustomerId)
        {
            try
            {
                var result = await Collection.Find(book => book.Issuers
                .Where(IssueDetails => IssueDetails.CustomerID == CustomerId) != null)
                    .ToListAsync();
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<Book> Subscribe(String BookId, IssuerDetails Issuer)
        {
            try
            {
                Issuer.SubscriptionDate = DateTime.Now;
                Issuer.DateOfReturn = Issuer.SubscriptionDate.AddDays(Issuer.SubscriptionDuration);
                var BookToBeSubscribed = await GetBookById(BookId);
                if(BookToBeSubscribed.Issuers == null)
                {
                    List<IssuerDetails> NewIssuerList = new List<IssuerDetails>();
                    NewIssuerList.Add(Issuer);
                    BookToBeSubscribed.Issuers = NewIssuerList;
                }
                else
                {
                    BookToBeSubscribed.Issuers.Add(Issuer);
                }
                var result = await Collection.ReplaceOneAsync(book => book._id == BookId, BookToBeSubscribed);
                return BookToBeSubscribed;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}
