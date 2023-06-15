using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OrderApi.Models;
using ProductApi.Data;
using ProductApi.Models;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IRepository<Product> repository;
        private readonly IConverter<Product, ProductDTO> productConverter;

        public ProductsController(IRepository<Product> repos, IConverter<Product,ProductDTO> converter)
        {
            repository = repos;
            productConverter = converter;
        }

        // GET products
        [HttpGet]
        public IEnumerable<ProductDTO> Get()
        {
            var productDtoList = new List<ProductDTO>();
            foreach(var product in repository.GetAll())
            {
                var productDto = productConverter.Convert(product);
                productDtoList.Add(productDto);
            }
            return productDtoList;
        }

        // GET products/5
        [HttpGet("{id}", Name="GetProduct")]
        public IActionResult Get(int id)
        {
            var item = repository.Get(id);
            if (item == null)
            {
                return NotFound();
            }
            var productDto = productConverter.Convert(item);
            return new ObjectResult(productDto);
        }

        // POST products
        [HttpPost]
        public IActionResult Post([FromBody]ProductDTO productDto)
        {
            if (productDto == null)
            {
                return BadRequest();
            }

            var product = productConverter.Convert(productDto);
            var newProduct = repository.Add(product);

            return CreatedAtRoute("GetProduct", new { id = newProduct.Id },
                    productConverter.Convert(newProduct));
        }

        // PUT products/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]ProductDTO productDto)
        {
            if (productDto == null || productDto.Id != id)
            {
                return BadRequest();
            }

            var modifiedProduct = repository.Get(id);

            if (modifiedProduct == null)
            {
                return NotFound();
            }

            modifiedProduct.Name = productDto.Name;
            modifiedProduct.Price = productDto.Price;
            modifiedProduct.ItemsInStock = productDto.ItemsInStock;
            modifiedProduct.ItemsReserved = productDto.ItemsReserved;

            repository.Edit(modifiedProduct);
            return new NoContentResult();
        }

        // DELETE products/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (repository.Get(id) == null)
            {
                return NotFound();
            }

            repository.Remove(id);
            return new NoContentResult();
        }
    }
}
