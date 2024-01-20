using AutoMapper;
using FullCart.API.Dtos;
using FullCart.Core.Consts;
using FullCart.Core.Entities;
using FullCart.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FullCart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = AppRoles.Admin)]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoriesController(IUnitOfWork unitOfWork
            , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Category>>> GetCategories()
        {
            var categories = await _unitOfWork.Repository<Category>().GetAllAsync();
            return Ok(categories);
        }
        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategory(CategoryDto CategoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var category = _mapper.Map<Category>(CategoryDto);
            _unitOfWork.Repository<Category>().Add(category);
            await _unitOfWork.Complete();
            return Ok(category);
        }
        [HttpPut]
        public async Task<ActionResult<Category>> UpdateCategory(CategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var category = await _unitOfWork.Repository<Category>().GetByIdAsync(categoryDto.Id);
            category = _mapper.Map<Category>(categoryDto);
            await _unitOfWork.Complete();
            return Ok(category);
        }
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteCategory(int id)
        {
            var category = await _unitOfWork.Repository<Category>().GetByIdAsync(id);
            if (category == null) return BadRequest(false);
            _unitOfWork.Repository<Category>().Delete(category);
            await _unitOfWork.Complete();
            return Ok(true);
        }
    }
}
