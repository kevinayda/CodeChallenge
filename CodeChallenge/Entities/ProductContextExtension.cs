using CodeChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.Entities
{
    public static class ProductContextExtension
    {
        public static void EnsureSeedDataForContext(this ProductContext context)
        {
            if (context.Products.Any())
            {
                return;
            }

            var products = new List<Product>()
            {
                new Product()
                {
                    Id = "11111",
                    Description = "Item 1",
                    Model = "Model 1",
                    Brand = "Brand 1"
                },
                new Product()
                {
                    Id = "22222",
                    Description = "Item 2",
                    Model = "Model 2",
                    Brand = "Brand 2"
                },
                new Product()
                {
                    Id = "33333",
                    Description = "Item 3",
                    Model = "Model 3",
                    Brand = "Brand 3"
                },
            };

            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }
}
