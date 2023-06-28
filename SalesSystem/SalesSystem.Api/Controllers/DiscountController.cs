using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SalesSystem.Application.Interfaces;
using SalesSystem.Core.DTOs.Discount;
using SalesSystem.Core.Entities;

namespace SalesSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;

        public DiscountController(IDiscountRepository discountRepository, IMapper mapper)
        {
            _discountRepository = discountRepository;
            _mapper = mapper;
        }

        [HttpGet("discounts")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _discountRepository.GetAll();

            return Ok(result);
        }

        [HttpGet("disocunt/{id}", Name ="get-discount")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _discountRepository.Get(id);

            return Ok(result);
        }

        [HttpPost("discount")]
        public async Task<IActionResult> CreateAsync([FromForm] CreateDto createDto)
        {
            var discount = _mapper.Map<Discount>(createDto);

            var result = await _discountRepository.Add(discount);

            return CreatedAtRoute("get-discount", new { id = result }, discount);
        }

        [HttpPut("discount/{id}")]
        public async Task<IActionResult> UpdateAsync([FromForm] UpdateDto dto, int id)
        {
            var discount = await _discountRepository.Get(id);

            if (discount is null) return NotFound();

            _mapper.Map(dto, discount);

            await _discountRepository.Update(id, discount);

            return NoContent();
        }

        [HttpDelete("discount-delete/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _discountRepository.Delete(id);

            return NoContent();
        }

    }
}
