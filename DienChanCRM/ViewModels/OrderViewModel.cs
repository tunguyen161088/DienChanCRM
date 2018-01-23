using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DienChanCRM.Models;

namespace DienChanCRM.ViewModels
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

        private ObservableCollection<ItemViewModel> _items;
        public ObservableCollection<ItemViewModel> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged("Items");
            }
        }

        private ItemViewModel _selectedItem;
        public ItemViewModel SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged("SelectedItem");
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

        public decimal SubTotal { get; set; }

        public decimal OrderTotal { get; set; }

        public DateTime OrderDate { get; set; }

        public int CustomerID { get; set; }
    }
}
