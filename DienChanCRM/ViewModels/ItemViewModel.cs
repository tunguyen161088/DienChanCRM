namespace DienChanCRM.ViewModels
{
    public class ItemViewModel : ViewModelBase
    {
        private string _id;

        public string ID
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged("ID");
            }
        }

        private string _itemName;
        public string ItemName
        {
            get => _itemName;
            set
            {
                _itemName = value;
                OnPropertyChanged("ItemName");
            }
        }

        private string _itemDescription;

        public string ItemDescription
        {
            get => _itemDescription;
            set
            {
                _itemDescription = value;
                OnPropertyChanged("ItemDescription");
            }
        }

        private int _quantity;

        public int Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;

                SubTotal = _quantity * _unitPrice;

                OnPropertyChanged("Quantity");
            }
        }

        private decimal _unitPrice;

        public decimal UnitPrice
        {
            get => _unitPrice;
            set
            {
                _unitPrice = value;

                SubTotal = _quantity * _unitPrice;

                OnPropertyChanged("UnitPrice");
            }
        }

        private string _category;

        public string Category
        {
            get => _category;
            set
            {
                _category = value;
                OnPropertyChanged("Category");
            }
        }

        private decimal _subTotal;

        public decimal SubTotal
        {
            get => _subTotal;
            set
            {
                _subTotal = value;
                OnPropertyChanged("SubTotal");
            }
        }
    }
}
