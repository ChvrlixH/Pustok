﻿namespace WebAPI.DTOs.ProductDtos
{
    public class ProductGetDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPercent { get; set; }
        public string? Description { get; set; }
        public int Rating { get; set; }
        public bool IsInStock { get; set; }
    }
}
