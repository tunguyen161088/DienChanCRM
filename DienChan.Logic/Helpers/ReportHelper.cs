using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DienChan.Entities;
using DienChan.DataAccess;
using iTextSharp.text.pdf;

namespace DienChan.Logic.Helpers
{
    public class ReportHelper
    {
        public Stream CreateReport(Order order)
        {
            var tempLoc = AppDomain.CurrentDomain.BaseDirectory + @"Templates\DienChanSaleInvoice.pdf";

            //var newFile = tempLoc.Replace(".pdf", "_" + order.orderId + ".pdf");

            var stream = new MemoryStream();

            using (var reader = new PdfReader(tempLoc))
            {
                using (var stamper = new PdfStamper(reader, stream))
                {
                    var font = BaseFont.CreateFont();

                    var content = stamper.GetOverContent(1);

                    content.SaveState();
                    content.BeginText();
                    content.SetFontAndSize(font, 12.0f);

                    content.SetTextMatrix(60, 600);
                    content.ShowText("Customer Name: " + order.customer.firstName + " " + order.customer.lastName);

                    content.SetTextMatrix(60, 585);
                    content.ShowText(order.customer.address1 + (string.IsNullOrEmpty(order.customer.address2) ? "" : order.customer.address2));

                    content.SetTextMatrix(60, 570);
                    content.ShowText(order.customer.city + ", " + order.customer.state + " " + order.customer.zip);

                    content.SetTextMatrix(60, 555);
                    content.ShowText("Email: " + order.customer.email);

                    content.SetTextMatrix(60, 540);
                    content.ShowText("Tel: " + order.customer.phoneNumber);

                    content.SetFontAndSize(font, 16.0f);
                    content.SetTextMatrix(367, 600);
                    content.ShowText("Order Number: " + order.orderId);

                    content.SetFontAndSize(font, 16.0f);
                    content.SetTextMatrix(340, 570);
                    content.ShowText("Customer Number: " + FormatCustomer(order.customerId));

                    content.SetFontAndSize(font, 16.0f);
                    content.SetTextMatrix(400, 540);
                    content.ShowText("Paris, le " + order.orderDate.ToString("MMM d yyyy"));

                    var posY = 485;
                    content.SetFontAndSize(font, 10.0f);

                    for (var i = 0; i < order.items.Count; i++)
                    {
                        content.SetTextMatrix(70, posY);
                        content.ShowText(order.items[i].itemId.ToString());

                        content.SetTextMatrix(130, posY);
                        content.ShowText(order.items[i].name);

                        content.SetTextMatrix(360, posY);
                        content.ShowText(order.items[i].quantity.ToString());

                        content.SetTextMatrix(380, posY);
                        content.ShowText(order.items[i].unitPrice.ToString("C", Configuration.CurrentCurrency));

                        content.SetTextMatrix(435, posY);
                        content.ShowText((order.items[i].unitPrice * order.items[i].quantity).ToString("C", Configuration.CurrentCurrency));

                        content.SetTextMatrix(485, posY);
                        content.ShowText((order.items[i].unitPrice * order.items[i].quantity).ToString("C", Configuration.CurrentCurrency));

                        posY -= 18;
                    }

                    if (order.tax > 0)
                    {
                        content.SetTextMatrix(130, 293);
                        content.ShowText("Tax");

                        content.SetTextMatrix(485, 293);
                        content.ShowText(order.tax.ToString("C", Configuration.CurrentCurrency));
                    }

                    if (order.discount > 0)
                    {
                        content.SetTextMatrix(130, 275);
                        content.ShowText("Discount");

                        content.SetTextMatrix(485, 275);
                        content.ShowText("-" + order.discount.ToString("C", Configuration.CurrentCurrency));
                    }

                    content.SetFontAndSize(font, 12.0f);
                    content.SetTextMatrix(485, 250);
                    content.ShowText(order.orderTotal.ToString("C", Configuration.CurrentCurrency));

                    content.EndText();
                    content.RestoreState();

                    stamper.Writer.CloseStream = false;
                }
            }

            stream.Position = 0;

            return stream;
        }

        private string FormatCustomer(int id)
        {
            var result = new StringBuilder();
            for (var i = 0; i < 8 - id.ToString().Length; i++)
            {
                result.Append("0");
            }

            result.Append(id);

            return result.ToString();
        }
    }
}
