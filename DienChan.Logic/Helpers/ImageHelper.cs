using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DienChan.DataAccess;
using DienChan.Entities;

namespace DienChan.Logic.Helpers
{
    public class ImageHelper
    {
        public static string UploadImage(string content, string imageName)
        {
            var ar = Convert.FromBase64String(content);

            Image image;

            using (MemoryStream ms = new MemoryStream(ar))
            {
                image = SquareImage(Image.FromStream(ms));
            }

            byte[] arr;

            using (var ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Jpeg);

                arr = ms.ToArray();
            }

            var imageContent = Convert.ToBase64String(arr);

            var ftpRequest = WebRequest.Create(Configuration.FtpUrl + imageName);
            
            ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;

            ftpRequest.Credentials = new NetworkCredential(Configuration.FtpUsername, Configuration.FtpPassword);

            var bytes = Convert.FromBase64String(imageContent);

            Stream requestStream = ftpRequest.GetRequestStream();

            requestStream.Write(bytes, 0, bytes.Length);

            requestStream.Close();

            FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();

            response.Close();

            requestStream.Close();

            return Configuration.BaseImageUrl + imageName;
        }

        public static Image SquareImage(Image originalImage)
        {
            var largestDimension = Math.Max(originalImage.Height, originalImage.Width);

            var squareSize = new Size(largestDimension, largestDimension);

            var squareImage = new Bitmap(squareSize.Width, squareSize.Height);

            using (var graphics = Graphics.FromImage(squareImage))
            {
                graphics.FillRectangle(Brushes.White, 0, 0, squareSize.Width, squareSize.Height);

                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                graphics.DrawImage(originalImage, (squareSize.Width / 2) - (originalImage.Width / 2), (squareSize.Height / 2) - (originalImage.Height / 2), originalImage.Width, originalImage.Height);
            }

            return squareImage;
        }

        public static void RemoveOldImage(string imageUrl)
        {
            try
            {
                var url = Configuration.FtpUrl + imageUrl.Replace(Configuration.BaseImageUrl, "");

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);
                request.Method = WebRequestMethods.Ftp.DeleteFile;
                request.Credentials = new NetworkCredential(Configuration.FtpUsername, Configuration.FtpPassword);

                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    var result = response.StatusDescription;
                }
            }
            catch (Exception e)
            {
                //
            }


        }
    }
}
