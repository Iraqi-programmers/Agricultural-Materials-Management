using Interface.Pages.UserControl;
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

namespace Interface.Pages.AccountingDepartment
{
    /// <summary>
    /// Interaction logic for ViewPurchasesList.xaml
    /// </summary>
    public partial class ViewPurchasesList : Page
    {

        private CollectionViewSource _invoiceViewSource;
        public class Invoice
        {
            public string ListNum { get; set; }
            public string InvoiceDate { get; set; }
            public decimal TotalAmount { get; set; }
            public decimal PirdAmount { get; set; }

        }
        public ViewPurchasesList()
        {
            InitializeComponent();

            // بيانات تجريبية
            var invoices = new List<Invoice>
            {
                new Invoice { ListNum = "1", InvoiceDate = "2023-10-01", TotalAmount = 1500, PirdAmount = 300},
                new Invoice { ListNum = "2", InvoiceDate = "2023-10-02", TotalAmount = 2000, PirdAmount = 400},
                new Invoice { ListNum = "3", InvoiceDate = "2023-10-03", TotalAmount = 2500, PirdAmount  = 50 },
                new Invoice { ListNum = "4", InvoiceDate = "2023-10-03", TotalAmount = 2500, PirdAmount = 500},
                new Invoice { ListNum = "5", InvoiceDate = "2023-10-03", TotalAmount = 2500, PirdAmount = 500},
                new Invoice { ListNum = "6", InvoiceDate = "2023-10-03", TotalAmount = 2500, PirdAmount = 500},
                new Invoice { ListNum = "7", InvoiceDate = "2023-10-03", TotalAmount = 2500, PirdAmount = 500},
                new Invoice { ListNum = "8", InvoiceDate = "2023-10-03", TotalAmount = 2500, PirdAmount = 500}
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
                    return invoice.ListNum.Contains(SearchTextBox.Text, StringComparison.OrdinalIgnoreCase) ||
                           invoice.InvoiceDate.Contains(SearchTextBox.Text, StringComparison.OrdinalIgnoreCase) ||
                           invoice.TotalAmount.ToString().Contains(SearchTextBox.Text, StringComparison.OrdinalIgnoreCase) ||
                           invoice.PirdAmount.ToString().Contains(SearchTextBox.Text, StringComparison.OrdinalIgnoreCase);

                };
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
                case "رقم القائمة":
                    e.Accepted = invoice.ListNum.ToLower().Contains(filterText);
                    break;
                case "تاريخ القائمة":
                    e.Accepted = invoice.InvoiceDate.ToLower().Contains(filterText);
                    break;

                default:
                    e.Accepted = true;
                    break;
            }
        }

        private void MenuItem_Export_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Details_Click(object sender, RoutedEventArgs e)
        {


            NavigationService?.Navigate(new ctrlPurchesesListDetils());
        }
    }
}
