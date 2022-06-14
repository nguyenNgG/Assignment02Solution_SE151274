using BusinessObject;
using DataAccess.Repositories.Interfaces;
using eBookStoreAPI.Constants;
using eBookStoreAPI.Models;
using eBookStoreAPI.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eUserStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ODataController
    {
        IUserRepository repository;
        IConfiguration configuration;

        public UsersController(IUserRepository _repository, IConfiguration _configuration)
        {
            repository = _repository;
            configuration = _configuration;
        }

        [EnableQuery(MaxExpansionDepth = 5)]
        [HttpGet]
        public async Task<ActionResult<List<User>>> Get()
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
        public async Task<ActionResult<User>> GetUser([FromODataUri] int key)
        {
            var obj = await repository.Get(key);
            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }

        [HttpPost]
        public async Task<ActionResult<User>> Post(User obj)
        {
            try
            {
                await repository.Add(obj);
                return Created(obj);
            }
            catch (DbUpdateException)
            {
                if (await repository.Get(obj.UserId) != null)
                {
                    return Conflict();
                }
                return BadRequest();
            }
        }

        [HttpPut("{key}")]
        public async Task<ActionResult<User>> Put(int key, User obj)
        {
            if (key != obj.UserId)
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
                if (await repository.Get(obj.UserId) == null)
                {
                    return NotFound();
                }
                return BadRequest();
            }
        }

        [HttpDelete("{key}")]
        public async Task<ActionResult<User>> Delete(int key)
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

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginForm loginForm)
        {
            if (loginForm == null)
            {
                return BadRequest();
            }

            if (SessionHelper.GetFromSession<UserAuthentication>(HttpContext.Session, SessionValue.Authentication) != null)
            {
                return BadRequest();
            }

            string adminEmail = configuration.GetValue<string>("Admin:Email");
            string adminPassword = configuration.GetValue<string>("Admin:Password");

            bool isAdmin = (loginForm.Email == adminEmail && loginForm.Password == adminPassword);

            User user = null!;

            if (isAdmin)
            {
                user = new User
                {
                    EmailAddress = adminEmail,
                    UserId = -933901,
                };
            }
            else
            {
                user = await repository.Login(loginForm.Email, loginForm.Password);
            }

            if (user != null)
            {
                var auth = new UserAuthentication
                {
                    Email = user.EmailAddress,
                    UserId = user.UserId,
                    IsAdmin = isAdmin,
                };

                SessionHelper.SaveToSession<UserAuthentication>(HttpContext.Session, auth, SessionValue.Authentication);

                return Ok(auth);
            }
            return BadRequest();
        }

        [HttpGet("cart")]
        public ActionResult GetCart()
        {
            Cart cart = SessionHelper.GetFromSession<Cart>(HttpContext.Session, SessionValue.Cart);
            if (cart == null)
            {
                return BadRequest();
            }
            return Ok(cart);
        }

        [HttpPost("cart")]
        public ActionResult SetCart(Cart cart)
        {
            if (cart == null)
            {
                return BadRequest();
            }
            SessionHelper.SaveToSession<Cart>(HttpContext.Session, cart, SessionValue.Cart);
            return Ok(cart);
        }

        [HttpGet("authorize")]
        public ActionResult Authorize()
        {
            var auth = SessionHelper.GetFromSession<UserAuthentication>(HttpContext.Session, SessionValue.Authentication);
            if (auth != null && auth.IsAdmin)
            {
                return Ok(auth);
            }
            return BadRequest();
        }

        [HttpGet("authenticate")]
        public ActionResult Authenticate()
        {
            var auth = SessionHelper.GetFromSession<UserAuthentication>(HttpContext.Session, SessionValue.Authentication);
            if (auth != null)
            {
                return Ok(auth);
            }
            return BadRequest();
        }

        [HttpGet("current")]
        public ActionResult Current()
        {
            var auth = SessionHelper.GetFromSession<UserAuthentication>(HttpContext.Session, SessionValue.Authentication);
            if (auth != null)
            {
                return Ok(auth.UserId);
            }
            return BadRequest();
        }
    }
}
