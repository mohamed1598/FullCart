using AutoMapper;
using FullCart.API.Dtos;
using FullCart.API.Dtos.InputModels;
using FullCart.Core.Consts;
using FullCart.Core.Entities;
using FullCart.Core.Interfaces;
using FullCart.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FullCart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = AppRoles.Admin)]
    public class BrandsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BrandsController(IUnitOfWork unitOfWork
            , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<BrandDto>>> GetBrands()
        {
            var brands = await _unitOfWork.Repository<Brand>().GetAllAsync();
            var brandsDto = _mapper.Map<IReadOnlyList<BrandDto>>(brands);
            return Ok(brandsDto);
        }
        [HttpPost, DisableRequestSizeLimit]
        public async Task<ActionResult<Brand>> CreateBrand([FromForm] BrandInputModel brandInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            string pictureUrl = string.Empty;
            if (brandInput.Picture != null)
                pictureUrl = await FileService.UploadFile(brandInput.Picture);
            var brand = _mapper.Map<Brand>(brandInput);
            brand.PictureUrl = pictureUrl;
            _unitOfWork.Repository<Brand>().Add(brand);
            await _unitOfWork.Complete();
            var brandDto = _mapper.Map<BrandDto>(brand);
            return Ok(brandDto);
        }
        [HttpPut]
        public async Task<ActionResult<Brand>> UpdateBrand([FromForm] BrandInputModel brandInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (brandInput.Id is null) return BadRequest();
            string pictureUrl = string.Empty;
            var oldPicture = _unitOfWork.Repository<Brand>().GetByIdAsNoTrackingAsync((int)brandInput.Id!).Result!.PictureUrl;
            if (brandInput.Picture != null)
            {
                FileService.DeleteFile(oldPicture);
                pictureUrl = await FileService.UploadFile(brandInput.Picture);
            }
            else if (brandInput.RemoveImage != true)
                pictureUrl = oldPicture;
            else
                FileService.DeleteFile(oldPicture);
            var brand = _mapper.Map<Brand>(brandInput);
            brand.PictureUrl = pictureUrl;
            _unitOfWork.Repository<Brand>().Update(brand);
            await _unitOfWork.Complete();
            var brandDto = _mapper.Map<BrandDto>(brand);
            return Ok(brandDto);
        }
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBrand(int id)
        {
            var brand = await _unitOfWork.Repository<Brand>().GetByIdAsync(id);
            if (brand == null) return BadRequest(false);
            if (brand.PictureUrl != string.Empty)
            {
                FileService.DeleteFile(brand.PictureUrl);
            }
            _unitOfWork.Repository<Brand>().Delete(brand);
            await _unitOfWork.Complete();
            return Ok(true);
        }
    }
}