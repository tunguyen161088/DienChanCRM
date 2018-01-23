using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DienChanCRM.DAL;
using DienChanCRM.Helpers;
using DienChanCRM.ViewModels;

namespace DienChanCRM.Product
{
    public class ProductSearchViewModel : ViewModelBase
    {
        public RelayCommand SearchCommand { get; }
        public RelayCommand<Window> SelectProductCommand { get; }
        public RelayCommand<Window> CloseCommand { get; }
        public RelayCommand SetQuantityCommand { get; }

        private readonly ProductQuery _productQuery;

        public ProductSearchViewModel()
        {
            SearchCommand = new RelayCommand(OnSearch);
            SelectProductCommand = new RelayCommand<Window>(OnSelectProduct);
            CloseCommand = new RelayCommand<Window>(OnClose);
            SetQuantityCommand = new RelayCommand(OnSetQuantity);
            Quantity = "0";
            _productQuery = new ProductQuery();
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

        private ProductViewModel _selectedProduct;

        public ProductViewModel SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;

                int qty;

                IsSelected = _selectedProduct != null && int.TryParse(_quantity, out qty) && qty > 0;

                OnPropertyChanged("SelectedProduct");
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

        private string _quantity;
        public string Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;

                int qty;

                IsSelected = _selectedProduct != null && int.TryParse(_quantity, out qty) && qty > 0;

                OnPropertyChanged("Quantity");
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

        private bool _isSelected;

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        private void OnClose(Window window)
        {
            if (window == null) return;

            window.DialogResult = false;

            window.Close();
        }

        private void OnSelectProduct(Window window)
        {
            if (window == null) return;

            window.DialogResult = true;

            window.Close();
        }

        private void OnSearch()
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject())) return;

            Task.Factory.StartNew(() =>
            {
                IsLoading = true;

                Products = MapHelper.MapProductModelToViewModel(_productQuery.SearchProducts(TextSearch));

                SelectedProduct = Products?.FirstOrDefault();

                IsLoading = false;
            });

        }

        private void OnSetQuantity()
        {
            int qty;

            if (int.TryParse(Quantity, out qty)) return;

            Quantity = "0";
        }
    }
}
