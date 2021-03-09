using EagleEye.Movie.DataAccess;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EagleEye.Movie.Api.Controllers
{
    [ApiController]
    [Route("movie")]
    public class MovieController : Controller
    {
        private readonly IAccessDb accessDb;

        public MovieController(IAccessDb accessDb)
        {
            this.accessDb = accessDb;
        }

        [HttpGet]
        [Route("stats")]
        public async Task<IActionResult> Get()
        {
            var response = await accessDb.GetStats();
            return Ok(response);
        }
    }
}
