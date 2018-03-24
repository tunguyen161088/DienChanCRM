using System.Web.Http;
using DienChan.Entities;
using DienChan.Logic;

namespace DienChanAPI.Controllers
{
    public class ProductsController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetProducts()
        {
            var products = ProductsLogic.GetProducts();

            if (products == null)
                return NotFound();

            return Ok(products);
        }

        [HttpGet]
        public IHttpActionResult GetProduct(int id)
        {
            var product = ProductsLogic.GetProduct(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPut]
        public IHttpActionResult UpdateProduct(Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var productDb = ProductsLogic.GetProduct(product.productId);

            if (productDb == null)
                return NotFound();

            ProductsLogic.UpdateProduct(product);

            return Ok();
        }

        [HttpPost]
        public IHttpActionResult CreateProduct(Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            ProductsLogic.CreateProduct(product);

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteProduct(int id)
        {
            var product = ProductsLogic.GetProduct(id);

            if (product == null)
                return NotFound();

            ProductsLogic.DeleteProduct(id);

            return Ok();
        }
    }
}