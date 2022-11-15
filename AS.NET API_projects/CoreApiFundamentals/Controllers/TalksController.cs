using AutoMapper;
using CoreCodeCamp.Data;
using CoreCodeCamp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreCodeCamp.Controllers
{
    [Route("api/Camps/{moniker}/[controller]")]
    [ApiController]
    public class TalksController : ControllerBase
    {
        private readonly IMapper _Mapper;
        private readonly ICampRepository _repository;

        public TalksController(IMapper autoMapper,ICampRepository icamprepository )
        {
            this._Mapper = autoMapper;
            this._repository = icamprepository;
        }
        [HttpGet]
        public async Task<ActionResult<Talk[]>> Get(string moniker)
        {
            try
            {
                var result = await _repository.GetTalksByMonikerAsync(moniker);
                return Ok(result);
            }
            catch (System.Exception)
            {
                return StatusCode(404, "internal error");
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Talk>> Get(string moniker,int id)
        {
            try
            {
                var result = await _repository.GetTalkByMonikerAsync(moniker,id);
                return Ok(result);
            }
            catch (System.Exception)
            {
                return StatusCode(404, "internal error");
            }
        }
        [HttpPost]
        public async Task<ActionResult<TalkModel>> Post(string moniker, TalkModel model)
        {
            try
            {
                var camp = await _repository.GetCampAsync(moniker);
                Talk talk=  _Mapper.Map<Talk>(model);
                talk.Camp = camp;
                _repository.Add<Talk>(talk);
                if (await _repository.SaveChangesAsync())
                {
                    return Ok(model);
                }
                return BadRequest();
            }
            catch (System.Exception)
            {
                return StatusCode(404, "internal error");
            }
        }
        //[HttpGet("{id:int}")]
        //public async Task<ActionResult<Talk>> Get(string moniker, int id)
        //{
        //    try
        //    {
        //        var result = await _repository.GetTalkByMonikerAsync(moniker, id);
        //        return Ok(result);
        //    }
        //    catch (System.Exception)
        //    {
        //        return StatusCode(404, "internal error");
        //    }
        //}

    }
}
