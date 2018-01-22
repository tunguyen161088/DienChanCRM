using System;
using System.IO;
using System.Linq;
using DienChanCRM.Main;
using iTextSharp.text.pdf;

namespace DienChanCRM.Helpers
{
    public class PDFHelper
    {
        public void ExportPDF(OrderViewModel order)
        {
            var tempLoc = @"D:\DienChanSaleInvoice.pdf";

            using (var reader = new PdfReader(tempLoc))
            {
                using (var stamper = new PdfStamper(reader, new FileStream(tempLoc.Replace(".pdf", "_1.pdf"), FileMode.Create, FileAccess.Write)))
                {
                    var font = BaseFont.CreateFont();

                    var content = stamper.GetOverContent(1);

                    content.SaveState();
                    content.BeginText();
                    content.SetFontAndSize(font, 12.0f);

                    content.SetTextMatrix(60, 600);
                    content.ShowText("Customer Name: " + order.Customer.FirstName + " " + order.Customer.LastName);

                    content.SetTextMatrix(60, 585);
                    content.ShowText(order.Customer.Address1 + (string.IsNullOrEmpty(order.Customer.Address2) ? "" : order.Customer.Address2));

                    content.SetTextMatrix(60, 570);
                    content.ShowText(order.Customer.City + ", " + order.Customer.State + " " + order.Customer.Zip);

                    content.SetTextMatrix(60, 555);
                    content.ShowText("Email: " + order.Customer.Email);

                    content.SetTextMatrix(60, 540);
                    content.ShowText("Tel: " + order.Customer.Phone);

                    content.SetFontAndSize(font, 16.0f);
                    content.SetTextMatrix(367, 600);
                    content.ShowText("Order Number: " + "20180001");

                    content.SetFontAndSize(font, 16.0f);
                    content.SetTextMatrix(340, 570);
                    content.ShowText("Customer Number: " + "81020001");

                    content.SetFontAndSize(font, 16.0f);
                    content.SetTextMatrix(400, 540);
                    content.ShowText("Paris, le " + DateTime.Today.ToString("MMM d yyyy"));

                    var posY = 450;
                    content.SetFontAndSize(font, 10.0f);

                    for (var i = order.Items.Count - 1; i >= 0; i--)
                    {

                        content.SetTextMatrix(70, posY);
                        content.ShowText(DateTime.Now.ToShortDateString());

                        content.SetTextMatrix(130, posY);
                        content.ShowText(order.Items[i].ItemName);

                        content.SetTextMatrix(360, posY);
                        content.ShowText(order.Items[i].Quantity.ToString());

                        content.SetTextMatrix(380, posY);
                        content.ShowText(order.Items[i].UnitPrice.ToString("C"));

                        content.SetTextMatrix(435, posY);
                        content.ShowText((order.Items[i].UnitPrice * order.Items[i].Quantity).ToString("C"));

                        posY += 18;
                    }

                    content.SetFontAndSize(font, 12.0f);
                    content.SetTextMatrix(485, 250);
                    content.ShowText(order.Items.Sum(i => i.UnitPrice * i.Quantity).ToString("C"));

                    content.EndText();
                    content.RestoreState();
                }
            }
        }
    }
}

