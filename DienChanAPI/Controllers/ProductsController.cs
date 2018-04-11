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
        public IHttpActionResult GetProducts(string token, int userId)
        {
            if(!ApplicationHelper.IsTokenValid(token, userId))
                return Content(HttpStatusCode.BadRequest, "BadRequest");

            var products = ProductsLogic.GetProducts();

            if (products == null)
                return Content(HttpStatusCode.NotFound, "NotFound");

            return Content(HttpStatusCode.OK, products);
        }

        [HttpGet]
        public IHttpActionResult GetProduct(string token, int userId, int id)
        {
            if (!ApplicationHelper.IsTokenValid(token, userId))
                return Content(HttpStatusCode.BadRequest, "BadRequest");

            var product = ProductsLogic.GetProduct(id);

            if (product == null)
                return Content(HttpStatusCode.NotFound, "NotFound");

            return Content(HttpStatusCode.OK, product);
        }

        [HttpPut]
        public IHttpActionResult UpdateProduct(string token, int userId, Product product)
        {
            if (!ModelState.IsValid || !ApplicationHelper.IsTokenValid(token, userId))
                return Content(HttpStatusCode.BadRequest, "BadRequest");

            var productDb = ProductsLogic.GetProduct(product.productId);

            if (productDb == null)
                return Content(HttpStatusCode.NotFound, "NotFound");

            var result = ProductsLogic.UpdateProduct(product);

            if (!result.Success)
                ApplicationHelper.Log(result.Message);

            return result.Success 
                ? Content(HttpStatusCode.OK, "OK") 
                : Content(HttpStatusCode.InternalServerError, result.Message);
        }

        [HttpPost]
        public IHttpActionResult CreateProduct(string token, int userId, Product product)
        {
            if (!ModelState.IsValid || !ApplicationHelper.IsTokenValid(token, userId))
                return Content(HttpStatusCode.BadRequest, "BadRequest");

            var result = ProductsLogic.CreateProduct(product);

            if (!result.Success)
                ApplicationHelper.Log(result.Message);

            return result.Success
                ? Content(HttpStatusCode.OK, "OK")
                : Content(HttpStatusCode.InternalServerError, result.Message);
        }

        [HttpDelete]
        public IHttpActionResult DeleteProduct(string token, int userId, int id)
        {
            if (!ApplicationHelper.IsTokenValid(token, userId))
                return Content(HttpStatusCode.BadRequest, "BadRequest");

            var product = ProductsLogic.GetProduct(id);

            if (product == null)
                return Content(HttpStatusCode.NotFound, "NotFound");

            var result = ProductsLogic.DeleteProduct(id);

            if (!result.Success)
                ApplicationHelper.Log(result.Message);

            return result.Success
                ? Content(HttpStatusCode.OK, "OK")
                : Content(HttpStatusCode.InternalServerError, result.Message);
        }
    }
}