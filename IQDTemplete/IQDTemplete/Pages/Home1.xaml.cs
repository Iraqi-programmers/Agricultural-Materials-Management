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
using Interface.Pages;

namespace IQDTemplete.Pages
{
    /// <summary>
    /// Interaction logic for Home1.xaml
    /// </summary>
    public partial class Home1 : Page
    {
        public Home1()
        {
            InitializeComponent();
        }

      

        private void DebtsMangment_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.MainFrameInstance.Content = new DebitsPage();
        }

        private void DeploySalesMenue_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.MainFrameInstance.Content = new AddSalesMunuePage();
        }

        private void AddPurchasesMenue_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.MainFrameInstance.Content = new AddPurchasesPage();
        }
    }
}
