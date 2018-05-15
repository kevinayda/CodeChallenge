using CodeChallenge.Entities;
using CodeChallenge.Models;
using CodeChallenge.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.Tests
{
    //Tests will return a 200 or 204 response on success or a 500 on error
    //These tests show whether the functions present in the API calls are working correctly
    [Route("api/test")]
    public class TestController : Controller
    {

        private IProductRepository productRepository;

        public TestController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        //First test is to create a new product and verify that it doesn't exist
        [HttpGet("test1")]
        public IActionResult TestOne()
        {
            var productEntity = new Product()
            {
                Id = "00000",
                Description = "Test Product",
                Model = "Test Model",
                Brand = "Test Brand"
            };

            if (productRepository.GetProduct(productEntity.Id) == null)
            {
                var product = new ProductDto()
                {
                    Id = productEntity.Id,
                    Description = productEntity.Description,
                    Model = productEntity.Model,
                    Brand = productEntity.Brand
                };
                return Ok(product);
            }
            return BadRequest(); 
        }

        //Second Test Is to add the item and verify it exits 
        [HttpGet("test2")]
        public IActionResult TestTwo()
        {
            var productEntity = new Product()
            {
                Id = "00000",
                Description = "Test Product",
                Model = "Test Model",
                Brand = "Test Brand"
            };

            productRepository.AddProduct(productEntity);

            var productFromRepo = productRepository.GetProduct(productEntity.Id);

            if ( productFromRepo != null)
            {
                if (productFromRepo.Id == productEntity.Id && productFromRepo.Description == productEntity.Description
                    && productFromRepo.Brand == productEntity.Brand && productFromRepo.Model == productEntity.Model)
                {
                    var product = new ProductDto()
                    {
                        Id = productFromRepo.Id,
                        Description = productFromRepo.Description,
                        Model = productFromRepo.Model,
                        Brand = productFromRepo.Brand
                    };
                    return Ok(product);
                }
            };
            return BadRequest();
        }

        //Third test is to modify the item and verify its data has changed
        [HttpGet("test3")]
        public IActionResult TestThree()
        {
            var productEntity = new Product()
            {
                Id = "00000",
                Description = "Test Product Changed",
                Model = "Test Model",
                Brand = "Test Brand"
            };

            productRepository.UpdateProduct(productEntity);

            var productFromRepo = productRepository.GetProduct(productEntity.Id);

            if (productFromRepo != null)
            {
                if (productFromRepo.Id == productEntity.Id && productFromRepo.Description == productEntity.Description
                    && productFromRepo.Brand == productEntity.Brand && productFromRepo.Model == productEntity.Model)
                {
                    var product = new ProductDto()
                    {
                        Id = productFromRepo.Id,
                        Description = productFromRepo.Description,
                        Model = productFromRepo.Model,
                        Brand = productFromRepo.Brand
                    };
                    return Ok(product);
                }
            };
            return BadRequest();
        }

        //Fourth Test is to delete the item and verify it is gone
        [HttpGet("test4")]
        public IActionResult TestFour()
        {
            var productId = "00000";

            productRepository.DeleteProduct(productId);

            if (productRepository.GetProduct(productId) == null)
            {
                return Ok();
            }
            return BadRequest();
        }
        
    }
}
