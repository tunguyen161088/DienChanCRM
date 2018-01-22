using System;
using System.Windows;
using System.Windows.Threading;
using DienChanCRM.DAL;
using DienChanCRM.Models;

namespace DienChanCRM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Current.DispatcherUnhandledException += HandleException;
        }

        private void HandleException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message);

            var log = new ApplicationLog
            {
                Message = e.Exception.Message,
                StackTrace = e.Exception.StackTrace,
                ComputerName = Environment.MachineName,
                OSVersion = Environment.OSVersion.ToString(),
                CreatedDate = DateTime.Now
            };

            ApplicationLogQuery.UpdateLog(log);

            e.Handled = true;
        }
    }
}
