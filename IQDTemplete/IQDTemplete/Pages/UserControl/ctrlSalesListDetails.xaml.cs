using BLL;
using Interface.Pages.AccountingDepartment;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Interface.Pages.UserControl
{
    
    public partial class ctrlSalesListDetails : System.Windows.Controls.UserControl,INotifyPropertyChanged
    {
       
        public class SaleInfo : INotifyPropertyChanged
        {
            private string _customerName;
            private string _listDate;
            private string _pairdAmount;
            private string _totalCost;

            private string _TotalProfir;


            private string _listNum;
            public string ListNum
            {
                get => _listNum;
                set { _listNum = value; OnPropertyChanged(nameof(ListNum)); }
            }


            public string TotalProfir
            {
                get { return _TotalProfir; }
                set { _TotalProfir = value;OnPropertyChanged(TotalProfir); }
            }
            public string CustomerName
            {
                get => _customerName;
                set { _customerName = value; OnPropertyChanged(CustomerName); }
            }

            public string ListDate
            {
                get => _listDate;
                set { _listDate = value; OnPropertyChanged(ListDate); }
            }

            public string PairdAmount
            {
                get => _pairdAmount;
                set { _pairdAmount = value; OnPropertyChanged(PairdAmount); }
            }

            public string TotalCost
            {
                get => _totalCost;
                set { _totalCost = value; OnPropertyChanged(TotalCost); }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private SaleInfo? _saleInfo;
        public SaleInfo saleInfo
        {
            get => _saleInfo;
            set { _saleInfo = value; OnPropertyChanged(nameof(saleInfo)); }
        }

        public class ProductInfo : INotifyPropertyChanged
        {
            private string _prodectType;
            private string _warrantyDate;
            private string _price;
            private string _quantity;
            private string _totalCost;
            private string _DetilsId;
            private string _Profit ;
            

            public string DetilsId
            {
                get { return _DetilsId; }
                set { _DetilsId = value; OnPropertyChanged(nameof(DetilsId)); }
            }

            public string Profit
            {
                get { return _Profit ; }
                set { _Profit  = value; OnPropertyChanged(Profit); }
            }

            public string ProdectType
            {
                get => _prodectType;
                set { _prodectType = value; OnPropertyChanged(ProdectType); }
            }

            public string WarrantyDate
            {
                get => _warrantyDate;
                set { _warrantyDate = value; OnPropertyChanged(WarrantyDate); }
            }

            public string Price
            {
                get => _price;
                set { _price = value; OnPropertyChanged(Price); }
            }

            public string Quantity
            {
                get => _quantity;
                set { _quantity = value; OnPropertyChanged(Quantity); }
            }

            public string TotalCost
            {
                get => _totalCost;
                set { _totalCost = value; OnPropertyChanged(TotalCost); }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        private ObservableCollection<ProductInfo>? _productInfos = new();
        public ObservableCollection<ProductInfo> ProductInfos
        {
            get => _productInfos;
            set { _productInfos = value; OnPropertyChanged(nameof(ProductInfos)); }
        }

        //لحفظ البيانات المحدده من الداتا جرد فيو
        private ProductInfo? _selectedProduct;
        public ProductInfo SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));

                // تحديث معلومات المنتج المعروضة عند اختيار صف
                if (value != null)
                {
                    CurrentProduct = new ProductInfo
                    {
                        ProdectType = value.ProdectType,
                        WarrantyDate = value.WarrantyDate,
                        Price = value.Price,
                        Quantity = value.Quantity,
                        TotalCost = value.TotalCost,
                        Profit = value.Profit,
                        DetilsId= value.DetilsId,
                        
                        
                    };
                    OnPropertyChanged(nameof(CurrentProduct));
                }
            }
        }
        
        //لتعبة الاوبجكت من الداتا جرد فيو وعرضها في التيكست بلوك
        private ProductInfo? _currentProduct;
        public ProductInfo CurrentProduct
        {
            get => _currentProduct;
            set { _currentProduct = value; OnPropertyChanged(nameof(CurrentProduct)); }
        }

        //لاظهار شريط التحميل
        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }
      

        private int? _Userid {  get; set; }
        private int _listNum;
        public int ListNum
        {
            get => _listNum;
            set
            {
                _listNum = value;
                OnPropertyChanged(nameof(ListNum));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
      
        private Page page {  get; set; }

        public ctrlSalesListDetails(int ListNum,Page Page)
        {
            InitializeComponent();
            DataContext = this;
            page = Page;
            this.ListNum = ListNum;
            LoadDataAsync();
            

        }


        private void UpdateCustomName(string newName)
        {
            if (CoustemrName.Inlines.Count > 0)
            {
                CoustemrName.Inlines.Clear();
            }

            CoustemrName.Inlines.Add(new Run(newName));
        }

        public async void LoadDataAsync()
        {
            try
            {
                IsLoading = true;
                var details = await Task.Run(() => clsSales.GetByIdAsync(ListNum));

                if (details != null)
                {
                    _Userid = details.UserInfo!.Id;
                    saleInfo =new SaleInfo()
                    {
                        CustomerName = details.Person?.FullName ?? "غير محدد",
                        ListDate = details.Date.ToString("yyyy-MM-dd"),
                        PairdAmount = details.PaidAmount?.ToString() ?? "0",
                        TotalCost = details.SaleTotalCost.ToString(),
                        TotalProfir = details.TotalProfit.ToString(),
                        ListNum=details.Id.ToString()!
                    };
                    UpdateCustomName(details.Person?.FullName ?? "غير محدد");

                    ProductInfos = new ObservableCollection<ProductInfo>(
                        details.SalesDetailsList.Select(row => new ProductInfo
                        {
                            ProdectType = row.Stock.ProductInfo.ProductType.TypeName,
                            WarrantyDate = row.WarrantyDate?.ToString("yyyy-MM-dd") ?? "غير محدد",
                            Price = row.Price.ToString(),
                            Quantity = row.Quantity.ToString(),
                            TotalCost = row.TotalCost.ToString(),
                            Profit = row.Profit.ToString(),
                            DetilsId=row.Id.ToString()!
                            
                            
                        }));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في تحميل البيانات: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
            IsLoading = false;
        }

        private void CustomerName_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        }

        private void UserInfo_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MainGrid.Visibility = Visibility.Collapsed;
            SubGrid.Children.Clear();
            SubGrid.Children.Add(new ctrlUserCardInfo(ctrlUserCardInfo.Mod.View,_Userid??-1,this));
            SubGrid.Visibility= Visibility.Visible;
        }

        private async void btnClose_Click(object sender, RoutedEventArgs e)
        {
            if (page is ViewSalesList View) 
            {
                await View.LoadDataToDGV();
                View.SubGrid.Visibility = Visibility.Collapsed;
                View.MainGrid.Visibility= Visibility.Visible;
            }
        }

       
    }
}

