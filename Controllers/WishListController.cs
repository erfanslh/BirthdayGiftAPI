using BirthdayApp.DTO;
using BirthdayApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Security.Claims;

namespace BirthdayApp.Controller
{
    [ApiController]
    [Route("api/[Controller]")]
    [Authorize]
    public class WishListController : ControllerBase
    {
        private readonly IWishListServices _services;
        public WishListController(IWishListServices services)
        {
            _services = services;
        }

        [HttpGet("MyWishList")]
        public async Task<IActionResult> GetMyList()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }
            var wishes = await _services.GetMyWishListAsync(userId);
            return Ok(wishes);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddWish(AddWishListDTO addWishListDTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var wishes = await _services.AddAsync(userId!, addWishListDTO);

            return Ok(wishes);
        }

        [HttpGet("MyWishListByID/{id}")]
        public async Task<IActionResult> GetWishById(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }
            var ItemList = await _services.GetByIdAsync(id, userId);
            return Ok(ItemList);
        }

        [HttpGet("ViewFriendsWishList/{friendId}")]
        public async Task<IActionResult> ViewFriendsWishList(string friendId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }
            var ItemList = await _services.ViewFriendsWishList(friendId, userId);
            return Ok(ItemList);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWish(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            if (userId == null)
            {
                return Unauthorized();
            }
            var delete = await _services.DeleteAsync(id, userId);
            return Ok(delete);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWish(int id, UpdateWishListDTO dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
            if (userId == null)
            {
                return Unauthorized();
            }
            var update = await _services.UpdateAsync(id, userId, dto);
            return Ok(update);
        }

        [HttpPost("Book/{id}")]
        public async Task<IActionResult> BookWish(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }
            var book = await _services.BookWishAsync(id, userId);
            return Ok(book);
        }

        [HttpPost("Unbook/{id}")]
        public async Task<IActionResult> UnbookWish(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }
            var unbook = await _services.UnBookWishAsync(id, userId);
            return Ok(unbook);
        }
    }
}
