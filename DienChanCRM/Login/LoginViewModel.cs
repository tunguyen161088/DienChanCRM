using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DienChanCRM.DAL;
using DienChanCRM.Models;

namespace DienChanCRM.Login
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly GetAuthenticationQuery _getAuthenticationQuery;
        public LoginViewModel()
        {
            _getAuthenticationQuery = new GetAuthenticationQuery();
        }

        public User IsPasswordValid(LoginViewModel vm)
        {
            var hashPassword = ComputeHash(vm.Password);

            return _getAuthenticationQuery.GetAuthentication(vm.UserName, hashPassword);
        }

        private string ComputeHash(string password)
        {
            using (var md5Hash = MD5.Create())
            {
                var bytes = Encoding.ASCII.GetBytes(password);

                var data = md5Hash.ComputeHash(bytes);

                var sb = new StringBuilder();

                foreach (var d in data)
                {
                    sb.Append(d.ToString("x2"));
                }

                return sb.ToString();
            }
        }

        private string _userName;

        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged("UserName");
            }
        }

        private string _password;

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }

        private string _version;

        public string Version
        {
            get => _version;
            set
            {
                _version = value;
                OnPropertyChanged("Version");
            }
        }

        private string _errorMessage;

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged("ErrorMessage");
            }
        }
    }
}
