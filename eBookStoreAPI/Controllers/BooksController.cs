using BusinessObject;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eBookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ODataController
    {
        IBookRepository repository;
        public BooksController(IBookRepository _repository)
        {
            repository = _repository;
        }

        [EnableQuery(MaxExpansionDepth = 5)]
        [HttpGet]
        public async Task<ActionResult<List<Book>>> Get()
        {
            return await repository.GetBooks(null);
        }

        [EnableQuery]
        [HttpGet("id")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            return await repository.GetBook(id);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> Post(Book book)
        {
            await repository.AddBook(book);
            return Created(book);
        }
    }
}
