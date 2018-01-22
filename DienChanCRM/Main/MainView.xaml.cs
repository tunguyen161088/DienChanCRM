using System.Windows.Controls;
using DienChanCRM.Models;

namespace DienChanCRM.Main
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Page
    {
        public MainView(User user)
        {
            InitializeComponent();

            ((MainViewModel)DataContext).User = user;
        }
    }
}
