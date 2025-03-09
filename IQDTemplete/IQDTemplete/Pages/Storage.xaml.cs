using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Interface.Pages.StorageDepartment;
using IQDTemplete;

namespace Interface.Pages
{
    /// <summary>
    /// Interaction logic for Storage.xaml
    /// </summary>
    public partial class Storage : Page
    {
        public Storage()
        {
            InitializeComponent();
        }

        private void Suppliers_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.MainFrameInstance.Content = new StorageContentPage();
        }

        private void StorageContent_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void DeploySalesMenue_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.MainFrameInstance.Content = new AddSalesMunuePage();
        }
    }
}
