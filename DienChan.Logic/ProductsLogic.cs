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

        public static ActionResult CreateProduct(Product product)
        {
            var result = _query.CreateProduct(product);

            if (product.productId == 0)
                return result;

            ImageHelper.UploadImage(product.image, $"{product.productId}.jpg");

            return result;
        }

        public static ActionResult UpdateProduct(Product product)
        {
            if (product.image != null && product.isImageUpdate)
                product.imageUrl = ImageHelper.UploadImage(product.image, $"{product.productId}.jpg");

            return _query.UpdateProduct(product);
        }

        public static ActionResult DeleteProduct(int productId)
        {
            return _query.DeleteProduct(productId);
        }
    }
}
