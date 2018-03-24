using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DienChan.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace DienChanAPI.Test
{
    [TestClass]
    public class OrdersTest
    {
        [TestMethod]
        public void CreateOrderTest()
        {
            var order = new Order
            {
                customer = new Customer
                {
                    firstName = "Tu",
                    lastName = "Nguyen",
                    address1 = "34 Clifton Pl",
                    city = "Port Jeff",
                    country = "USA",
                    email = "tunguyen161088@gmail.com",
                    phone = "6319770728",
                    state = "NY",
                    zip = "11776",
                    updateDate = DateTime.Now
                },
                active = true,
                discount = 10,
                items = new List<Item>
                {
                    new Item
                    {
                        quantity = 2,
                        updateDate = DateTime.Now,
                        productId = 1,
                        name = "GRAND MARTEAU",
                        description = "Marteler sur le dos, les épaules, les cuisses, les genoux, les jambes. Réduire les douleurs lancinantes. Aider à assouplir les nerfs. Assouplir les muscles. Aider à la circulation du sang. Utilisé souvent pour traiter le mal au dos, la sciatique, le mal au genou, l’entorse. Deux côtés yin et yang.",
                        unitPrice = 17,
                        weight = 115,
                        imageUrl = "http://dienchanus.com/images/Products/OUT072.jpg"
                    },
                    new Item
                    {
                        quantity = 5,
                        updateDate = DateTime.Now,
                        productId = 2,
                        name = "ROULEAU YANG DYNAMIQUE",
                        description = "Rouler et frotter les points sur les épaules, les bras. Réchauffer le corps. Caractère yang.",
                        unitPrice = 17,
                        weight = 104,
                        imageUrl = "http://dienchanus.com/images/Products/OUT073.jpg"
                    },
                },
                orderDate = DateTime.Now,
                tax = 20,
                subTotal = 119,
                orderTotal = 129
            };

            var client = new HttpClient();

            var response = client.PostAsJsonAsync("http://localhost:56588/api/orders/createorder", order);

            var result = response.Result.StatusCode;
        }

        [TestMethod]
        public void GetOrdersTest()
        {
            var client = new HttpClient();

            var url = "http://localhost:56588/api/orders/getorders";

            var content = client.GetStringAsync(url);

            var orders = JsonConvert.DeserializeObject<List<Order>>(content.Result);
        }

        [TestMethod]
        public void GetOrderTest()
        {
            var client = new HttpClient();

            var url = "http://localhost:56588/api/orders/getorder/3";

            var content = client.GetStringAsync(url);

            var orders = JsonConvert.DeserializeObject<Order>(content.Result);
        }

        [TestMethod]
        public void DeleteOrderTest()
        {
            var client = new HttpClient();

            var response = client.DeleteAsync("http://localhost:56588/api/orders/deleteorder/1");

            var result = response.Result.StatusCode;
        }

        [TestMethod]
        public void UpdateOrderTest()
        {
            var order = new Order
            {
                customer = new Customer
                {
                    customerId = 7,
                    firstName = "Tu2",
                    lastName = "Nguyen",
                    address1 = "34 Clifton Pl",
                    city = "Port Jeff",
                    country = "USA",
                    email = "tunguyen161088@gmail.com",
                    phone = "6319770728",
                    state = "NY",
                    zip = "11776",
                    updateDate = DateTime.Now
                },
                active = true,
                discount = 10,
                items = new List<Item>
                {
                    new Item
                    {
                        quantity = 2,
                        updateDate = DateTime.Now,
                        productId = 1,
                        name = "GRAND MARTEAU",
                        description = "Marteler sur le dos, les épaules, les cuisses, les genoux, les jambes. Réduire les douleurs lancinantes. Aider à assouplir les nerfs. Assouplir les muscles. Aider à la circulation du sang. Utilisé souvent pour traiter le mal au dos, la sciatique, le mal au genou, l’entorse. Deux côtés yin et yang.",
                        unitPrice = 17,
                        weight = 115,
                        imageUrl = "http://dienchanus.com/images/Products/OUT072.jpg"
                    },
                    new Item
                    {
                        quantity = 6,
                        updateDate = DateTime.Now,
                        productId = 2,
                        name = "ROULEAU YANG DYNAMIQUE",
                        description = "Rouler et frotter les points sur les épaules, les bras. Réchauffer le corps. Caractère yang.",
                        unitPrice = 17,
                        weight = 104,
                        imageUrl = "http://dienchanus.com/images/Products/OUT073.jpg"
                    },
                },
                orderDate = DateTime.Now,
                updateDate = DateTime.Now,
                tax = 20,
                subTotal = 136,
                orderTotal = 146,
                orderId = 20180001,
                isItemUpdate = true
            };

            var client = new HttpClient();

            var response = client.PutAsJsonAsync("http://localhost:56588/api/orders/updateorder", order);

            var result = response.Result.StatusCode;
        }
    }
}
