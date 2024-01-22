using AutoMapper;
using FullCart.API.Dtos;
using FullCart.API.Dtos.InputModels;
using FullCart.API.Services;
using FullCart.Core.Consts;
using FullCart.Core.Entities;
using FullCart.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace FullCart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = AppRoles.Admin)]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoriesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<CategoryDto>>> GetCategories()
        {
            var categories = await _unitOfWork.Repository<Category>().GetAllAsync();
            var categoriesDto = _mapper.Map<IReadOnlyList<CategoryDto>>(categories);
            return Ok(categoriesDto);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateCategory([FromForm] CategoryInputModel categoryInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            string pictureUrl = string.Empty;
            if (categoryInput.Picture != null)
                pictureUrl = await FileService.UploadFile(categoryInput.Picture);
            var category = _mapper.Map<Category>(categoryInput);
            category.PictureUrl = pictureUrl;
            _unitOfWork.Repository<Category>().Add(category);
            await _unitOfWork.Complete();
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return Ok(categoryDto);
        }

        [HttpPut]
        public async Task<ActionResult<CategoryDto>> UpdateCategory([FromForm] CategoryInputModel categoryInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (categoryInput.Id is null) return BadRequest();
            string pictureUrl = string.Empty;
            var oldPicture = _unitOfWork.Repository<Category>().GetByIdAsNoTrackingAsync((int)categoryInput.Id!).Result!.PictureUrl;
            if (categoryInput.Picture != null)
            {
                FileService.DeleteFile(oldPicture);
                pictureUrl = await FileService.UploadFile(categoryInput.Picture);
            }
            else if (categoryInput.RemoveImage != true)
                pictureUrl = oldPicture;
            else
                FileService.DeleteFile(oldPicture);
            var category = _mapper.Map<Category>(categoryInput);
            category.PictureUrl = pictureUrl;
            _unitOfWork.Repository<Category>().Update(category);
            await _unitOfWork.Complete();
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return Ok(categoryDto);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteCategory(int id)
        {
            var category = await _unitOfWork.Repository<Category>().GetByIdAsync(id);
            if (category == null) return BadRequest(false);
            if (category.PictureUrl != string.Empty)
            {
                FileService.DeleteFile(category.PictureUrl);
            }
            _unitOfWork.Repository<Category>().Delete(category);
            await _unitOfWork.Complete();
            return Ok(true);
        }
    }
}