using BLL;
using Interface.Pages.UserControl;
using Microsoft.Extensions.Configuration;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;


namespace Interface.Pages.AccountingDepartment
{
    public partial class ViewSalesList : Page ,INotifyPropertyChanged
    {
       
        public class Invoice
        {
            public string ListNum { get; set; }
            public string CustomerName { get; set; }
            public string InvoiceDate { get; set; }
            public decimal TotalAmount { get; set; }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; OnPropertyChanged(nameof(IsLoading)); }
        }
        private DispatcherTimer _filterTimer;

        private DataTable? _dtSales;
        private List<Invoice> _allInvoices = new();

        private ObservableCollection<Invoice> _invoices = new();
        public ObservableCollection<Invoice> Invoices
        {
            get => _invoices;
            set { _invoices = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public ViewSalesList()
        {
            InitializeComponent();
            DataContext = this;
            Loaded += ViewSalesList_Loaded;
            _filterTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(300) };
            _filterTimer.Tick += (s, e) => { _filterTimer.Stop(); ApplyFilter(); };

        }

        public async Task LoadDataToDGV()
        {
            IsLoading = true;
            try
            {
                
                _dtSales = await clsSales.GetAllAsync();
                if (_dtSales == null || _dtSales.Rows.Count == 0)
                {
                    MessageBox.Show("لا توجد فواتير لعرضها", "معلومة", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                _allInvoices = _dtSales.AsEnumerable().Select(row => new Invoice
                {
                    ListNum = row["SealeID"].ToString()!,
                    CustomerName = row["PersonName"].ToString()!,
                    TotalAmount = Convert.ToDecimal(row["TotalCost"]),
                    InvoiceDate = Convert.ToDateTime(row["Date"]).ToString("yyyy-MM-dd")
                }).ToList();

                ApplyFilter();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ أثناء تحميل البيانات: {ex.Message}", "خطأ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally 
            { 
                IsLoading = false;
            }
        }

        private void ApplyFilter()
        {

            try
            {
                IsLoading = true;

                // إذا كان صندوق البحث فارغاً، اعرض كل الفواتير بدون تصفية
                if (string.IsNullOrWhiteSpace(SearchTextBox?.Text))
                {
                    Invoices = new ObservableCollection<Invoice>(_allInvoices);
                    return;
                }

                var searchText = SearchTextBox.Text.Trim();
                var filterType = (FilterComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();

                var filtered = _allInvoices.Where(invoice =>
                {
                    return filterType switch
                    {
                        "رقم القائمة" => invoice.ListNum.Contains(searchText, StringComparison.OrdinalIgnoreCase),
                        "تاريخ القائمة" => invoice.InvoiceDate.Contains(searchText),
                        "المبلغ الكلي" => invoice.TotalAmount.ToString().Contains(searchText),
                        "اسم الزبون" => invoice.CustomerName.Contains(searchText, StringComparison.OrdinalIgnoreCase),
                        _ => true
                    };
                }).ToList();

                Invoices = new ObservableCollection<Invoice>(filtered);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                IsLoading = false;
            }
        }


        private async void ViewSalesList_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            await LoadDataToDGV();

        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _filterTimer.Stop(); 
            _filterTimer.Start(); 
           // ApplyFilter();

        }

        private void FilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilter();

        }
      
        private void MenuItem_Details_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var selectedInvoice = (Invoice)InvoiceDataGrid.SelectedItem;

            if (selectedInvoice != null)
            {
                MainGrid.Visibility = Visibility.Collapsed;
                SubGrid.Children.Clear();
                var detailsControl = new ctrlSalesListDetails(int.Parse(selectedInvoice.ListNum),this);
                SubGrid.Children.Add(detailsControl);
                SubGrid.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("يجب تحديد قائمة أولاً!", "تحذير", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void MenuItem_Export_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
       
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
    }
    
}

