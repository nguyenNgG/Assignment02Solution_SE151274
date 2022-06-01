using BusinessObject;
using DataAccess.Repositories.Interfaces;
using eBookStoreAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eBookStoreAPI.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    [ODataAttributeRouting]
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
        [HttpGet("{key}")]
        public async Task<ActionResult<Book>> GetBook([FromODataUri] int key)
        {
            return await repository.GetBook(key);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> Post(Book book)
        {
            await repository.AddBook(book);
            return Created(book);
        }

        [EnableQuery]
        [HttpPost("odata/login")]
        public async Task<ActionResult<Book>> PostLogin([FromBody]LoginForm loginForm)
        {
            var pwd = loginForm.Password;
            return Ok();
        }
    }

}
