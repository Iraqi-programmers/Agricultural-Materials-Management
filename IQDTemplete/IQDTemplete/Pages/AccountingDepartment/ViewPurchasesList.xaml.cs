using BLL;
using Interface.Pages.UserControl;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Interface.Pages.AccountingDepartment
{
    /// <summary>
    /// Interaction logic for ViewPurchasesList.xaml
    /// </summary>
    public partial class ViewPurchasesList : Page ,INotifyPropertyChanged
    {
        public class ShoppingList
        {
            public string ListNum { get; set; }
            public string InvoiceDate { get; set; }
            public string SupplierName { get; set; }
            public decimal TotalAmount { get; set; }
            public decimal PirdAmount { get; set; }

        }

        private List<ShoppingList> _allShoppingLists = new(); 
        public ObservableCollection<ShoppingList> ShoppingLists { get; set; } = new();
        private clsPurchases? __Purchases = null;
        private bool _isLoaded = false;


        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChange(string? Prop=null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Prop));
        }
        private Visibility _VisiblteText;
        public Visibility VisibltyText
        {
            get { return _VisiblteText; }
            set { _VisiblteText = value;
                OnPropertyChange(nameof(VisibltyText)); }
        }


        public ViewPurchasesList()
        {
            InitializeComponent();
            DataContext = this; 
            Loaded += OnPageLoaded;  
        }

        private async Task LoadDataToDGV()
        {
            try
            {
                var dtPurchases = await clsPurchases.GetAllAsync();

                if (dtPurchases == null || dtPurchases.Rows.Count == 0)
                {
                    MessageBox.Show("لا توجد قوائم لعرضها!!", "رسالة");
                    return;
                }

                _allShoppingLists = dtPurchases.AsEnumerable().Select(row => new ShoppingList
                {
                    ListNum = row["PurchaseID"].ToString()!,
                    InvoiceDate = Convert.ToDateTime(row["PurchaseDate"]).ToString("yyyy-MM-dd"),
                    SupplierName = row["SupplierName"].ToString()!,
                    TotalAmount = Convert.ToDecimal(row["TotalPrice"]),
                    PirdAmount = Convert.ToDecimal(row["TotalPaid"])
                }).ToList();

                ShoppingLists.Clear();
                _allShoppingLists.ForEach(ShoppingLists.Add);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ أثناء تحميل البيانات: {ex.Message}", "خطأ",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        public void ShowMainPage()
        {
            SubGrid.Children.Clear(); // إزالة الـ UserControl
            SubGrid.Visibility = Visibility.Collapsed;
            MainGrid.Visibility = Visibility.Visible;
        }
        
        private void ApplyFilter()
        {
            if (!_isLoaded || SearchTextBox == null || FilterComboBox == null)
                return;
            try
            {

                var searchText = SearchTextBox.Text?.ToLower() ?? string.Empty;
                var filterType = (FilterComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();

                if (string.IsNullOrEmpty(searchText)&&filterType=="") 
                {
                    ShoppingLists.Clear();
                    _allShoppingLists.ForEach(ShoppingLists.Add);
                    return;
                }

                var filtered = _allShoppingLists.Where(item =>
                {
                    switch (filterType)
                    {
                        case "رقم القائمة":
                            return item.ListNum.ToLower().Contains(searchText);
                        case "تاريخ القائمة":
                            return item.InvoiceDate.Contains(searchText);
                        case "اسم المورد":
                            return item.SupplierName.ToLower().Contains(searchText);
                        
                        case "القوائم المدانة":
                            SearchTextBox.ToolTip = "ابحث باستخدام رقم القائمة";
                            return item.TotalAmount > item.PirdAmount &&
                                  (string.IsNullOrEmpty(searchText) ||
                                   item.ListNum.Contains(searchText));

                        case "القوائم المكتملة":
                            SearchTextBox.ToolTip = "ابحث باستخدام رقم القائمة";
                            return item.TotalAmount == item.PirdAmount &&
                                  (string.IsNullOrEmpty(searchText) ||
                                   item.ListNum.Contains(searchText));

                        default:
                            return true;
                    }
                }).ToList();

                ShoppingLists.Clear();
                filtered.ForEach(ShoppingLists.Add);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ في التصفية: {ex.Message}");

            }
        }

        
        private void OnPageLoaded(object sender, RoutedEventArgs e)
        {
            _isLoaded = true;
            DataContext = this;
            LoadDataToDGV().ConfigureAwait(false);
        }

        private void MenuItem_Export_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("ستتوفر هذه الميزة قريبا..", "قريبا", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void MenuItem_Details_Click(object sender, RoutedEventArgs e)
        {
            if (InvoiceDataGrid.SelectedItem is ShoppingList selectedItem)
            {
                int purchaseID = int.Parse(selectedItem.ListNum);
                MainGrid.Visibility = Visibility.Collapsed;
                SubGrid.Children.Add(new ctrlPurchesesListDetils(purchaseID, this));
                SubGrid.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("الرجاء تحديد قائمة أولاً!", "تنبيه", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilter();
        }

        private void FilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //string filterType = (FilterComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString()!;
            
            //switch (filterType)
            //{
            //    case "رقم القائمة":
            //        VisibltyText = Visibility.Visible;
            //        break;
            //    case "تاريخ القائمة":
            //        VisibltyText = Visibility.Visible;
            //        break;
            //    case "اسم المورد":
            //        VisibltyText = Visibility.Visible;
            //        break;
            //    case "القوائم المدانة":
            //        VisibltyText = Visibility.Hidden;
            //        break;
            //    case "القوائم المكتملة":
            //        VisibltyText = Visibility.Hidden;
            //        break;
            //    default:
            //        Debug.WriteLine($"قيمة غير متوقعة: {filterType}");
            //        break;
            //}

            ApplyFilter();
        }

    }
}
