using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace DienChanCRM.Converters
{
    public class BoolToMultiVisibilityConverter : IMultiValueConverter
    {
        public bool IsNewButton { get; set; }
        public bool IsEditButton { get; set; }
        public bool IsSaveButton { get; set; }
        //public bool IsCancelButton { get; set; }
        //public bool IsClearButton { get; set; }
        //public bool IsExportPDFButton { get; set; }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 2) return Visibility.Visible;

            var isCreatingOrder = (bool)values[0];

            var isShowingOrder = (bool)values[1];

            if (IsNewButton)
                return isCreatingOrder ? Visibility.Collapsed : Visibility.Visible;

            if (IsEditButton)// || IsExportPDFButton)
                return !isCreatingOrder && isShowingOrder ? Visibility.Visible : Visibility.Collapsed;

            if (IsSaveButton)// || IsCancelButton || IsClearButton)
                return isCreatingOrder ? Visibility.Visible : Visibility.Collapsed;

            return Visibility.Visible;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
