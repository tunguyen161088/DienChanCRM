using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using DienChan.Entities;
using DienChan.Logic;
using DienChanAPI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace DienChanAPI.Test
{
    [TestClass]
    public class UsersTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            //var url = "http://localhost:56588/api/users/GetAuthentication";

            //var client = new HttpClient();

            //var content = client.GetStringAsync(url);

            //var user = JsonConvert.DeserializeObject<User>(content.Result);

            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri("http://localhost:56588/api/users/GetAuthentication");

                var user = new User
                {
                    username = "admin",
                    password = ComputeHash("1Nothing")
                };

                //HTTP POST
                var response = client.PostAsJsonAsync("http://localhost:56588/api/users/GetAuthentication", user);

                var result = response.Result.Content.ReadAsAsync<User>().Result;
            }
        }

        [TestMethod]
        public void TestMethod2()
        {
            var client = new HttpClient();

            var pairs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("username", "admin"),
                new KeyValuePair<string, string>("password", "test@123")
            };

            var content = new FormUrlEncodedContent(pairs);

            var response = client.PostAsync("http://localhost:56588/api/users/GetAuthentication", content);

            var result = response.Result.Content.ReadAsAsync<User>().Result;
        }

        //[TestMethod]
        //public void TestMethod3()
        //{
        //    new UsersLogic().GetAuthentication("admin", ComputeHash("1Nothing"));
        //}

        private string ComputeHash(string password)
        {
            using (var md5Hash = MD5.Create())
            {
                var bytes = Encoding.ASCII.GetBytes(password);

                var data = md5Hash.ComputeHash(bytes);

                var sb = new StringBuilder();

                foreach (var d in data)
                {
                    sb.Append(d.ToString("x2"));
                }

                return sb.ToString();
            }
        }
    }
}
