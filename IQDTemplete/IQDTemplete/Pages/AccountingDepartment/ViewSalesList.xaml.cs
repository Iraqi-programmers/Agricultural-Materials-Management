using Interface.Pages.UserControl;
using System.Windows.Controls;
using System.Windows.Data;

namespace Interface.Pages.AccountingDepartment
{
    /// <summary>
    /// Interaction logic for ViewSalesList.xaml
    /// </summary>
    /// 

    public partial class ViewSalesList : Page
    {

        private CollectionViewSource _invoiceViewSource;
        public class Invoice
        {
            public string CustomerName { get; set; }
            public string InvoiceDate { get; set; }
            public decimal TotalAmount { get; set; }
            public decimal LisrNum { get; set; }
            public string UserName { get; set; }
            public string UserPhone { get; set; }
        }

        public ViewSalesList()
        {
            InitializeComponent();
            // بيانات تجريبية
            var invoices = new List<Invoice>
            {
                new Invoice { CustomerName = "محمد أحمد", InvoiceDate = "2023-10-01", TotalAmount = 1500, LisrNum = 1, UserName = "علي محمود", UserPhone = "0123456789" },
                new Invoice { CustomerName = "سارة خالد", InvoiceDate = "2023-10-02", TotalAmount = 2000, LisrNum = 4, UserName = "علي محمود", UserPhone = "0123456789" },
                new Invoice { CustomerName = "يوسف سعيد", InvoiceDate = "2023-10-03", TotalAmount = 2500, LisrNum = 50, UserName = "علي محمود", UserPhone = "0123456789" },
                new Invoice { CustomerName = "زيون الكئيب", InvoiceDate = "2023-10-03", TotalAmount = 2500, LisrNum  = 500, UserName = "علي محمود", UserPhone = "0123456789" },
                new Invoice { CustomerName = "سند الحزين", InvoiceDate = "2023-10-03", TotalAmount = 2500, LisrNum = 9, UserName = "علي محمود", UserPhone = "0123456789" },
                new Invoice { CustomerName = "محمد علي كلاي", InvoiceDate = "2023-10-03", TotalAmount = 2500, LisrNum = 40, UserName = "علي محمود", UserPhone = "0123456789" },
                new Invoice { CustomerName = "علي حسن", InvoiceDate = "2023-10-03", TotalAmount = 2500, LisrNum = 20, UserName = "علي محمود", UserPhone = "0123456789" },
                new Invoice { CustomerName = "علي حسن", InvoiceDate = "2023-10-03", TotalAmount = 2500, LisrNum = 25, UserName = "علي محمود", UserPhone = "0123456789" }
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
                           invoice.LisrNum.ToString().Contains(SearchTextBox.Text, StringComparison.OrdinalIgnoreCase) ||
                           invoice.UserName.Contains(SearchTextBox.Text, StringComparison.OrdinalIgnoreCase) ||
                           invoice.UserPhone.Contains(SearchTextBox.Text, StringComparison.OrdinalIgnoreCase);
                };
            }
        }


        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (NavigationService?.CanGoBack == true)
            {
                NavigationService.GoBack();
            }
        }

        private void CollectionViewSource_Filter(object sender, FilterEventArgs e)
        {
            // تطبيق الفلترة بناءً على البحث
            var invoice = e.Item as Invoice; // استبدل Invoice بنوع البيانات الخاص بك
            if (invoice == null) return;

            string filterText = SearchTextBox.Text.ToLower();
            string selectedFilter = (FilterComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            switch (selectedFilter)
            {
                case "اسم المستخدم":
                    e.Accepted = invoice.UserName.ToLower().Contains(filterText);
                    break;
                case "تاريخ القائمة":
                    e.Accepted = invoice.InvoiceDate.ToLower().Contains(filterText);
                    break;
                case "رقم القائمة":
                    e.Accepted = invoice.LisrNum.ToString().Contains(filterText);
                    break;
                case "المبلغ الكلي":
                    e.Accepted = invoice.TotalAmount.ToString().Contains(filterText);
                    break;
                case "اسم الزبون":
                    e.Accepted = invoice.CustomerName.ToLower().Contains(filterText);
                    break;
                default:
                    e.Accepted = true;
                    break;
            }
        }

        private void MenuItem_Details_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new ctrlSalesListDetails());
        }

        private void MenuItem_Export_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
    
}

