using BusinessObject;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Collections.Generic;

namespace eBookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ODataController
    {
        IRoleRepository repository;
        public RolesController(IRoleRepository _repository)
        {
            repository = _repository;
        }

        [EnableQuery(MaxExpansionDepth = 5)]
        [HttpGet]
        public ActionResult<List<Role>> Get()
        {
            var list = repository.GetList();
            if (list == null)
            {
                return NotFound();
            }
            return Ok(list);
        }
    }
}
