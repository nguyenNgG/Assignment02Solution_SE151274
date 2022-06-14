using BusinessObject;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eBookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookAuthorsController : ODataController
    {
        IBookAuthorRepository repository;
        public BookAuthorsController(IBookAuthorRepository _repository)
        {
            repository = _repository;
        }

        [EnableQuery(MaxExpansionDepth = 5)]
        [HttpGet]
        public async Task<ActionResult<List<BookAuthor>>> Get()
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
        public async Task<ActionResult<BookAuthor>> GetBookAuthor([FromODataUri] int keyBookId, [FromODataUri] int keyAuthorId)
        {
            var obj = await repository.Get(keyBookId, keyAuthorId);
            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }

        [HttpPost]
        public async Task<ActionResult<BookAuthor>> Post(BookAuthor obj)
        {
            try
            {
                await repository.Add(obj);
                return Created(obj);
            }
            catch (DbUpdateException)
            {
                if (await repository.Get(obj.BookId.Value, obj.AuthorId.Value) != null)
                {
                    return Conflict();
                }
                return BadRequest();
            }
        }

        [HttpPut("{key}")]
        public async Task<ActionResult<BookAuthor>> Put([FromODataUri] int keyBookId, [FromODataUri] int keyAuthorId, BookAuthor obj)
        {
            if (keyBookId != obj.BookId && keyAuthorId != obj.AuthorId)
            {
                return BadRequest();
            }

            try
            {
                await repository.Update(obj);
                return Ok(obj);
            }
            catch (DbUpdateException)
            {
                if (await repository.Get(obj.BookId.Value, obj.AuthorId.Value) == null)
                {
                    return NotFound();
                }
                return BadRequest();
            }
        }

        [HttpDelete("{key}")]
        public async Task<ActionResult<BookAuthor>> Delete(int keyBookId, int keyAuthorId)
        {
            try
            {
                await repository.Delete(keyBookId, keyAuthorId);
                return NoContent();
            }
            catch (DbUpdateException)
            {
                if (await repository.Get(keyBookId, keyAuthorId) == null)
                {
                    return NotFound();
                }
                return BadRequest();
            }
        }
    }
}
