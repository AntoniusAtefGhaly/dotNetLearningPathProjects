using AutoMapper;
using CoreCodeCamp.Data;
using CoreCodeCamp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCodeCamp.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CampsController : ControllerBase
    {
        private readonly ICampRepository repository;
        private readonly IMapper mapper;

        public CampsController(ICampRepository repository,IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        //[Route("api/[Controller]")]
        [HttpGet]
        public async Task< ActionResult<CampModel[]>> Get(bool Talks)
        {
            try
            {
                var result = await repository.GetAllCampsAsync(Talks);
                return mapper.Map<CampModel[]>(result);
                //return campModels;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                //return BadRequest(ex.Message);
            }
        }
        [Route("api/[Controller]")]
        [HttpGet("{moniker}")]
        public async Task<ActionResult<CampModel>> Get(string moniker)
        {
            try
            {
                var result = await repository.GetCampAsync(moniker);
                CampModel campModels = mapper.Map<CampModel>(result);
                return campModels;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                //return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CampModel>> ActionResult(CampModel model)
        {
            return Ok();

        }
    }
}