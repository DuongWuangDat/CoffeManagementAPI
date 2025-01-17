using CoffeeManagementAPI.DTOs.Product;
using CoffeeManagementAPI.Model;

namespace CoffeeManagementAPI.Mappers.Prod
{
    public static class ProductMapper
    {

        public static ProductDTO toProdDTO (this Product product)
        {
            return new()
            {
                CategoryName = product.Category.CategoryName,
                ProductName = product.ProductName,
                IsSoldOut = product.IsSoldOut,
                Price = product.Price,
                ProductID = product.ProductID,
                Image = product.Image,
                AverageStar = product.AverageStar,
            };
        }

        public static Product toProductFromCreate (this CreateProductDTO createProductDTO)
        {
            return new()
            {
                CategoryId = createProductDTO.CategoryId,
                ProductName = createProductDTO.ProductName,
                IsSoldOut = createProductDTO.IsSoldOut,
                Price = createProductDTO.Price,
                Image = createProductDTO.Image,
                AverageStar=0,
                RatingPerson=0
            };

        }

    }
}
