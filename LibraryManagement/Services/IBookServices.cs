using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Services
{
    interface IBookServices
    {
        public Task<List<Book>> GetAllBooks();
        public Task<Book> InsertNewBook(Book NewBook);
        public Task<Book> DeleteBook(Book BookToBeDeleted);
        public Task<List<Book>> GetBookByName(String Name);
        public Task<Book> UpdateBook(Book BookToBeUpdated);
        public Task<Book> GetBookById(Book BookById);
        public Task<List<Book>> GetBooksBySubscriber(String CustomerId);
    }
}
