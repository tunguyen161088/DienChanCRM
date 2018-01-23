using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DienChanCRM.Models;
using DienChanCRM.ViewModels;

namespace DienChanCRM.Helpers
{
    public class MapHelper
    {
        public static ObservableCollection<ProductViewModel> MapProductModelToViewModel(List<Models.Product> products)
        {
            var result = new ObservableCollection<ProductViewModel>();

            products.ForEach(p => result.Add(MapProductModelToViewModel(p)));

            return result;
        }

        public static ProductViewModel MapProductModelToViewModel(Models.Product product)
        {
            return new ProductViewModel
            {
                ID = product.ID,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Weight = product.Weight,
                Category = product.Category
            };
        }

        public static ObservableCollection<ItemViewModel> MapItemModelToViewModel(List<Item> items)
        {
            var result = new ObservableCollection<ItemViewModel>();

            items.ForEach(i => result.Add(MapItemModelToViewModel(i)));

            return result;
        }

        public static ItemViewModel MapItemModelToViewModel(Item item)
        {
            return new ItemViewModel
            {
                ID = item.ID,
                ItemName = item.ItemName,
                ItemDescription = item.ItemDescription,
                UnitPrice = item.UnitPrice,
                Category = item.Category,
                Quantity = item.Quantity
            };
        }

        public static CustomerViewModel MapCustomerModelToViewModel(Customer cus)
        {
            return new CustomerViewModel
            {
                ID = cus.ID,
                FirstName = cus.FirstName,
                LastName= cus.LastName,
                Email = cus.Email,
                Phone = cus.Phone,
                Address1 = cus.Address1,
                Address2 = cus.Address2,
                City = cus.City,
                State = cus.State,
                Zip = cus.Zip,
                Country = cus.Country
            };
        }

        public static ObservableCollection<OrderViewModel> MapOrderModelToViewModel(List<Order> orders)
        {
            var result = new ObservableCollection<OrderViewModel>();

            orders.ForEach(o => result.Add(MapOrderModelToViewModel(o)));

            return result;
        }

        public static OrderViewModel MapOrderModelToViewModel(Order order)
        {
            return new OrderViewModel
            {
                CustomerID = order.CustomerID,
                ID = order.ID,
                OrderDate = order.OrderDate,
                SubTotal = order.SubTotal,
                Tax = order.Tax,
                Discount = order.Discount,
                OrderTotal = order.OrderTotal
            };
        }
    }
}
