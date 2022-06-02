using BusinessObject;
using DataAccess.Repositories.Interfaces;
using eBookStoreAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text.Json;
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
            var list = await repository.GetList();
            if (list == null)
            {
                return NotFound();
            }
            return Ok(list);
        }

        [EnableQuery]
        [HttpGet("{key}")]
        public async Task<ActionResult<Book>> GetBook([FromODataUri] int key)
        {
            var obj = await repository.Get(key);
            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> Post(Book book)
        {
            try
            {
                await repository.Add(book);
                return Created(book);
            }
            catch (DbUpdateException)
            {
                if (await repository.Get(book.BookId) != null)
                {
                    return Conflict();
                }
                return BadRequest();
            }
        }

        [HttpPut("{key}")]
        public async Task<ActionResult<Book>> Put(int key, Book book)
        {
            if (key != book.BookId)
            {
                return BadRequest();
            }

            try
            {
                await repository.Update(book);
                return Ok(book);
            }
            catch (DbUpdateException)
            {
                if (await repository.Get(book.BookId) == null)
                {
                    return NotFound();
                }
                return BadRequest();
            }
        }

        [HttpDelete("{key}")]
        public async Task<ActionResult<Book>> Put(int key)
        {
            try
            {
                await repository.Delete(key);
                return NoContent();
            }
            catch (DbUpdateException)
            {
                if (await repository.Get(key) == null)
                {
                    return NotFound();
                }
                return BadRequest();
            }
        }
    }
}
