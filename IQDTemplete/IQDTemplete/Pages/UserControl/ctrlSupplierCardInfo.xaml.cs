using System;
using System.Collections.Generic;
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
    /// This User Control Using To Showing Supplier Info Or Add New Supplier OR Edit
    /// </summary>
    public partial class ctrlSupplierCardInfo : System.Windows.Controls.UserControl
    {
        public enum Mod { AddNew,Update,View}
        public Mod _mod;

        private string __TitleName;
        public string titleName
        {
            get { return __TitleName; }
            set { __TitleName = value; }
        }

        private Visibility __visibalBtnSave;
        public Visibility visibalBtnSave
        {
            get { return __visibalBtnSave; }
            set { __visibalBtnSave = value; }
        }

        private Visibility __visibalBtnEdit;
        public Visibility visibalBtnEdit
        {
            get { return __visibalBtnEdit; }
            set { __visibalBtnEdit = value; }
        }



        public ctrlSupplierCardInfo()
        {
            InitializeComponent();
        }

        public ctrlSupplierCardInfo(string titleName,Visibility visableBtnSave, Visibility visableBtnEdit, Mod mod)
        {
            InitializeComponent();
            __TitleName = titleName;
            __visibalBtnEdit = visableBtnEdit;
            __visibalBtnSave = visableBtnSave;
            _mod = mod;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
