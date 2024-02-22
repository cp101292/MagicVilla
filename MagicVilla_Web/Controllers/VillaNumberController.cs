using AutoMapper;
using MagicVilla_Utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Models.VM;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MagicVilla_Web.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly IVillaNumberService _villaNumberService;
        private readonly IVillaService _villaService;
        private readonly IMapper _mapper;

        public VillaNumberController(IVillaNumberService villaNumberService, IVillaService villaService, IMapper mapper)
        {
            _villaService = villaService;
            _villaNumberService = villaNumberService;
            _mapper = mapper;
        }

        public async Task<IActionResult> IndexVillaNumber()
        {
            List<VillaNumberDto> villaList = new();
            var response = await _villaNumberService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));

            if (response != null && response.IsSuccess)
            {
                villaList = JsonConvert.DeserializeObject<List<VillaNumberDto>>(Convert.ToString(response.Result));
            }

            return View(villaList);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateVillaNumber()
        {
            VillaNumberCreateVM villaNumberVM = new();
            var response = await _villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response is { IsSuccess: true, Result: { } })
            {
                villaNumberVM.VillaList = JsonConvert.DeserializeObject<List<VillaDto>>
                (Convert.ToString(response.Result)).Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()

                });
            }


            return View(villaNumberVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateVillaNumber(VillaNumberCreateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _villaNumberService.CreateAsync<APIResponse>(model.VillaNumber, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null)
                {
                    if(response.IsSuccess == true )
                    {
                        TempData["success"] = "Villa created successfully";
                        return RedirectToAction(nameof(IndexVillaNumber));
                    }
                    else if (response.ErrorMessages.Count > 0)
                    {
                        string errorMsg = response.ErrorMessages.FirstOrDefault();
                        TempData["error"] = "Error in creating villa : " + errorMsg;
                        ModelState.AddModelError("ErrorMessage", errorMsg);
                    }
                }
            }

            var resp = await _villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (resp is { IsSuccess: true, Result: { } })
            {
                model.VillaList = JsonConvert.DeserializeObject<List<VillaDto>>
                    (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()

                    });
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateVillaNumber(int villaNumber)
        {
            VillaNumberUpdateVM villaNumberVM = new();
            var response = await _villaNumberService.GetAsync<APIResponse>(villaNumber, HttpContext.Session.GetString(SD.SessionToken));

            if (response != null && response.IsSuccess)
            {
                var model = JsonConvert.DeserializeObject<VillaNumberDto>(Convert.ToString(response.Result));
                villaNumberVM.VillaNumber =  _mapper.Map<VillaNumberUpdateDto>(model);
            }

            response = await _villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response is { IsSuccess: true, Result: { } })
            {
                villaNumberVM.VillaList = JsonConvert.DeserializeObject<List<VillaDto>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()

                });
                return View(villaNumberVM);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateVillaNumber(VillaNumberUpdateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _villaNumberService.UpdateAsync<APIResponse>(model.VillaNumber, HttpContext.Session.GetString(SD.SessionToken));

                if (response != null)
                {
                    if (response.IsSuccess == true)
                    {
                        TempData["success"] = "Villa Updated successfully";
                        return RedirectToAction(nameof(IndexVillaNumber));
                    }
                    else if (response.ErrorMessages.Count > 0)
                    {
                        string errorMsg = response.ErrorMessages.FirstOrDefault();
                        TempData["error"] = "Error in updating villa : " +errorMsg;
                        ModelState.AddModelError("ErrorMessage", errorMsg);
                    }
                }
            }

            var resp = await _villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (resp is { IsSuccess: true, Result: { } })
            {
                model.VillaList = JsonConvert.DeserializeObject<List<VillaDto>>
                    (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()

                });
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteVillaNumber(int villaNumber)
        {
            VillaNumberDeleteVM villaNumberVM = new();
            var response = await _villaNumberService.GetAsync<APIResponse>(villaNumber, HttpContext.Session.GetString(SD.SessionToken));

            if (response != null && response.IsSuccess)
            {
                villaNumberVM.VillaNumber = JsonConvert.DeserializeObject<VillaNumberDto>(Convert.ToString(response.Result));
            }

            response = await _villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response is { IsSuccess: true, Result: { } })
            {
                villaNumberVM.VillaList = JsonConvert.DeserializeObject<List<VillaDto>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()

                });
                return View(villaNumberVM);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteVillaNumber(VillaNumberDeleteVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _villaNumberService.DeleteAsync<APIResponse>(model.VillaNumber.VillaNo, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Villa Deleted successfully";
                    return RedirectToAction(nameof(IndexVillaNumber));
                }
            }
            TempData["error"] = "Error in deleting villa";
            return View(model);
        }

    }
}
