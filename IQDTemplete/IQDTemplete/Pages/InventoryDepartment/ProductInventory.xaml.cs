using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Interface.Pages.InventoryDepartment
{
    /// <summary>
    /// Interaction logic for ProductInventory.xaml
    /// </summary>
    /// 

    public class Product
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Status { set; get; }
        public int Quantity { set; get; }
        public decimal TotalPrice { set; get; }

    }


    public class MainViewModel
    {
        public ObservableCollection<Product> Products { get; set; }

        public MainViewModel()
        {
            Products = new ObservableCollection<Product>
        {
            new Product { Id = 1, Name = "Laptop",Status = "New", Quantity = 20 , TotalPrice = 900},
            new Product { Id = 2, Name = "Mouse", Status = "Used", Quantity = 10, TotalPrice = 1500 }
        };
        }
    }


    public partial class ProductInventory : Page
    {
        public MainViewModel viewModel { get; set; }
        public ProductInventory()
        {
            InitializeComponent();
            viewModel = new MainViewModel();
            this.DataContext = viewModel;
            LoadDgvWithDataExpirment();
        }

        public void LoadDgvWithDataExpirment()
        {
            for (int i = 0; i < 10; i++)
            {
                viewModel.Products.Add(new Product { Id = i, Name = "Laptop", Status = "New", Quantity = 20, TotalPrice = 900 + i + 20});
                viewModel.Products.Add(new Product { Id = i, Name = "Mouse", Status = "Used", Quantity = 10, TotalPrice = 1500 + i + 55});
            }


        }

        private void MenuItem_Export_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Details_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SearchTextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

       
    }


}
