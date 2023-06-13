﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.DTOs.ProductDtos;
using WebAPI.Models;
using WebAPI.Repositories.Implementations;
using WebAPI.Repositories.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productRepository.GetAll().ToListAsync();

            List<ProductGetDto> productGetDtos= new List<ProductGetDto>();

            foreach (var product in products)
            {
                productGetDtos.Add(new ProductGetDto
                {
                    Id= product.Id,
                    Name= product.Name,
                    Price= product.Price,
                    DiscountPercent= product.DiscountPercent,
                    Description= product.Description,
                    Rating= product.Rating,
                    IsInStock= product.IsInStock
                });
            }

            return StatusCode(StatusCodes.Status200OK, productGetDtos);
        }


        [HttpPost]
        public async Task<IActionResult> Create(ProductPostDto productPostDto)
        {
            Product product = new()
            {
                Name = productPostDto.Name,
                Price = productPostDto.Price,
                DiscountPercent = productPostDto.DiscountPercent,
                Description = productPostDto.Description,
                Rating = productPostDto.Rating,
                IsInStock = productPostDto.IsInStock,
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            await _productRepository.CreateAsync(product);
            await _productRepository.SaveAsync();

            return StatusCode(StatusCodes.Status201Created, "Product successfully created");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var products = await _productRepository.GetByIdAsync(id);

            if (products is null)
                return NotFound($"Product not found by id: {id}");

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProductPutDto productPutDto)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product is null) return NotFound($"Product not found by id: {id}");

            if (product.Id != productPutDto.Id)
                return BadRequest();

            product.Name = productPutDto.Name;
            product.Price = productPutDto.Price;
            product.DiscountPercent = productPutDto.DiscountPercent;
            product.Description = productPutDto.Description;
            product.Rating = productPutDto.Rating;
            product.IsInStock= productPutDto.IsInStock;

            _productRepository.Update(product);
            await _productRepository.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product is null) return NotFound($"Product not found by id: {id}");

            _productRepository.Delete(product);
            await _productRepository.SaveAsync();

            return StatusCode(StatusCodes.Status200OK, "Product successfully deleted");
        }
    }
}
