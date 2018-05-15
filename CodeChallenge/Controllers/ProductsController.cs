using CodeChallenge.Entities;
using CodeChallenge.Models;
using CodeChallenge.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.Controllers
{
    [Route("api/products")]
    public class ProductsController : Controller
    {

        private IProductRepository productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [HttpGet()]
        public IActionResult GetAllProducts(ProductSearchModel productSearchModel)
        {
            //Returns 200 OK with product List as the Response Body
            var productEntities = productRepository.GetProducts(productSearchModel);

            var results = new List<ProductDto>();

            foreach (var productEntity in productEntities)
            {
                results.Add(new ProductDto
                {
                    Id = productEntity.Id,
                    Description = productEntity.Description,
                    Model = productEntity.Model,
                    Brand = productEntity.Brand
                });
            }
            return Ok(results);
        }
        
        [HttpGet("{id}", Name = "GetProduct")]
        public IActionResult GetProduct(string id)
        {
            //Retrieves Product with id that matches from Data Store
            var productEntity = productRepository.GetProduct(id);

            //If it doesn't exist returns a 404 Not Found
            if (productEntity == null)
            {
                return NotFound();
            }

            var productResult = new ProductDto()
            {
                Id = productEntity.Id,
                Description = productEntity.Description,
                Model = productEntity.Model,
                Brand = productEntity.Brand
            };

            //Returns 200 OK with product as the Response Body
            return Ok(productResult);

        }

        [HttpPost]
        public IActionResult CreateProduct(
            [FromBody] ProductDto product)
        {
            //If data in body of POST request could not be serialized to ProductDto
            if (product == null)
            {
                return BadRequest();
            }

            // Error with body data based on constraints in ProductDto
            if (!ModelState.IsValid)
            {
                //Return custom Error Messages
                return BadRequest(ModelState);
            }

            var productEntity = new Product()
            {
                Id = product.Id,
                Description = product.Description,
                Brand = product.Brand,
                Model = product.Model

            };

            //Adds to Data Storage
            if (!productRepository.AddProduct(productEntity))
            {
                return StatusCode(500, "Product already exists with that Id");
            }

            //Returns 201 Created Status Code, Product Data in Body of response with Location in the Response Header
            return Ok(product);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(string id,
            [FromBody] ProductDto product)
        {
            //If data in body of POST request could not be serialized to ProductDto
            if (product == null)
            {
                return BadRequest();
            }

            // Error with body data based on constraints in ProductDto
            if (!ModelState.IsValid)
            {
                //Return custom Error Messages
                return BadRequest(ModelState);
            }

            var productEntity = new Product()
            {
                Id = product.Id,
                Description = product.Description,
                Brand = product.Brand,
                Model = product.Model

            };

            //If it doesn't exist returns a 404 Not Found
            if (productRepository.GetProduct(productEntity.Id) == null)
            {
                return NotFound();
            }

            //Stores Product in the Data Store Overriting the old data
            if (!productRepository.UpdateProduct(productEntity))
            {
                return StatusCode(500, "There was an issue with updating the Product in the server");
            }

            //Returns 200 Ok with product from data store as Response Body
            return Ok(product);

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(string id)
        {
            if (!productRepository.DeleteProduct(id))
            {
                //If it doesn't exist returns a 404 Not Found
                return NotFound();
            }
            
            //Return 204 No Content to signify completion of deletion
            return NoContent();
        }


    }
}
