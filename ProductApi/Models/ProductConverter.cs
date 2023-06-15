

using OrderApi.Models;

namespace ProductApi.Models
{
    public class ProductConverter : IConverter<Product, ProductDTO>
    {
        public Product Convert(ProductDTO sharedProduct)
        {
            return new Product
            {
                Id = sharedProduct.Id,
                Name = sharedProduct.Name,
                Price = sharedProduct.Price,
                ItemsInStock = sharedProduct.ItemsInStock,
                ItemsReserved = sharedProduct.ItemsReserved
            };
        }

        public ProductDTO Convert(Product hiddenProduct)
        {
            return new ProductDTO
            {
                Id = hiddenProduct.Id,
                Name = hiddenProduct.Name,
                Price = hiddenProduct.Price,
                ItemsInStock = hiddenProduct.ItemsInStock,
                ItemsReserved = hiddenProduct.ItemsReserved
            };
        }
    }
}
