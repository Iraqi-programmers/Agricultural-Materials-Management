using BLL;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Documents;

namespace Interface.Pages.UserControl
{
    
    public partial class ctrlSalesListDetails : System.Windows.Controls.UserControl,INotifyPropertyChanged
    {
        public class SaleInfo : INotifyPropertyChanged
        {
            private string _listNum;
            private string _customerName;
            private string _listDate;
            private string _pairdAmount;
            private string _totalCost;

            private string _TotalProfir;

            public string TotalProfir
            {
                get { return _TotalProfir; }
                set { _TotalProfir = value;OnPropertyChanged(); }
            }


            public string ListNum
            {
                get => _listNum;
                set { _listNum = value; OnPropertyChanged(); }
            }

            public string CustomerName
            {
                get => _customerName;
                set { _customerName = value; OnPropertyChanged(); }
            }

            public string ListDate
            {
                get => _listDate;
                set { _listDate = value; OnPropertyChanged(); }
            }

            public string PairdAmount
            {
                get => _pairdAmount;
                set { _pairdAmount = value; OnPropertyChanged(); }
            }

            public string TotalCost
            {
                get => _totalCost;
                set { _totalCost = value; OnPropertyChanged(); }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public class ProductInfo : INotifyPropertyChanged
        {
            private string _prodectType;
            private string _warrantyDate;
            private string _price;
            private string _quantity;
            private string _totalCost;

            private string _Profit ;

            public string Profit
            {
                get { return _Profit ; }
                set { _Profit  = value; OnPropertyChanged(); }
            }


            public string ProdectType
            {
                get => _prodectType;
                set { _prodectType = value; OnPropertyChanged(); }
            }

            public string WarrantyDate
            {
                get => _warrantyDate;
                set { _warrantyDate = value; OnPropertyChanged(); }
            }

            public string Price
            {
                get => _price;
                set { _price = value; OnPropertyChanged(); }
            }

            public string Quantity
            {
                get => _quantity;
                set { _quantity = value; OnPropertyChanged(); }
            }

            public string TotalCost
            {
                get => _totalCost;
                set { _totalCost = value; OnPropertyChanged(); }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private SaleInfo _saleInfo;
        public SaleInfo saleInfo
        {
            get => _saleInfo;
            set { _saleInfo = value; OnPropertyChanged(); }
        }

        private ObservableCollection<ProductInfo> _productInfos = new();
        public ObservableCollection<ProductInfo> ProductInfos
        {
            get => _productInfos;
            set { _productInfos = value; OnPropertyChanged(); }
        }

        private ProductInfo _selectedProduct;
        public ProductInfo SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                OnPropertyChanged();

                // تحديث معلومات المنتج المعروضة عند اختيار صف
                if (value != null)
                {
                    CurrentProduct = new ProductInfo
                    {
                        ProdectType = value.ProdectType,
                        WarrantyDate = value.WarrantyDate,
                        Price = value.Price,
                        Quantity = value.Quantity,
                        TotalCost = value.TotalCost
                    };
                    OnPropertyChanged(nameof(CurrentProduct));
                }
            }
        }
        public ProductInfo CurrentProduct { get; set; }

 
        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsNotLoading));
            }
        }

        public bool IsNotLoading => !IsLoading;

       
        // كلاس لحفظ تفاصيل المنتج المحدد
        public ProductInfo ProductDetails { get; set; }


        public ctrlSalesListDetails(int ListNum)
        {
            InitializeComponent();
            DataContext = this;
            LoadDataAsync(ListNum);

        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void UpdateCustomName(string newName)
        {
            if (CoustemrName.Inlines.Count > 0)
            {
                CoustemrName.Inlines.Clear();
            }

            CoustemrName.Inlines.Add(new Run(newName));
        }

        private async void LoadDataAsync(int listNum)
        {
            try
            {
                IsLoading = true;
                var details = await Task.Run(() => clsSales.GetByIdAsync(listNum));

                if (details != null)
                {
                    await Dispatcher.InvokeAsync(() =>
                    {
                        // تعبئة بيانات القائمة الثابتة
                        saleInfo = new SaleInfo
                        {
                            ListNum = listNum.ToString()!,
                            CustomerName = details.Person!.FullName,
                            ListDate = details.Date.ToString("yyyy-MM-dd"),
                            PairdAmount = details.PaidAmount.ToString()!,
                            TotalCost = details.SaleTotalCost.ToString()
                        };
                        UpdateCustomName(details.Person.FullName);

                        // تعبئة قائمة المنتجات
                        ProductInfos = new ObservableCollection<ProductInfo>(
                            details.SalesDetailsList.Select(row => new ProductInfo
                            {
                                ProdectType = row.Stock.ProductInfo.ProductType.TypeName,
                                WarrantyDate =Convert.ToDateTime(row.WarrantyDate).ToString("yyyy-MM-dd"),
                                Price = row.Price.ToString(),
                                Quantity = row.Quantity.ToString(),
                                TotalCost = row.TotalCost.ToString()
                            }));
                    });
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
        }

        //private async void LoadDataAsync()
        //{
        //    try
        //    {
        //        IsLoading = true;
        //        var details = await Task.Run(() => clsSales.GetByIdAsync(int.Parse(_PropertyInfo.ListNum)));

        //        if (details != null && details.SalesDetailsList.Count > 0)
        //        {
        //            await Dispatcher.InvokeAsync(() =>
        //            {
        //                PropertyInfos = new ObservableCollection<PropertyInfo>(
        //                    details.SalesDetailsList.AsEnumerable().Select(row => new PropertyInfo
        //                    {
        //                        ListNum = row.Id.ToString()!,
        //                        CustomerName = details.Person.FullName!,
        //                        ListDate=details.Date.ToString("yyyyy,MM,dd"),
        //                        PairdAmount=details.PaidAmount.ToString()!,
        //                        TotalCost=details.SaleTotalCost.ToString(),

        //                        ProdectType=row.Stock.ProductInfo.ProductType.TypeName,
        //                        WarrantyDate=Convert.ToDateTime(row.WarrantyDate).ToString("yyyy,mm,dd"),
        //                        Price=row.Price.ToString(),
        //                        Quantity=row.Quantity.ToString(),
        //                        TotalCostBaseOnProdect=row.TotalCost.ToString()


        //                    }));

        //            });

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"خطأ في تحميل البيانات: {ex.Message}");
        //    }
        //    finally
        //    {
        //        IsLoading = false;
        //    }
        //}

        private void CustomerName_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        }

        private void UserInfo_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

