using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DienChan.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace DienChanAPI.Test
{
    [TestClass]
    public class ProductsTest
    {
        [TestMethod]
        public void GetProductsTest()
        {
            var client = new HttpClient();

            var url = "http://api.dienchanus.com/api/products/getproducts";

            var content = client.GetStringAsync(url);

            var products = JsonConvert.DeserializeObject<List<Product>>(content.Result);
        }

        [TestMethod]
        public void CreateProductTest()
        {
            var product = new Product
            {
                name = "Demo Name",
                description = "Deme description",
                price = 100,
                weight = 50,
                categoryId = 1,
                image = ConvertImage(@"D:\Products\OUT89.jpg")
            };

            var client = new HttpClient();

            var response = client.PostAsJsonAsync("http://localhost:56588/api/products/createproduct", product);

            var result = response.Result.StatusCode;
        }

        [TestMethod]
        public void UpdateProductTest()
        {
            var product = new Product
            {
                productId = 103,
                name = "Demo Name 2",
                description = "Deme description",
                price = 100,
                weight = 50,
                categoryId = 1
            };

            var client = new HttpClient();

            var response = client.PutAsJsonAsync("http://localhost:56588/api/products/updateproduct", product);

            var result = response.Result.StatusCode;
        }

        [TestMethod]
        public void DeleteProductTest()
        {
            var client = new HttpClient();

            // var response = client.PostAsJsonAsync("http://localhost:56588/api/products/createproduct", product);

            //var result = response.Result.StatusCode;

            var response = client.DeleteAsync("http://localhost:56588/api/products/deleteproduct/103");

            var result = response.Result.StatusCode;
        }

        [TestMethod]
        public void ConvertImageToString()
        {
            var imageUrl = @"D:\Products\OUT89.jpg";

            var image = Image.FromFile(imageUrl);

            byte[] arr;

            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Jpeg);

                arr = ms.ToArray();
            }

            var imageContent = Convert.ToBase64String(arr);

            var url = UploadImage(imageContent);
        }

        private string ConvertImage(string imageUrl)
        {
            var image = Image.FromFile(imageUrl);

            byte[] arr;

            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Jpeg);

                arr = ms.ToArray();
            }

            var imageContent = Convert.ToBase64String(arr);

            return imageContent;
        }

        private string UploadImage(string content)
        {
            var ar = Convert.FromBase64String(content);

            Image image;

            using (MemoryStream ms = new MemoryStream(ar))
            {
                image = PadImage(Image.FromStream(ms));
            }

            byte[] arr;

            using (var ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Jpeg);

                arr = ms.ToArray();
            }

            var imageContent = Convert.ToBase64String(arr);

            var ftpRequest = WebRequest.Create("ftp://dienchanus.com/httpdocs/images/Products/square3.jpg");

            ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;

            ftpRequest.Credentials = new NetworkCredential("tunguyen", "1Nothing");

            var bytes = Convert.FromBase64String(imageContent);

            Stream requestStream = ftpRequest.GetRequestStream();

            requestStream.Write(bytes, 0, bytes.Length);

            requestStream.Close();

            FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();

            response.Close();

            requestStream.Close();

            return "";
        }

        [TestMethod]
        public void FormatImages()
        {
            foreach (string fileName in Directory.GetFiles(@"D:\Products"))
            {
                var image = PadImage(Image.FromFile(fileName));

                byte[] arr;

                using (MemoryStream ms = new MemoryStream())
                {
                    image.Save(ms, ImageFormat.Jpeg);

                    arr = ms.ToArray();
                }

                var imageContent = Convert.ToBase64String(arr);

                File.WriteAllBytes(fileName.Replace("Products", "Products2"), Convert.FromBase64String(imageContent));
            }
        }

        public static Image PadImage(Image originalImage)
        {
            int largestDimension = Math.Max(originalImage.Height, originalImage.Width);
            Size squareSize = new Size(largestDimension, largestDimension);
            Bitmap squareImage = new Bitmap(squareSize.Width, squareSize.Height);
            using (Graphics graphics = Graphics.FromImage(squareImage))
            {
                graphics.FillRectangle(Brushes.White, 0, 0, squareSize.Width, squareSize.Height);
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                graphics.DrawImage(originalImage, (squareSize.Width / 2) - (originalImage.Width / 2), (squareSize.Height / 2) - (originalImage.Height / 2), originalImage.Width, originalImage.Height);
            }
            return squareImage;
        }
    }
}
