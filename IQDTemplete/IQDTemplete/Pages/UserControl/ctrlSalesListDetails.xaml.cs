namespace Interface.Pages.UserControl
{
    /// <summary>
    /// Interaction logic for ctrlSalesListDetails.xaml
    /// </summary>
    public partial class ctrlSalesListDetails : System.Windows.Controls.UserControl
    {
        // نموذج بيانات تفاصيل القائمة
        public class InvoiceDetails
        {
            public string InvoiceNumber { get; set; }
            public string StoreNumber { get; set; }
            public string WarrantyDate { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }
            public decimal TotalAmount { get; set; }
            public string InvoiceDate { get; set; }
            public string CustomerName { get; set; }
            public string NationalID { get; set; }
            public string UserName { get; set; }
        }
        public ctrlSalesListDetails()
        {
            InitializeComponent();
            DataContext = this;

        }

        // خاصية لعرض تفاصيل القائمة
        public InvoiceDetails InvoiceDetail { get; set; }

        // خاصية لعرض القوائم الفرعية
        public System.Collections.ObjectModel.ObservableCollection<SubInvoice> SubInvoices { get; set; }


       

        // نموذج بيانات القوائم الفرعية
        public class SubInvoice
        {
            public string InvoiceNumber { get; set; }
            public string Date { get; set; }
            public string UserName { get; set; }
        }
    }
}

