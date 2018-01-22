using System;
using System.Collections.Generic;
using DienChanCRM.Helpers;
using DienChanCRM.Models;
using DienChanCRM.Product;

namespace DienChanCRM.Main
{
    public class MainViewModel : ViewModelBase
    {
        private readonly PDFHelper _helper;
        public RelayCommand NewOrderCommand { get; }
        public RelayCommand EditOrderCommand { get; }
        public RelayCommand SaveOrderCommand { get; }
        public RelayCommand CancelOrderCommand { get; }
        public RelayCommand ClearOrderCommand { get; }
        public RelayCommand ExportPDFCommand { get; }
        public RelayCommand AddItemCommand { get; }


        public MainViewModel()
        {
            _helper = new PDFHelper();

            _order = new OrderViewModel
            {
                Items = new List<Item>(),
                Customer = new CustomerViewModel()
            };

            NewOrderCommand = new RelayCommand(OnNewOrder);
            EditOrderCommand = new RelayCommand(OnEditOrder);
            SaveOrderCommand = new RelayCommand(OnSaveOrder);
            CancelOrderCommand = new RelayCommand(OnCancelOrder);
            ClearOrderCommand = new RelayCommand(OnClearOrder);
            ExportPDFCommand = new RelayCommand(OnExportPDF);
            AddItemCommand = new RelayCommand(OnAddItem);
        }

        private OrderViewModel _order;
        public OrderViewModel Order
        {
            get => _order;
            set
            {
                _order = value;
                OnPropertyChanged("Order");
            }
        }

        private string _message;
        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged("Message");
            }
        }

        private string _textSearch;

        public string TextSearch
        {
            get => _textSearch;
            set
            {
                _textSearch = value;
                OnPropertyChanged("TextSearch");
            }
        }

        private bool _isCreatingOrder;
        public bool IsCreatingOrder
        {
            get => _isCreatingOrder;
            set
            {
                _isCreatingOrder = value;
                OnPropertyChanged("IsCreatingOrder");
            }
        }

        private bool _isShowingOrder;

        public bool IsShowingOrder
        {
            get => _isShowingOrder;
            set
            {
                _isShowingOrder = value;
                OnPropertyChanged("IsShowingOrder");
            }
        }

        public User User { get; set; }

        private void OnNewOrder()
        {
            IsCreatingOrder = true;
        }

        private void OnEditOrder()
        {

        }

        private void OnSaveOrder()
        {
            IsCreatingOrder = false;

            IsShowingOrder = true;
        }

        private void OnCancelOrder()
        {
            IsCreatingOrder = false;
        }

        private void OnClearOrder()
        {
            Order = new OrderViewModel
            {
                Items = new List<Item>(),
                Customer = new CustomerViewModel()
        };
        }

        private void OnExportPDF()
        {
            _helper.ExportPDF(Order);
        }

        private void OnAddItem()
        {
            var productView = new ProductSearchView();

            productView.ShowDialog();
        }
    }
}


