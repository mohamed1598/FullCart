using AutoMapper;
using FullCart.API.Dtos;
using FullCart.API.Dtos.InputModels;
using FullCart.API.Helpers;
using FullCart.Core.Consts;
using FullCart.Core.Entities;
using FullCart.Core.Interfaces;
using FullCart.Core.Specifications;
using FullCart.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

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
        public async Task<ActionResult<Pagination<ProductDto>>> GetProducts([FromQuery] ProductSpecParams productParams)
        {
            var spec = new ProductsWithCategoriesAndBrandsSpecifications(productParams);
            var countSpec = new ProductWithFiltersForCountSpecification(productParams);
            var totalItems = await _unitOfWork.Repository<Product>().CountAsync(countSpec);
            var products = await _unitOfWork.Repository<Product>().ListAsync(spec);
            var data = _mapper.Map<IReadOnlyList<ProductDto>>(products);
            return Ok(new Pagination<ProductDto>(productParams.PageIndex, productParams.PageSize, totalItems, data));
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

        [HttpPost("Import")]
        public async Task<ActionResult<bool>> ImportCategories(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(false);
            }
            await _unitOfWork.Repository<Product>().AddOrUpdateBulk(GetProductsFromExcel(file));
            return Ok(true);
        }
        private IEnumerable<Product> GetProductsFromExcel(IFormFile file)
        {
            using var package = new ExcelPackage(file.OpenReadStream());
            var worksheet = package.Workbook.Worksheets[0];
            List<Product> productList = [];
            for (int row = 2; row <= worksheet.Dimension.Rows; row++)
            {
                productList.Add(new Product
                {
                    Id = int.Parse((worksheet.Cells[row, 1].Value ?? "0").ToString()),
                    Name = (worksheet.Cells[row, 2].Value ?? "").ToString(),
                    Description = (worksheet.Cells[row, 3].Value ?? "").ToString(),
                    Price = decimal.Parse((worksheet.Cells[row, 4].Value ?? "0.0").ToString()),
                    PictureUrl = (worksheet.Cells[row, 3].Value ?? "").ToString(),
                    BrandId = int.Parse((worksheet.Cells[row, 4].Value ?? "0").ToString()),
                    CategoryId = int.Parse((worksheet.Cells[row, 5].Value ?? "0").ToString())
                });
            }
            return productList;
        }
    }
}