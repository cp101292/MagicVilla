using System.Net;
using AutoMapper;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using MagicVilla_VillaAPI.Repository;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("api/VillaNumberAPI")]
    [ApiController] // Provide build in support for the data annotation.
    public class VillaNumberAPIController : Controller
    {
        //private readonly ILogger<VillaAPIController> _logger;
        private readonly IMapper _mapper;
        private readonly IVillaNumberRepository _dbVillaNumber;
        private readonly IVillaRepository _dbVilla;
        protected APIResponse _response;

        //Using our own logger 
        public VillaNumberAPIController(IVillaNumberRepository dbVillaNumber, IMapper mapper, IVillaRepository dbVilla)
        {
            _dbVillaNumber = dbVillaNumber;
            _dbVilla = dbVilla;
            _mapper = mapper;
            this._response = new();
        }

        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetVillas()
        {
            try
            {
                IEnumerable<VillaNumber> villaNumberList = await _dbVillaNumber.GetAllAsync();
                _response.Result = _mapper.Map<List<VillaNumberDto>>(villaNumberList);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.Message };
            }

            return _response;
        }

        [HttpGet("{id:int}", Name = "GetVillaNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetVillaNumber(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var villaNumber = await _dbVillaNumber.GetAsync(x => x.VillaNo == id);
                if (villaNumber == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.Result = _mapper.Map<VillaNumberDto>(villaNumber);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.Message };
            }

            return _response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> CreateVillaNumber([FromBody] VillaNumberCreateDto createDto)
        {
            try
            {
                if (await _dbVillaNumber.GetAsync(u => u.VillaNo == createDto.VillaNo) != null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new() { "VillaNumber already exists" };
                    return BadRequest(_response);
                }

                if (await _dbVilla.GetAsync(u => u.Id == createDto.VillaID) == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new() { "VillaID is invalid" };
                    return BadRequest(_response);
                }
                
                VillaNumber villaNumber = _mapper.Map<VillaNumber>(createDto);

                await _dbVillaNumber.CreateAsync(villaNumber);
                _response.Result = _mapper.Map<VillaNumberDto>(villaNumber);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;

                return CreatedAtRoute("GetVillaNumber", new { id = villaNumber.VillaNo }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new() { ex.Message };
            }

            return _response;


        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}", Name = "DeleteVillaNumber")]
        public async Task<ActionResult<APIResponse>> DeleteVillaNumber(int id) // Reason behind using IActionResult is that we do not have to return anything here.
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var villaNumber = await _dbVillaNumber.GetAsync(u => u.VillaNo == id);
                if (villaNumber == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                await _dbVillaNumber.Remove(villaNumber);

                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;

                return Ok(_response); // There is nothing wrong returning Ok() here but when we delete we usually do not return any content.
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.Message };
            }

            return _response;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id:int}", Name = "UpdateVillaNumber")]
        public async Task<ActionResult<APIResponse>> UpdateVillaNumber(int id, [FromBody] VillaNumberUpdateDto villaNumberUpdateDto)
        {
            try
            {
                if (villaNumberUpdateDto.VillaNo != id)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                if (await _dbVilla.GetAsync(u => u.Id == villaNumberUpdateDto.VillaID) == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new() { "Villa with the given villaID does not exists" };
                    return BadRequest(_response);
                }

                VillaNumber villa = _mapper.Map<VillaNumber>(villaNumberUpdateDto);


                await _dbVillaNumber.UpdateAsync(villa);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.Message };
            }

            return _response;
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialVillaNumber")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialVillaNumber(int id, JsonPatchDocument<VillaNumberUpdateDto> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }
            var villaNumber = await _dbVillaNumber.GetAsync(u => u.VillaNo == id, tracked: false);

            VillaNumberUpdateDto villaNumberDTO = _mapper.Map<VillaNumberUpdateDto>(villaNumber);


            if (villaNumber == null)
            {
                return BadRequest();
            }
            patchDTO.ApplyTo(villaNumberDTO, ModelState);
            VillaNumber model = _mapper.Map<VillaNumber>(villaNumberDTO);

            await _dbVillaNumber.UpdateAsync(model);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}
