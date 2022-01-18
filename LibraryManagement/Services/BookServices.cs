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
                var result = await Collection.DeleteOneAsync(book => book.Id == BookToBeDeleted.Id);
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
                return await Collection.Find(_ => true).ToListAsync();
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
                Console.WriteLine(NewBook.Id);
               return await GetBookById(NewBook);
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
                var result = await GetBookById(BookToBeUpdated);
                if(result != null)
                {
                    await Collection.ReplaceOneAsync(book => book.Id == BookToBeUpdated.Id, BookToBeUpdated);
                }
                else
                {
                    await InsertNewBook(BookToBeUpdated);
                }
                return BookToBeUpdated;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            throw new NotImplementedException();
        }

        public async Task<Book> GetBookById(Book BookById)
        {
            try
            {
                var result = await Collection.FindAsync(book => book.Id == BookById.Id);
                return result.FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            throw new NotImplementedException();
        }
    }
}
