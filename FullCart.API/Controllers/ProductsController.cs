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

namespace FullCart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = AppRoles.Admin)]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductsController(IUnitOfWork unitOfWork
            , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetBrands()
        {
            var products = await _unitOfWork.Repository<Product>().GetAllAsync();
            var productsDto = _mapper.Map<IReadOnlyList<ProductDto>>(products);
            return Ok(productsDto);
        }
        [HttpPost, DisableRequestSizeLimit]
        public async Task<ActionResult<Product>> CreateProduct([FromForm] ProductInputModel productInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            string pictureUrl = string.Empty;
            if (productInput.Picture != null)
                pictureUrl = await FileService.UploadFile(productInput.Picture);
            var product = _mapper.Map<Product>(productInput);
            product.PictureUrl = pictureUrl;
            _unitOfWork.Repository<Product>().Add(product);
            await _unitOfWork.Complete();
            var productDto = _mapper.Map<ProductDto>(product);
            return Ok(productDto);
        }
        [HttpPut]
        public async Task<ActionResult<Product>> UpdateProduct([FromForm] ProductInputModel productInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (productInput.Id is null) return BadRequest();
            string pictureUrl = string.Empty;
            var oldPicture = _unitOfWork.Repository<Product>().GetByIdAsNoTrackingAsync((int)productInput.Id!).Result!.PictureUrl;
            if (productInput.Picture != null)
            {
                FileService.DeleteFile(oldPicture);
                pictureUrl = await FileService.UploadFile(productInput.Picture);
            }
            else if (productInput.RemoveImage != true)
                pictureUrl = oldPicture;
            else
                FileService.DeleteFile(oldPicture);
            var product = _mapper.Map<Product>(productInput);
            product.PictureUrl = pictureUrl;
            _unitOfWork.Repository<Product>().Update(product);
            await _unitOfWork.Complete();
            var productDto = _mapper.Map<ProductDto>(product);
            return Ok(productDto);
        }
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteProduct(int id)
        {
            var product = await _unitOfWork.Repository<Product>().GetByIdAsync(id);
            if (product == null) return BadRequest(false);
            if (product.PictureUrl != string.Empty)
            {
                FileService.DeleteFile(product.PictureUrl);
            }
            _unitOfWork.Repository<Product>().Delete(product);
            await _unitOfWork.Complete();
            return Ok(true);
        }
    }
}