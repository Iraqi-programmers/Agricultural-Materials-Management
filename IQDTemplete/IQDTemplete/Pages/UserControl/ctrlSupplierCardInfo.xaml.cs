using BLL;
using System;
using System.Collections.Generic;
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

namespace Interface.Pages.UserControl
{
    /// <summary>
    /// This User Control Using To Showing Supplier Info Or Add New Supplier Or Edit
    /// </summary>
    public partial class ctrlSupplierCardInfo : System.Windows.Controls.UserControl ,INotifyPropertyChanged
    {
        public enum Mod { AddNew,Update,View}
        public Mod _mod;

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
       

        private string? __TitleName;
        public string titleName
        {
            get { return __TitleName; }
            set 
            { 
                __TitleName = value;
                OnPropertyChanged(nameof(titleName));
            }
        }

        private Visibility __visibalBtnSave;
        public Visibility visibalBtnSave
        {
            get { return __visibalBtnSave; }
            set 
            {
                __visibalBtnSave = value;
                OnPropertyChanged(nameof(visibalBtnSave));
            }
        }

        private Visibility __visibalBtnEdit;
        public Visibility visibalBtnEdit
        {
            get { return __visibalBtnEdit; }
            set 
            {
                __visibalBtnEdit = value;
                OnPropertyChanged(nameof(visibalBtnEdit));
            }
        }

        private int? __SupllierId = null;

        private clsSupplier? __supplier = null;

        private System.Windows.Controls.UserControl? _userControl;

        public ctrlSupplierCardInfo(Mod mod, int supllierID, System.Windows.Controls.UserControl userControl = null)
        {
            InitializeComponent();
            DataContext = this;
            _mod = mod;
            __SupllierId = supllierID;
            _userControl = userControl;
            MoodView();
        }

       
        private void EnableTextBox()
        {
            switch (_mod)
            {
                case Mod.View:
                    {
                        txtAddress.IsReadOnly = true;
                        txtPhoneNumber.IsReadOnly = true;
                        txtSupplierName.IsReadOnly = true;
                        chkIsPerson.IsEnabled = false;
                    }
                    break;

                default:
                    {
                        txtAddress.IsReadOnly = false;
                        txtPhoneNumber.IsReadOnly = false;
                        txtSupplierName.IsReadOnly = false;
                        chkIsPerson.IsEnabled = true;
                    }
                    break;
            }
           
        }

        private void ReturnProcess()
        {
            if (_userControl != null)
            {
                Grid? subGrid = _userControl.FindName("SubGrid") as Grid;
                Grid? MainGrid = _userControl.FindName("MainGrid") as Grid;

                if (subGrid != null)
                {
                    subGrid.Visibility = Visibility.Collapsed;
                    MainGrid.Visibility= Visibility.Visible;
                }
                else
                {
                    MessageBox.Show("لم يتم العثور على subGrid.");
                }
            }
        }
       
        private void MoodView()
        {
            switch(_mod)
            {
                case Mod.AddNew:
                    {
                        ProprteChange(" اضـافـة الـمـورد");
                        EnableTextBox();

                    }
                    break;

                case Mod.Update:
                    {
                        ProprteChange("تحديث معلومات المورد");
                        EnableTextBox();

                    }
                    break;

                case Mod.View:
                    {
                        ProprteChange("معـلـومـات المـورد", Visibility.Collapsed, Visibility.Visible);
                        EnableTextBox();

                    }
                    break;
            }

        }
        
        private void ProprteChange(string title,Visibility btnSave=Visibility.Visible,Visibility btnEdit = Visibility.Collapsed)
        {
            titleName = title;
            visibalBtnEdit = btnEdit;
            visibalBtnSave = btnSave;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            _mod = Mod.Update;
            MoodView();
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtAddress.Text == __supplier.Address && txtSupplierName.Text == __supplier.SupplierName && txtPhoneNumber.Text == __supplier.Phone && chkIsPerson.IsChecked == __supplier.IsPerson)
            {
                ReturnProcess();
                return;
            }

            __supplier.Address = txtAddress.Text;
            __supplier.SupplierName = txtSupplierName.Text;
            __supplier.Phone= txtPhoneNumber.Text;
            __supplier.IsPerson = chkIsPerson.IsChecked ?? false;


            if (await __supplier.SaveAsync())
            {
                if (_mod == Mod.AddNew)
                {
                    MessageBox.Show("تم الاضــافة بنجاح", "تمت الاضافة");
                    return;

                }
                else if (_mod == Mod.Update)
                {
                    MessageBox.Show("تم تحديث المعلومات بنجاح", "العملية تمت");
                    return;
                }
                ReturnProcess();
            }
            else
                MessageBox.Show("العملية فشلت ,لم يتم الحفظ بنجاح!!", "خطأ", MessageBoxButton.OK, MessageBoxImage.Error);

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            ReturnProcess();
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (_mod == Mod.View ||_mod==Mod.Update && __SupllierId == null || __SupllierId == -1)
                return;
            else
            {

                if (_mod == Mod.AddNew)
                {
                    __supplier = new clsSupplier(txtSupplierName.Text, txtPhoneNumber.Text, chkIsPerson.IsChecked ?? false, txtAddress.Text);
                    return;
                }

                __supplier = await clsSupplier.GetByIdAsync(__SupllierId ?? -1);

                if (__supplier == null)
                    return;

                txtSupplierName.Text = __supplier.SupplierName;
                txtSupplierID.Text = __SupllierId.ToString();
                txtAddress.Text= __supplier.Address;
                txtPhoneNumber.Text = __supplier.Phone;
                chkIsPerson.IsChecked = __supplier.IsPerson;

            }
        }
    }
}
