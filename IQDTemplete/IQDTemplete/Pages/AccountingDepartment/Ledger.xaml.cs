using Interface.Pages.AccountingDepartment;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Interface.Pages
{
    
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

        private void Debts_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService?.Navigate(new ViewDebtsLists());
        }
    }
}
