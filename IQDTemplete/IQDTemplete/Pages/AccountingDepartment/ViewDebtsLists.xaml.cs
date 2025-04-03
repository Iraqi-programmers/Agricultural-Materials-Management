using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for ViewDebtsLists.xaml
    /// </summary>
    public partial class ViewDebtsLists : Page ,INotifyPropertyChanged
    {
        public class DebtsInfo : INotifyPropertyChanged
        {
            private string _DebtsID;
            private string _PersonID;
            private string _TotalAmount;
            private string _DebtsPymentDate;
            private string _IsHeDebets;


            public string IsHeDebets
            {
                get { return _IsHeDebets; }
                set
                {
                    _IsHeDebets = value;
                    OnPropertyChang(nameof(IsHeDebets));
                }
            }

            public string DebtsPymentDate
            {
                get { return _DebtsPymentDate; }
                set
                {
                    _DebtsPymentDate = value;
                    OnPropertyChang(nameof(DebtsPymentDate));
                }
            }

            public string TotalAmount
            {
                get { return _TotalAmount; }
                set
                {
                    _TotalAmount = value;
                    OnPropertyChang(nameof(TotalAmount));
                }
            }

            public string DebtsID
            {
                get { return _DebtsID; }
                set 
                { 
                    _DebtsID = value;
                    OnPropertyChang(nameof(DebtsID));
                }
            }

            public string PersonID
            {
                get { return _PersonID; }
                set
                {
                    _PersonID = value;
                    OnPropertyChang(nameof(PersonID));
                }
            }

            protected void OnPropertyChang(string? propertyName=null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            public event PropertyChangedEventHandler? PropertyChanged;
        }

        public DebtsInfo? debts { get; set; }

        public ObservableCollection<DebtsInfo> ObsFillData = new();

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChang(string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ViewDebtsLists()
        {
            InitializeComponent();
            DataContext = this;
            ObsFillData.Add(new DebtsInfo()
            {
                DebtsID="1",
                PersonID="احمد محمود",
                TotalAmount="20,000",
                DebtsPymentDate="2025/2/23"
            });

            InvoiceDataGrid.ItemsSource = ObsFillData;
        }


        private void FilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

       
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Send_Notficton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
