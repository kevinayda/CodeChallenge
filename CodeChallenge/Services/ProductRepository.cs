using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Entities;
using CodeChallenge.Models;

namespace CodeChallenge.Services
{
    public class ProductRepository : IProductRepository
    {
        private ProductContext context;

        public ProductRepository(ProductContext context)
        {
            this.context = context;
        }

        public Boolean AddProduct(Product product)
        {
            if (context.Products.Where(c => c.Id == product.Id).FirstOrDefault() == null)
            { 
                context.Add(product);
                context.SaveChanges();
                return true;
            }

            return false;
        }

        public bool DeleteProduct(string id)
        {
            var productEntity = context.Products.Where(c => c.Id == id).FirstOrDefault();

            if (productEntity != null)
            {
                context.Remove(productEntity);
                context.SaveChanges();
                return true;
            }

            return false;
        }

        public Product GetProduct(string id)
        {
            return context.Products.Where(c => c.Id == id).FirstOrDefault();
        }

        public IEnumerable<Product> GetProducts(ProductSearchModel productSearchModel)
        {
            if (productSearchModel != null)
            {
                if (productSearchModel.Brand != null)
                {
                    return context.Products.Where(c => c.Brand == productSearchModel.Brand).ToList();
                }
                else if (productSearchModel.Model != null)
                {
                    return context.Products.Where(c => c.Model == productSearchModel.Model).ToList();
                }
                else if (productSearchModel.Description != null)
                {
                    return context.Products.Where(c => c.Description.Contains(productSearchModel.Description)).ToList();
                }
            }
            return context.Products.OrderBy(c => c.Id).ToList();
        }
       
        public bool UpdateProduct(Product product)
        {
            var productEntity = context.Products.Where(c => c.Id == product.Id).FirstOrDefault();
            if (productEntity != null)
            {
                productEntity.Id = product.Id;
                productEntity.Description = product.Description;
                productEntity.Model = product.Model;
                productEntity.Brand = product.Brand;
            }
            return (context.SaveChanges() >= 0);
        }
    }
}
