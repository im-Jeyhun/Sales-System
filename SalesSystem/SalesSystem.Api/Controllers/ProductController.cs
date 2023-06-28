using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SalesSystem.Application.Interfaces;
using SalesSystem.Core.DTOs.Product;
using SalesSystem.Core.Entities;

namespace SalesSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet("products")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _productRepository.GetAll();

            return Ok(result);
        }

        [HttpGet("product/{id}", Name = "get-product")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _productRepository.Get(id);

            return Ok(result);
        }

        [HttpPost("product")]
        public async Task<IActionResult> CreateAsync([FromForm] CreateDto createDto)
        {
            var discount = _mapper.Map<Product>(createDto);

            var result = await _productRepository.Add(discount);

            return CreatedAtRoute("get-product", new { id = result }, discount);
        }

        [HttpPut("product/{id}")]
        public async Task<IActionResult> UpdateAsync([FromForm] UpdateDto dto, int id)
        {
            var discount = await _productRepository.Get(id);

            if (discount is null) return NotFound();

            _mapper.Map(dto, discount);

            await _productRepository.Update(id, discount);

            return NoContent();
        }

        [HttpDelete("product-delete/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _productRepository.Delete(id);

            return NoContent();
        }

    }
}
