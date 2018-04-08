using System.Net;
using System.Web.Http;
using DienChan.Entities;
using DienChan.Logic;
using DienChan.Logic.Helpers;

namespace DienChanAPI.Controllers
{
    public class ProductsController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetProducts()
        {
            var products = ProductsLogic.GetProducts();

            if (products == null)
                return Content(HttpStatusCode.NotFound, "NotFound");

            return Content(HttpStatusCode.OK, products);
        }

        [HttpGet]
        public IHttpActionResult GetProduct(int id)
        {
            var product = ProductsLogic.GetProduct(id);

            if (product == null)
                return Content(HttpStatusCode.NotFound, "NotFound");

            return Content(HttpStatusCode.OK, product);
        }

        [HttpPut]
        public IHttpActionResult UpdateProduct(Product product)
        {
            if (!ModelState.IsValid)
                return Content(HttpStatusCode.BadRequest, "BadRequest");

            var productDb = ProductsLogic.GetProduct(product.productId);

            if (productDb == null)
                return Content(HttpStatusCode.NotFound, "NotFound");

            var result = ProductsLogic.UpdateProduct(product);

            if (!result.Success)
                ApplicationLogHelper.Log(result.Message);

            return result.Success 
                ? Content(HttpStatusCode.OK, "OK") 
                : Content(HttpStatusCode.InternalServerError, result.Message);
        }

        [HttpPost]
        public IHttpActionResult CreateProduct(Product product)
        {
            if (!ModelState.IsValid)
                return Content(HttpStatusCode.BadRequest, "BadRequest");

            var result = ProductsLogic.CreateProduct(product);

            if (!result.Success)
                ApplicationLogHelper.Log(result.Message);

            return result.Success
                ? Content(HttpStatusCode.OK, "OK")
                : Content(HttpStatusCode.InternalServerError, result.Message);
        }

        [HttpDelete]
        public IHttpActionResult DeleteProduct(int id)
        {
            var product = ProductsLogic.GetProduct(id);

            if (product == null)
                return Content(HttpStatusCode.NotFound, "NotFound");

            var result = ProductsLogic.DeleteProduct(id);

            if (!result.Success)
                ApplicationLogHelper.Log(result.Message);

            return result.Success
                ? Content(HttpStatusCode.OK, "OK")
                : Content(HttpStatusCode.InternalServerError, result.Message);
        }
    }
}