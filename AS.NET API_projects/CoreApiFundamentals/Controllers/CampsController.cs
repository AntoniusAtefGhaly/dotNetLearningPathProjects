using AutoMapper;
using CoreCodeCamp.Data;
using CoreCodeCamp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCodeCamp.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    public class CampsController : ControllerBase
    {
        private readonly ICampRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        public CampsController(ICampRepository repository, IMapper mapper, LinkGenerator linkGenerator)
        {
            this._repository = repository;
            this._mapper = mapper;
            this._linkGenerator = linkGenerator;
        }
        [HttpGet("{Talks}")]
        public async Task<ActionResult<CampModel[]>> Get(bool Talks)
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

        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<CampModel[]>> Get()
        {
            try
            {
                var result = await _repository.GetAllCampsAsync();
                CampModel[] campModels = _mapper.Map<CampModel[]>(result);
                return campModels;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                //return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [MapToApiVersion("1.1")]
        public async Task<ActionResult<CampModel[]>> Get1()
        {
            try
            {
                var result = await _repository.GetAllCampsAsync();
                CampModel[] campModels = _mapper.Map<CampModel[]>(result);
                return campModels;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                //return BadRequest(ex.Message);
            }
        }



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
        public async Task<ActionResult<CampModel>> Post([FromBody] CampModel model)
        {
            try
            {
                var exising = await _repository.GetCampAsync(model.Moniker);
                if(exising != null)
                    return BadRequest($"the moniker {model.Moniker} is in use");
                var location = _linkGenerator.GetPathByAction("Get", "Camps",
                    new
                    {
                        moniker = model.Moniker
                    });
                Camp camp =_mapper.Map<Camp>(model);
                 _repository.Add(camp);
                await _repository.SaveChangesAsync();
                CampModel res=_mapper.Map<CampModel>(camp);
                return  Created(location,res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            return BadRequest();
        }
        [HttpPut("{moniker}")]
        public async Task<ActionResult<CampModel>> Put(string moniker,[FromBody] CampModel model)
        {
            try
            {
                var oldCamp = await _repository.GetCampAsync(moniker);
                if (oldCamp == null)
                    return NotFound($"can not found Camp with moniker {moniker}");
                _mapper.Map(model, oldCamp);
                if(await _repository.SaveChangesAsync())
                {
                    return _mapper.Map<CampModel>(oldCamp);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            return BadRequest();
        }
        [HttpDelete("{moniker}")]
        public async Task<ActionResult> Delete(string moniker)
        {
            try
            {
                var camp= await _repository.GetCampAsync(moniker);
                if(camp == null)
                    return NotFound($"can not found Camp with moniker {moniker}");
                    _repository.Delete(camp);
                if (await _repository.SaveChangesAsync())
                {
                    return Ok();
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            return BadRequest();
        }
    }
}