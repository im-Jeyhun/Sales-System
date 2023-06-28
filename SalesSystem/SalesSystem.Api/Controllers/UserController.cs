using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SalesSystem.Application.Interfaces;
using SalesSystem.Core.DTOs.User;
using SalesSystem.Core.Entities;

namespace SalesSystem.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        [HttpGet("users")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _userRepository.GetAll();

            return Ok(result);
        }
        [HttpGet("user/{id}", Name ="get-user")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _userRepository.Get(id);

            return Ok(result);
        }

        [HttpPost("user")]
        public async Task<IActionResult> CreateAsync(CreateDto createDto)
        {
            var user = _mapper.Map<User>(createDto);

            var result = await _userRepository.Add(user);

            return CreatedAtAction(nameof(GetById), new { id = result} , user);
        }

        [HttpPut("user/{id}")]
        public async Task<IActionResult> UpdateAsync(UpdateDto dto , int id)
        {
            var user = await _userRepository.Get(id);

            if (user is null) return NotFound();

            _mapper.Map(dto, user);

            await _userRepository.Update(id , user);

            return NoContent();
        }

        [HttpDelete("user/{id}")] 
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _userRepository.Delete(id);

            return NoContent();
        }

    }
}
