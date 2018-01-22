using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DienChanCRM.DAL;

namespace DienChanCRM.Product
{
    public class ProductSearchViewModel : ViewModelBase
    {
        public RelayCommand SearchCommand { get; }
        public RelayCommand SelectProductCommand { get; }
        public RelayCommand<Window> CloseCommand { get; }

        private readonly ProductQuery _productQuery;

        public ProductSearchViewModel()
        {
            SearchCommand = new RelayCommand(OnSearch);
            SelectProductCommand = new RelayCommand(OnSelectProduct);
            CloseCommand = new RelayCommand<Window>(OnClose);
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

        private int _quantity;
        public int Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
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

        private void OnClose(Window window)
        {
            window?.Close();
        }

        private void OnSelectProduct()
        {

        }

        private void OnSearch()
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject())) return;

            Task.Factory.StartNew(() =>
            {
                IsLoading = true;

                Products = MapProductModelToViewModel(_productQuery.SearchProducts(TextSearch));

                SelectedProduct = Products?.FirstOrDefault();

                IsLoading = false;
            });

        }

        private ObservableCollection<ProductViewModel> MapProductModelToViewModel(List<Models.Product> products)
        {
            var result = new ObservableCollection<ProductViewModel>();

            products.ForEach(p =>
                result.Add(new ProductViewModel
                {
                    ID = p.ID,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Weight = p.Weight,
                    Category = p.Category
                }));

            return result;
        }
    }
}
