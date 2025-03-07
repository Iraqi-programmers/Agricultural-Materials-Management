using Interface.Pages.AccountingDepartment;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Interface.Pages
{
    /// <summary>
    /// Interaction logic for Ledger.xaml
    /// </summary>
    public partial class Ledger : Page
    {
        public Ledger()
        {
            InitializeComponent();
        }

        private void Sales_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
            NavigationService?.Navigate(new ViewSalesList());
        }

        private void Purchases_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService?.Navigate(new ViewPurchasesList());

        }
    }
}
