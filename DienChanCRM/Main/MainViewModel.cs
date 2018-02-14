using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DienChanCRM.DAL;
using DienChanCRM.Helpers;
using DienChanCRM.Models;
using DienChanCRM.Product;
using DienChanCRM.ViewModels;

namespace DienChanCRM.Main
{
    public class MainViewModel : ViewModelBase
    {
        private readonly PDFHelper _helper;
        private readonly OrderQuery _orderQuery;
        private readonly ProductQuery _productQuery;

        public RelayCommand NewOrderCommand { get; }
        public RelayCommand EditOrderCommand { get; }
        public RelayCommand SaveOrderCommand { get; }
        public RelayCommand CancelOrderCommand { get; }
        public RelayCommand ClearOrderCommand { get; }
        public RelayCommand ExportPDFCommand { get; }
        public RelayCommand AddItemCommand { get; }
        public RelayCommand RemoveItemCommand { get; }
        public RelayCommand SearchCommand { get; }
        public RelayCommand RemoveProductCommand { get; }
        public RelayCommand UpdateProductCommand { get; }
        public RelayCommand AddProductCommand { get; }


        public MainViewModel()
        {
            _helper = new PDFHelper();
            _orderQuery = new OrderQuery();
            _productQuery = new ProductQuery();

            _order = new OrderViewModel
            {
                Items = new ObservableCollection<ItemViewModel>(),
                Customer = new CustomerViewModel()
            };

            NewOrderCommand = new RelayCommand(OnNewOrder);
            EditOrderCommand = new RelayCommand(OnEditOrder);
            SaveOrderCommand = new RelayCommand(OnSaveOrder);
            CancelOrderCommand = new RelayCommand(OnCancelOrder);
            ClearOrderCommand = new RelayCommand(OnClearOrder);
            ExportPDFCommand = new RelayCommand(OnExportPDF);
            AddItemCommand = new RelayCommand(OnAddItem);
            RemoveItemCommand = new RelayCommand(OnRemoveItem);
            SearchCommand = new RelayCommand(OnSearch);
            AddProductCommand = new RelayCommand(OnAddProduct);
            RemoveProductCommand = new RelayCommand(OnRemoveProduct);
            UpdateProductCommand = new RelayCommand(OnUpdateProduct);

            GetProducts();
        }

        private void OnRemoveProduct()
        {
            if (MessageBox.Show("Are you sure to remove this product?", "Confirmation", MessageBoxButton.YesNo,
                    MessageBoxImage.Question) != MessageBoxResult.Yes) return;

            _productQuery.RemoveProduct(Product.ID);

            GetProducts();
        }

        private void OnUpdateProduct()
        {
            if (string.IsNullOrEmpty(Product.ID) || string.IsNullOrEmpty(Product.Name) ||
                string.IsNullOrEmpty(Product.Description) || Product.Price == 0m || Product.Weight == 0m)
            {
                MessageBox.Show("You need to fill out all the fields");

                Product = new ProductViewModel { CategoryID = 1 };

                return;
            }

            _productQuery.UpdateProduct(MapHelper.MapProductViewModelToModel(Product));

            GetProducts();
        }

        private void OnAddProduct()
        {
            if (!Products.Any() || !string.IsNullOrEmpty(Products.FirstOrDefault()?.ID))
                Products.Insert(0, new ProductViewModel { CategoryID = 1 });
        }

        public void GetProducts()
        {
            Task.Factory.StartNew(() =>
            {
                Products = MapHelper.MapProductModelToViewModel(_productQuery.GetProducts());

                Product = Products?.FirstOrDefault();
            });
        }

        private ObservableCollection<OrderViewModel> _orders;

        public ObservableCollection<OrderViewModel> Orders
        {
            get => _orders;
            set
            {
                _orders = value;

                OnPropertyChanged("Orders");
            }
        }

        private OrderViewModel _order;

        public OrderViewModel Order
        {
            get => _order;
            set
            {
                _order = value;

                IsShowingOrder = _order != null && _order.ID != 0;

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

        private bool _isLoading;

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged("IsLoading");
            }
        }

        private ObservableCollection<ProductViewModel> _products;

        public ObservableCollection<ProductViewModel> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged("Products");
            }
        }

        private ProductViewModel _product;

        public ProductViewModel Product
        {
            get => _product;
            set
            {
                _product = value;
                OnPropertyChanged("Product");
            }
        }

        public User User { get; set; }

        private void OnNewOrder()
        {
            IsCreatingOrder = true;

            Order = Order ?? new OrderViewModel
            {
                Items = new ObservableCollection<ItemViewModel>(),
                Customer = new CustomerViewModel()
            };
        }

        private void OnEditOrder()
        {
            if (MessageBox.Show("Are you sure to remove this order?", "Confirmation", MessageBoxButton.YesNo,
                    MessageBoxImage.Question) != MessageBoxResult.Yes) return;

            _orderQuery.DeleteOrder(Order.ID);

            OnClearOrder();

            OnSearch();
        }

        private void OnSaveOrder()
        {
            if (Order.Items.Count == 0)
            {
                MessageBox.Show("You need to add at least 1 item!");

                return;
            }

            if (string.IsNullOrEmpty(Order.Customer.FirstName))
            {
                MessageBox.Show("You need to enter your first name!");

                return;
            }

            if (string.IsNullOrEmpty(Order.Customer.LastName))
            {
                MessageBox.Show("You need to enter your last name!");

                return;
            }

            if (string.IsNullOrEmpty(Order.Customer.Email))
            {
                MessageBox.Show("You need to enter your email!");

                return;
            }

            Task.Factory.StartNew(() =>
            {
                IsLoading = true;

                IsCreatingOrder = false;

                IsShowingOrder = true;

                _orderQuery.UpdateOrder(Order);

                OnClearOrder();

                OnSearch();

                IsLoading = false;
            });
        }

        private void OnCancelOrder()
        {
            IsCreatingOrder = false;

            OnClearOrder();
        }

        private void OnClearOrder()
        {
            Order = new OrderViewModel
            {
                Items = new ObservableCollection<ItemViewModel>(),
                Customer = new CustomerViewModel()
            };

            IsShowingOrder = false;
        }

        private void OnExportPDF()
        {
            _helper.ExportPDF(Order);
        }

        private void OnAddItem()
        {
            var productView = new ProductSearchView();

            if (productView.ShowDialog() != true) return;

            var productSearch = ((ProductSearchViewModel)productView.DataContext);

            var selectedProduct = productSearch.SelectedProduct;

            var existItem = Order.Items.SingleOrDefault(i => i.ID == selectedProduct.ID);

            if (existItem != null)
            {
                existItem.Quantity += Convert.ToInt32(productSearch.Quantity);

                return;
            }

            Order.Items.Insert(0, new ItemViewModel
            {
                ID = selectedProduct.ID,
                ItemDescription = selectedProduct.Description,
                ItemName = selectedProduct.Name,
                UnitPrice = selectedProduct.Price,
                Quantity = Convert.ToInt32(productSearch.Quantity),
                Category = selectedProduct.Category
            });
        }

        private void OnRemoveItem()
        {
            if (MessageBox.Show("Are you sure to remove this item?", "Confirmation", MessageBoxButton.YesNo,
                    MessageBoxImage.Question) != MessageBoxResult.Yes) return;

            Order.Items.Remove(Order.SelectedItem);

            Order.SelectedItem = Order.Items.FirstOrDefault();
        }

        public void OnSearch()
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject())) return;

            Task.Factory.StartNew(() =>
            {
                IsLoading = true;

                Orders = _orderQuery.GetListOrders(TextSearch);

                Order = Orders?.FirstOrDefault() ?? new OrderViewModel
                {
                    Items = new ObservableCollection<ItemViewModel>(),
                    Customer = new CustomerViewModel()
                };

                IsLoading = false;
            });
        }
    }
}


