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
using System.Text.Json;
using System.Threading.Tasks;

namespace eBookStoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
            return await repository.GetList();
        }

        [EnableQuery]
        [HttpGet("{key}")]
        public async Task<ActionResult<Book>> GetBook([FromODataUri] int key)
        {
            return await repository.Get(key);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> Post(Book book)
        {
            await repository.Add(book);
            return Created(book);
        }

        [HttpPut("{key}")]
        public async Task<ActionResult<Book>> Put(int key, Book book)
        {
            var _book = new Book
            {
                PublishedDate = new System.DateTime(book.PublishedDate.Ticks, System.DateTimeKind.Utc)
            };
            string v = JsonSerializer.Serialize(_book);
            await repository.Update(book);
            return Ok(book);
        }

        [EnableQuery]
        [HttpPost("login")]
        public async Task<ActionResult<Book>> Login([FromBody]LoginForm loginForm)
        {
            await repository.GetList();
            var pwd = loginForm.Password;
            return Ok();
        }
    }

}
