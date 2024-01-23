using AutoMapper;
using FullCart.API.Dtos;
using FullCart.API.Dtos.InputModels;
using FullCart.Core.Consts;
using FullCart.Core.Entities;
using FullCart.Core.Interfaces;
using FullCart.Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FullCart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = AppRoles.Admin)]
    public class CartController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public CartController(UserManager<AppUser> userManager, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CartDto>> GetCart()
        {
            var email = User.FindFirstValue(ClaimTypes.Email)!;
            var user = await _userManager.FindByEmailAsync(email);
            CartByUserSpecifications cartSpec = new CartByUserSpecifications(user.Id);
            var cart = await _unitOfWork.Repository<Cart>().GetEntityWithSpec(cartSpec);
            if (cart is null)
            {
                return NotFound();
            }
            var cartDto = _mapper.Map<CartDto>(cart);
            return Ok(cartDto);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> AddCartItem(CartItemInputModel cartItemInput)
        {
            var email = User.FindFirstValue(ClaimTypes.Email)!;
            var user = await _userManager.FindByEmailAsync(email);
            var cart = await _unitOfWork.Repository<Cart>().GetByIdAsNoTrackingAsync(cartItemInput.Id);
            var cartItem = _mapper.Map<CartItem>(cartItemInput);
            if (cart is null)
            {
                Cart createdCart = new Cart()
                {
                    UserId = user.Id,
                    Status = CartStatus.Entered,
                    CartItems = new List<CartItem>() { cartItem }
                };
                _unitOfWork.Repository<Cart>().Add(createdCart);
            }
            else
            {
                cartItem.CartId = cart.Id;
                _unitOfWork.Repository<CartItem>().Add(cartItem);
            }
            await _unitOfWork.Complete();
            return Ok(true);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteCart(int cartId)
        {
            var cart = await _unitOfWork.Repository<Cart>().GetByIdAsync(cartId);

            if (cart is null) return NotFound();
            cart.Status = CartStatus.Cancelled;
            await _unitOfWork.Complete();
            return Ok(true);
        }

        [HttpDelete("RemoveItemFromCart")]
        public async Task<ActionResult<bool>> DeleteCartItem(int cartItemId)
        {
            var cartItem = await _unitOfWork.Repository<CartItem>().GetByIdAsync(cartItemId);
            if (cartItem is null) return NotFound();
            _unitOfWork.Repository<CartItem>().Delete(cartItem);
            await _unitOfWork.Complete();
            return Ok(true);
        }
    }
}