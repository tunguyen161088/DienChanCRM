using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DienChan.DataAccess;
using DienChan.Entities;
using DienChan.Logic.Helpers;

namespace DienChan.Logic
{
    public class ProductsLogic
    {
        private static readonly ProductsQuery _query = new ProductsQuery();
        public static List<Product> GetProducts()
        {
            return _query.GetProducts();
        }

        public static Product GetProduct(int productId)
        {
            return _query.GetProduct(productId);
        }

        public static void CreateProduct(Product product)
        {
            var productId = _query.CreateProduct(product);

            ImageHelper.UploadImage(product.image, $"{productId}.jpg");
        }

        public static void UpdateProduct(Product product)
        {
            if (product.image != null && product.ischangeimage)
                product.imageUrl = ImageHelper.UploadImage(product.image, $"{product.productId}.jpg");

            _query.UpdateProduct(product);
        }

        public static void DeleteProduct(int productId)
        {
            _query.DeleteProduct(productId);
        }
    }
}
