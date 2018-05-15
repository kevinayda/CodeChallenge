using CodeChallenge.Entities;
using CodeChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.Services
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts(ProductSearchModel productSearchModel);
        
        Product GetProduct(string id);

        Boolean AddProduct(Product product);

        Boolean UpdateProduct(Product product);

        Boolean DeleteProduct(string id);
    }
}
