using System.Collections.Generic;
using DienChanCRM.Models;

namespace DienChanCRM.ViewModels
{
    public class CustomerViewModel : ViewModelBase
    {
        public CustomerViewModel(Customer c = null)
        {
            //FirstName = "Tu";
        }

        private int _id;

        public int ID
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged("ID");
            }
        }

        private string _firstName;

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        private string _lastName;

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged("LastName");
            }
        }

        private string _phone;

        public string Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                OnPropertyChanged("Phone");
            }
        }

        private string _email;

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged("Email");
            }
        }

        private string _address1;

        public string Address1
        {
            get => _address1;
            set
            {
                _address1 = value;
                OnPropertyChanged("Address1");
            }
        }

        private string _address2;

        public string Address2
        {
            get => _address2;
            set
            {
                _address2 = value;
                OnPropertyChanged("Address2");
            }
        }

        private string _city;

        public string City
        {
            get => _city;
            set
            {
                _city = value;
                OnPropertyChanged("City");
            }
        }

        private string _zip;

        public string Zip
        {
            get => _zip;
            set
            {
                _zip = value;
                OnPropertyChanged("Zip");
            }
        }

        private string _state;

        public string State
        {
            get => _state;
            set
            {
                _state = value;
                OnPropertyChanged("State");
            }
        }

        private string _country;

        public string Country
        {
            get => _country;
            set
            {
                _country = value;
                OnPropertyChanged("Country");
            }
        }

        private List<Order> _orders;

        public List<Order> Orders
        {
            get => _orders;
            set
            {
                _orders = value;
                OnPropertyChanged("Orders");
            }
        }
    }
}
