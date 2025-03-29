using BLL;
using Interface.Helper;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace Interface.Pages.UserControl
{
    /// <summary>
    /// Interaction logic for ctrlPersonCardInfo.xaml
    /// </summary>
    public partial class ctrlPersonCardInfo : System.Windows.Controls.UserControl , INotifyPropertyChanged
    {

        public enum Mod { AddNew,Update,View}
        public Mod _mod;

        private string? __title;
        public string? Title
        {
            get { return __title; }
            set 
            { 
                __title = value;
                OnPropertyChanged(nameof(Title));


            }
        }

        private Visibility __visibalBtnSave;
        public Visibility visibalBtnSave
        {
            get { return __visibalBtnSave; }
            set
            {
                __visibalBtnSave = value;
                OnPropertyChanged();
            }
        }

        private Visibility __visibalBtnEdit;
        public Visibility visibalBtnEdit
        {
            get { return __visibalBtnEdit; }
            set
            {
                __visibalBtnEdit = value;
                OnPropertyChanged();

            }
        }


        private int? __PersonId = null;
        

        private clsPerson? person = null;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public ctrlPersonCardInfo(Mod mod,int? personId)
        {
            InitializeComponent();
            DataContext = this;
            __PersonId = personId;
            _mod = mod;
        }

       
        private void ChangeTextVisblte()
        {
            if (_mod == Mod.View) 
            {
                txtAddress.IsReadOnly = true;
                txtName.IsReadOnly = true;
                txtNationalID.IsReadOnly = true;
                txtPersonID.IsReadOnly = true;
                txtPhoneNumber.IsReadOnly = true;
                return;
            }

            txtAddress.IsReadOnly = false;
            txtName.IsReadOnly = false;
            txtNationalID.IsReadOnly = false;
            txtPersonID.IsReadOnly = true;
            txtPhoneNumber.IsReadOnly = false;

        }
       
        private void ChangeProparteUI(Visibility btnSave = Visibility.Visible ,Visibility btnEdit = Visibility.Hidden, string title="اضافة شخص")
        {
            visibalBtnEdit = btnEdit;
            visibalBtnSave = btnSave;
            Title = title;
            ChangeTextVisblte();
        }

        private async Task LoadData()
        {
            if (__PersonId == null || __PersonId == -1)
                return;
            person = await clsPerson.GetByIdAsync(__PersonId ?? -1);
            if (person == null)
                return;

            txtPersonID.Text = person?.Id.ToString();
            txtName.Text = person?.FullName;
            txtPhoneNumber.Text = person?.PhoneNumber;
            txtNationalID.Text = person?.NationalNum;
            txtAddress.Text = person?.Address;
        }

        public  async Task ChooseMode()
        {

            switch(_mod)
            {
                case Mod.AddNew:
                    {
                        ChangeProparteUI();
                        person = new clsPerson(txtName.Text);
                    }
                    break; 

                case Mod.Update:
                    {
                        ChangeProparteUI(title:"تعديل بيانات");
                       await LoadData();
                    }
                    break;

                case Mod.View:
                    {
                        ChangeProparteUI(Visibility.Hidden, Visibility.Visible, "عرض معلومات الشخص");
                       await LoadData();
                    }
                    break;
                   
            }
        }

        private bool IsDataUnchanged()
        {
            return txtName.Text == person?.FullName &&
                   txtNationalID.Text == person?.NationalNum &&
                   txtPhoneNumber.Text == person?.PhoneNumber &&
                   txtAddress.Text == person?.Address;
        }

        public async void btnSave_Click(object sender, RoutedEventArgs e)
        {

            if (!clsValidationHelper.IsNotEmpty(txtName.Text))
            {
                MessageBox.Show("الرجاء إدخال الاسم!");
                return;
            }

          
            if (IsDataUnchanged())
            {
                MessageBox.Show("لا يوجد أي تغيير لحفظه!");
                return;
            }

            
            if (await person!.SaveAsync())
            {
                person = new clsPerson(txtName.Text, txtNationalID.Text, txtPhoneNumber.Text, txtAddress.Text);
                MessageBox.Show("تم الحفظ بنجاح");
            }
            else
            {
                MessageBox.Show("حدث خطأ أثناء الحفظ!");
                return;
            }


        }

        public  void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            btnSave.Visibility = Visibility.Visible;
            btnEdit.Visibility= Visibility.Collapsed;
            _mod = Mod.Update;
           
            
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadData();
            await ChooseMode();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
