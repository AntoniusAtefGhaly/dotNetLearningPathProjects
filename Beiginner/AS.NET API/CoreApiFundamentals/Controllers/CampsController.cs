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
        private readonly ICampRepository _repository;
        private readonly IMapper _mapper;

        public CampsController(ICampRepository repository,IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }
        //[Route("api/[Controller]")]
        [HttpGet]
        public async Task< ActionResult<CampModel[]>> Get(bool Talks)
        {
            try
            {
                var result = await _repository.GetAllCampsAsync(Talks);
                return _mapper.Map<CampModel[]>(result);
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
                var result = await _repository.GetCampAsync(moniker);
                CampModel campModels = _mapper.Map<CampModel>(result);
                return campModels;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                //return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CampModel>> ActionResult([FromBody] CampModel model)
        {
            try
            {
                Camp camp =_mapper.Map<Camp>(model);
                 _repository.Add(camp);
                return  Created("",_mapper.Map<CampModel>(camp));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            return BadRequest();
        }
    }
}