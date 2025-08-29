using BuddyR.Api.DTOs;
using BuddyR.Application.Services;
using BuddyR.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BuddyR.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();

            var result = users.Select(u => new UserDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email
            }).ToList();

            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var result = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };

            return Ok(result);
        }

        [HttpGet("by-email")]
        public async Task<IActionResult> GetByEmail([FromQuery] string email)
        {
            var user = await _userService.GetByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }

            var result = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid data");

            try
            {
                var user = await _userService.CreateAsync(dto.Name, dto.Email);
                return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UserDto dto)
        {
            var entity = new UserEntity
            {
                Id = id,
                Name = dto.Name,
                Email = dto.Email,
            };

            try
            {
                var updatedUser = await _userService.UpdateAsync(entity);
                if (updatedUser == null)
                    return NotFound();

                var result = new UserDto
                {
                    Id = updatedUser.Id,
                    Name = updatedUser.Name,
                    Email = updatedUser.Email,
                };

                return Ok(result);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }

        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _userService.DeleteAsync(id);
                return Ok();
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

    }
}
