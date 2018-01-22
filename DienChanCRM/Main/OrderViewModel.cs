using System.Collections.Generic;
using DienChanCRM.Models;

namespace DienChanCRM.Main
{
    public class OrderViewModel: ViewModelBase
    {

        private CustomerViewModel _customer;
        public CustomerViewModel Customer
        {
            get => _customer;
            set
            {
                _customer = value;
                OnPropertyChanged("Customer");
            }
        }

        private decimal _tax;
        public decimal Tax
        {
            get => _tax;
            set
            {
                _tax = value;
                OnPropertyChanged("Tax");
            }
        }

        private decimal _discount;
        public decimal Discount
        {
            get => _discount;
            set
            {
                _discount = value;
                OnPropertyChanged("Discount");
            }
        }

        public int ID { get; set; }

        public List<Item> Items { get; set; }

        public decimal SubTotal { get; set; }

        public decimal OrderTotal { get; set; }
    }
}
