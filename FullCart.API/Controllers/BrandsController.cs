﻿using AutoMapper;
using FullCart.API.Dtos;
using FullCart.API.Dtos.InputModels;
using FullCart.Core.Consts;
using FullCart.Core.Entities;
using FullCart.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FullCart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles =AppRoles.Admin)]
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
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Brand>>> GetBrands()
        {
            var brands = await _unitOfWork.Repository<Brand>().GetAllAsync();
            return Ok(brands);
        }
        [HttpPost, DisableRequestSizeLimit]
        public async Task<ActionResult<Brand>> CreateBrand([FromForm]BrandInputModel brandInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            string pictureUrl = string.Empty;
            if(brandInput.Picture != null) 
                pictureUrl = await UploadFile(brandInput.Picture);
            var brand = _mapper.Map<Brand>(brandInput);
            brand.PictureUrl = pictureUrl;
            _unitOfWork.Repository<Brand>().Add(brand);
            await _unitOfWork.Complete();
            return Ok(brand);
        }
        [HttpPut]
        public async Task<ActionResult<Brand>> UpdateBrand(BrandDto brandDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var brand = await _unitOfWork.Repository<Brand>().GetByIdAsync(brandDto.Id);
            brand = _mapper.Map<Brand>(brandDto);
            await _unitOfWork.Complete();
            return Ok(brand);
        }
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBrand(int id)
        {
            var brand = await _unitOfWork.Repository<Brand>().GetByIdAsync(id);
            if (brand == null) return BadRequest(false);
            _unitOfWork.Repository<Brand>().Delete(brand);
            await _unitOfWork.Complete();
            return Ok(true);
        }
        // Method to handle file upload
        private async Task<string> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }

            // Generate a unique file name
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            // Define the file path
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

            // Save the file to the server
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Return the file URL
            return "/images/" + fileName;
        }
    }
}