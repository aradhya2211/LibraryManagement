using LibraryManagement.Models;
using LibraryManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        //Constructor
        public BookServices BookService = new BookServices();
        [HttpGet]
        //Get all Books
        public async Task<ActionResult> GetBooksAsync()
        {
            try
            {
                var results = await BookService.GetAllBooks();
                return Ok(results);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return NotFound(e.Message);
            }
        }
        [HttpGet("/{Name}")]
        //Get Book by Name
        public async Task<ActionResult> GetBookByName(String Name)
        {
            try
            {
                var results = await BookService.GetBookByName(Name);
                return Ok(results);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return NotFound(e.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> InsertBook(Book NewBook)
        {
            try
            {
                var result = await BookService.InsertNewBook(NewBook);
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
                throw;
            }
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteBook(Book BookToBeDeleted)
        {
            try
            {
                var DeletedBook = await BookService.DeleteBook(BookToBeDeleted);
                return Ok(DeletedBook);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                throw;
            }
        }
        [HttpPut]
        public async Task<ActionResult> UpdateBook(Book BookToBeUpdated)
        {
            try
            {
                var UpdatedBook = await BookService.UpdateBook(BookToBeUpdated);
                return Ok(UpdatedBook);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                throw;
            }
        }
    }
}
