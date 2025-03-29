using BLL;
using Interface.Pages.AccountingDepartment;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;

namespace Interface.Pages.UserControl
{
   
    public partial class ctrlPurchesesListDetils : System.Windows.Controls.UserControl ,INotifyPropertyChanged
    {
        public class PageInfo : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler? PropertyChanged;
            //تفاصيل القائمة
            private string _ListNum;
            private string _supplierName;
            private string _listDate;
            private string _totalListAmount;
            private string _paidAmount;
            private int _userID;
            private string _IsDebt;
            //تفاصيل المنتج
            private string _productType;
            private string _totalPrice;
            private string _price;
            private string _quantity;
            private string _warrantyDate;
            private string _Status;




            public string IsDebt
            {
                get { return _IsDebt; }
                set 
                { 
                    _IsDebt = value;
                    OnPropertyChanged(nameof(IsDebt));
                }
            }

            public string Status
            {
                get { return _Status; }
                set { _Status = value; OnPropertyChanged(nameof(Status)); }
            }

            public string ListNum
            {
                get { return _ListNum; }
                set { _ListNum = value; OnPropertyChanged(nameof(ListNum)); }
            }

            public string ProductType
            {
                get => _productType;
                set
                {
                    _productType = value;
                    OnPropertyChanged(nameof(ProductType));
                }
            }

            public string Price
            {
                get => _price;
                set
                {
                    _price = value;
                    OnPropertyChanged(nameof(Price));
                }
            }

            public string Quantity
            {
                get => _quantity;
                set
                {
                    _quantity = value;
                    OnPropertyChanged(nameof(Quantity));
                }
            }

            public string TotalPrice
            {
                get => _totalPrice;
                set
                {
                    _totalPrice = value;
                    OnPropertyChanged(nameof(TotalPrice));
                }
            }

            public string WarrintyDate
            {
                get => _warrantyDate;
                set
                {
                    _warrantyDate = value;
                    OnPropertyChanged(nameof(WarrintyDate));
                }
            }

          

            public string SupplierName
            {
                get => _supplierName;
                set
                {
                    _supplierName = value;
                    OnPropertyChanged(nameof(SupplierName));
                }
            }

            public string ListDate
            {
                get => _listDate;
                set
                {
                    _listDate = value;
                    OnPropertyChanged(nameof(ListDate));
                }
            }

            public string TotalListAmount
            {
                get => _totalListAmount;
                set
                {
                    _totalListAmount = value;
                    OnPropertyChanged(nameof(TotalListAmount));
                }
            }

            public string ParidAmount
            {
                get => _paidAmount;
                set
                {
                    _paidAmount = value;
                    OnPropertyChanged(nameof(ParidAmount));
                }
            }

            public int UserID
            {
                get => _userID;
                set
                {
                    _userID = value;
                    OnPropertyChanged(nameof(UserID));
                }
            }

            protected void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ObservableCollection<PageInfo> ProdectList { get; set; } = new ObservableCollection<PageInfo>();
       
        private clsPurchases? __Purchases = null;

        private int ? __PurchasesId = null;

        private Page __Page;

        private PageInfo _selectedProduct;

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string? PropName=null)
        {
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(PropName));
        }

        public PageInfo SelectedProduct
        {
            get => _selectedProduct;
            set { _selectedProduct = value; OnPropertyChanged(nameof(SelectedProduct)); }
        }


        public ctrlPurchesesListDetils(int purchasesid,Page parentPage)
        {
            InitializeComponent();
            DataContext = this;
            __PurchasesId = purchasesid;
           __Page= parentPage;
            Loaded += async (s, e) => await LoadListDetils();
        }

        private void UserInfo_MouseDown(object sender, MouseButtonEventArgs e)
        {
           
            MainGrid.Visibility = Visibility.Collapsed;
            SubGrid.Children.Clear();
            SubGrid.Children.Add(new ctrlUserCardInfo(ctrlUserCardInfo.Mod.View, __Purchases?.UserInfo?.Id ?? -1,this));
            SubGrid.Visibility = Visibility.Visible;
        }

        private void SupplierName_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Visibility = Visibility.Collapsed;
            SubGrid.Children.Clear();
            SubGrid.Children.Add(new ctrlSupplierCardInfo(ctrlSupplierCardInfo.Mod.View,__Purchases?.SupplierInfo.Id ?? -1,this));
            SubGrid.Visibility = Visibility.Visible;
        }

        private void ReturnProcess()
        {
            //if (__UserControl != null)
            //{
            //    Grid? subGrid = __UserControl.FindName("SubGrid") as Grid;

            //    if (subGrid != null)
            //    {
            //        subGrid.Visibility = Visibility.Collapsed;
            //        __UserControl.Visibility= Visibility.Visible;
            //    }
            //    else
            //    {
            //        MessageBox.Show("لم يتم العثور على subGrid.");
            //    }
            //}
        }


        private void UpdateSupplierName(string newName)
        {
            if (SupplierName.Inlines.Count > 0)
            {
                SupplierName.Inlines.Clear(); 
            }

            SupplierName.Inlines.Add(new Run(newName));
        }

        private async Task LoadListDetils()
        {
            try
            {
                __Purchases = await clsPurchases.GetByIdAsync(__PurchasesId ?? -1);
                if (__Purchases == null)
                    return;

                ListNum.Text = __Purchases.Id.ToString();
                UpdateSupplierName(__Purchases.SupplierInfo.SupplierName);
                ListDate.Text = __Purchases.Date.ToString();
                TotleListPrice.Text = __Purchases.TotalPrice.ToString();
                TotalPaid.Text = __Purchases.TotalPaid.ToString();
                _selectedProduct.IsDebt = __Purchases.IsDebt ? "نعم" : "لا";

                LoadProdectDetiles();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ أثناء تحميل البيانات: {ex.Message}", "خطأ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadProdectDetiles()
        {

            foreach (var item in __Purchases!.PurchaseDetailsList)
            {
                var productInfo = new PageInfo
                {
                    ProductType = item.Product.ProductType.TypeName,
                    Price = item.Price.ToString(),
                    Quantity = item.Quantity.ToString(),
                    TotalPrice = (item.Quantity * item.Price).ToString(),
                    WarrintyDate = item.WarrantyDate.ToString(),
                    ListNum = item.Id.ToString()!,
                    Status = item.Status
                };

                ProdectList.Add(productInfo);
            }

           // dgvProdect.SetBinding(DataGrid.SelectedItemProperty, new Binding(nameof(SelectedProduct)) { Mode = BindingMode.TwoWay });

        }

        private void DataGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //if (dgvProdect.SelectedItem is PageInfo selectedRow)
            //{
            //    txtProductName.Text = selectedRow.ProductType;
            //    txtProductPrice.Text = selectedRow.Price;
            //    txtQuantity.Text = selectedRow.Quantity;
            //    txtTotalPrice.Text = selectedRow.TotalPrice;
            //    txtWarraintyDate.Text = selectedRow.WarrintyDate;
            //}
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            if(__Page is ViewPurchasesList ViewPage)
            {
                ViewPage.ShowMainPage();
                
            }
        }
    }
}
