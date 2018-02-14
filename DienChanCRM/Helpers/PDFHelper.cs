using System;
using System.IO;
using System.Linq;
using System.Text;
using DienChanCRM.Main;
using DienChanCRM.ViewModels;
using iTextSharp.text.pdf;

namespace DienChanCRM.Helpers
{
    public class PDFHelper
    {
        public void ExportPDF(OrderViewModel order)
        {
            var tempLoc = AppDomain.CurrentDomain.BaseDirectory + @"Templates\DienChanSaleInvoice.pdf";

            var newFile = tempLoc.Replace(".pdf", "_" + order.ID + ".pdf");

            using (var reader = new PdfReader(tempLoc))
            {
                using (var stamper = new PdfStamper(reader, new FileStream(newFile, FileMode.Create, FileAccess.Write)))
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
                    content.ShowText("Order Number: " + order.ID);

                    content.SetFontAndSize(font, 16.0f);
                    content.SetTextMatrix(340, 570);
                    content.ShowText("Customer Number: " + FormatCustomer(order.CustomerID));

                    content.SetFontAndSize(font, 16.0f);
                    content.SetTextMatrix(400, 540);
                    content.ShowText("Paris, le " + order.OrderDate.ToString("MMM d yyyy"));

                    var posY = 485;
                    content.SetFontAndSize(font, 10.0f);

                    for (var i = 0 ; i < order.Items.Count; i++)
                    {
                        content.SetTextMatrix(70, posY);
                        content.ShowText(order.Items[i].ID);

                        content.SetTextMatrix(130, posY);
                        content.ShowText(order.Items[i].ItemName);

                        content.SetTextMatrix(360, posY);
                        content.ShowText(order.Items[i].Quantity.ToString());

                        content.SetTextMatrix(380, posY);
                        content.ShowText(order.Items[i].UnitPrice.ToString("C"));

                        content.SetTextMatrix(435, posY);
                        content.ShowText((order.Items[i].UnitPrice * order.Items[i].Quantity).ToString("C"));

                        posY -= 18;
                    }

                    content.SetFontAndSize(font, 12.0f);
                    content.SetTextMatrix(485, 250);
                    content.ShowText(order.Items.Sum(i => i.UnitPrice * i.Quantity).ToString("C"));

                    content.EndText();
                    content.RestoreState();
                }
            }

            System.Diagnostics.Process.Start(newFile);
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

