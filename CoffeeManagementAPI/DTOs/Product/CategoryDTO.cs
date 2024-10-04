﻿namespace CoffeeManagementAPI.DTOs.Product
{
    public class CategoryDTO
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; } = string.Empty;

        public List<ProductDTO> Products { get; set; } = new List<ProductDTO>();
    }
}