using System.Windows;

namespace Interface.Pages.UserControl
{
    
    public partial class ctrlSalesListDetails : System.Windows.Controls.UserControl
    {
        public ctrlSalesListDetails()
        {
            InitializeComponent();

        }

        private void CustomerName_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MessageBox.Show("جاسم");
        }

        private void UserInfo_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
    }
}

