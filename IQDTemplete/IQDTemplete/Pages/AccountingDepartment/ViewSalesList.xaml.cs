using System.Windows.Controls;
using System.Windows.Data;

namespace Interface.Pages.AccountingDepartment
{
    /// <summary>
    /// Interaction logic for ViewSalesList.xaml
    /// </summary>
    public partial class ViewSalesList : Page
    {
        private CollectionViewSource _invoiceViewSource;
        public class Invoice
        {
            public string CustomerName { get; set; }
            public string InvoiceDate { get; set; }
            public decimal TotalAmount { get; set; }
            public decimal Profit { get; set; }
            public string UserName { get; set; }
            public string UserPhone { get; set; }
        }

        public ViewSalesList()
        {
            InitializeComponent();
            // بيانات تجريبية
            var invoices = new List<Invoice>
            {
                new Invoice { CustomerName = "محمد أحمد", InvoiceDate = "2023-10-01", TotalAmount = 1500, Profit = 300, UserName = "علي محمود", UserPhone = "0123456789" },
                new Invoice { CustomerName = "سارة خالد", InvoiceDate = "2023-10-02", TotalAmount = 2000, Profit = 400, UserName = "علي محمود", UserPhone = "0123456789" },
                new Invoice { CustomerName = "يوسف سعيد", InvoiceDate = "2023-10-03", TotalAmount = 2500, Profit = 500, UserName = "علي محمود", UserPhone = "0123456789" },
                new Invoice { CustomerName = "زيون الكئيب", InvoiceDate = "2023-10-03", TotalAmount = 2500, Profit = 500, UserName = "علي محمود", UserPhone = "0123456789" },
                new Invoice { CustomerName = "سند الحزين", InvoiceDate = "2023-10-03", TotalAmount = 2500, Profit = 500, UserName = "علي محمود", UserPhone = "0123456789" },
                new Invoice { CustomerName = "محمد علي كلاي", InvoiceDate = "2023-10-03", TotalAmount = 2500, Profit = 500, UserName = "علي محمود", UserPhone = "0123456789" },
                new Invoice { CustomerName = "علي حسن", InvoiceDate = "2023-10-03", TotalAmount = 2500, Profit = 500, UserName = "علي محمود", UserPhone = "0123456789" },
                new Invoice { CustomerName = "علي حسن", InvoiceDate = "2023-10-03", TotalAmount = 2500, Profit = 500, UserName = "علي محمود", UserPhone = "0123456789" }
            };

      
            _invoiceViewSource = new CollectionViewSource { Source = invoices };
            InvoiceDataGrid.ItemsSource = _invoiceViewSource.View;
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // فلترة البيانات بناءً على نص البحث
            if (_invoiceViewSource != null)
            {
                _invoiceViewSource.View.Filter = item =>
                {
                    var invoice = item as Invoice;
                    if (invoice == null) return false;

                    // البحث في جميع الحقول
                    return invoice.CustomerName.Contains(SearchTextBox.Text, StringComparison.OrdinalIgnoreCase) ||
                           invoice.InvoiceDate.Contains(SearchTextBox.Text, StringComparison.OrdinalIgnoreCase) ||
                           invoice.TotalAmount.ToString().Contains(SearchTextBox.Text, StringComparison.OrdinalIgnoreCase) ||
                           invoice.Profit.ToString().Contains(SearchTextBox.Text, StringComparison.OrdinalIgnoreCase) ||
                           invoice.UserName.Contains(SearchTextBox.Text, StringComparison.OrdinalIgnoreCase) ||
                           invoice.UserPhone.Contains(SearchTextBox.Text, StringComparison.OrdinalIgnoreCase);
                };
            }
        }

        private void InvoiceDataGrid_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (NavigationService?.CanGoBack == true)
            {
                NavigationService.GoBack();
            }
        }
    }
}

