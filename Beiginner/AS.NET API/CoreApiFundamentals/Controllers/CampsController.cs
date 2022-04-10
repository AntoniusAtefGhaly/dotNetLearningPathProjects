using CoreCodeCamp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCodeCamp.Controllers
{
    public class CampsController : ControllerBase
    {
        private readonly ICampRepository repository;

        public CampsController(ICampRepository repository)
        {
            this.repository = repository;
        }
        [Route("api/[Controller]")]
        public async Task< ActionResult> Get()
        {
            try
            {
                var result = await repository.GetAllCampsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                //return BadRequest(ex.Message);
            }
        }
    }
}
