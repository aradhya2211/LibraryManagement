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
        //Delete Book Function
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
        //Get all books
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
        //Get Books by name
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
        //Add new book to the library
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
        //Update book info
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
        //Get book by ID
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
        //Get books Subcribers
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
        //Subscribe a book
        public async Task<Book> Subscribe(String BookId, IssuerDetails Issuer)
        {
            try
            {
                //Date of subscription
                Issuer.SubscriptionDate = DateTime.Now;
                //Expected date of return
                Issuer.DateOfReturn = Issuer.SubscriptionDate.AddDays(Issuer.SubscriptionDuration);
                //subscription is alive
                Issuer.IsSubscribed = true;
                var BookToBeSubscribed = await GetBookById(BookId);
                //decrement available copies
                BookToBeSubscribed.Copies--;
                if (BookToBeSubscribed.Issuers == null)
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
        //Return a book
        public async Task<Book> ReturnBook(string BookId, IssuerDetails Issuer)
        {
            Book ReturnedBook;
            IssuerDetails Issue;
            try
            {
                ReturnedBook = await GetBookById(BookId);
                Issue = ReturnedBook.Issuers.Where(iss => iss.CustomerID == Issuer.CustomerID).FirstOrDefault();
                Issue.DateOfReturn = DateTime.Now;
                //Increment number of copies
                ReturnedBook.Copies++;
                //Calculate Fine
                if (Issue.SubscriptionDate.AddDays(Issue.SubscriptionDuration) < Issue.DateOfReturn)
                {
                    Issue.Fine = (Issue.DateOfReturn.Subtract(Issue.SubscriptionDate).TotalDays
                        - Issue.SubscriptionDuration)
                        * ReturnedBook.FinePerDay;
                }
                else
                {
                    Issue.Fine = 0;
                }
                await Collection.ReplaceOneAsync(book => book._id == ReturnedBook._id, ReturnedBook);
                return ReturnedBook;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}
