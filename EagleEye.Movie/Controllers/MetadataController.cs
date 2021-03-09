using EagleEye.Movie.DataAccess;
using EagleEye.Movie.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EagleEye.Movie.Api.Controllers
{
    [ApiController]
    [Route("metadata")]
    public class MetadataController : Controller
    {
        private readonly IAccessDb accessDb;

        public MetadataController(IAccessDb accessDb)
        {
            this.accessDb = accessDb;
        }

        [HttpPost]
        public async Task<IActionResult> Post(MetadataPostViewModel metadata)
        {
            await accessDb.PostMetadata(metadata);
            return Ok();
        }

        [HttpGet]
        [Route("{movieId}")]
        public async Task<IActionResult> Get([Required] int movieId)
        {
            var response = await accessDb.GetMetadata(movieId);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }
    }
}
