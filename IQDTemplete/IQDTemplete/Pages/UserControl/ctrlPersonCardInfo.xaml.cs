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
        public string Title
        {
            get { return __title; }
            set 
            { 
                __title = value;
                OnPropertyChanged();


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

        private bool __ReadOnly;
        public bool ReadOnly
        {
            get { return __ReadOnly; }
            set
            {
                __ReadOnly = value;
                OnPropertyChanged();
            }
        }

        //براميتر البيرسون ايدي في دالة الجيت يجب ان يكون من نوع null
        private int? __PersonId;
        public int? PersonId
        {
            get { return __PersonId; }
            set { __PersonId = value; }
        }

        private clsPerson? person;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public ctrlPersonCardInfo(Mod mod,int? personId)
        {
            InitializeComponent();
            DataContext = this;
            __PersonId = personId;
            _mod = mod;
            Task.Run(() => ChooseMod());
        }

        public ctrlPersonCardInfo()
        {
            InitializeComponent();

        }

        private void ChangeProparteUI(Visibility btnSave = Visibility.Visible ,Visibility btnEdit = Visibility.Hidden, string title="اضافة شخص" , bool ReadOnly=false)
        {
            visibalBtnEdit = btnEdit;
            visibalBtnSave = btnSave;
            Title = title;
            this.ReadOnly = ReadOnly;
        }

        private async Task LoadData()
        {
            person = await clsPerson.GetByIdAsync(1);

            txtPersonID.Text = person.Id.ToString();
            txtName.Text = person.FullName;
            txtPhoneNumber.Text = person.PhoneNumber;
            txtNationalID.Text = person.NationalNum;
            txtAddress.Text = person.Address;
        }

        private async Task ChooseMod()
        {

            switch(_mod)
            {
                case Mod.AddNew:
                    {
                        ChangeProparteUI();
                    }
                    break; 

                case Mod.Update:
                    {
                       await LoadData();
                        ChangeProparteUI(title:"تعديل بيانات");
                       
                        if (person == null)
                            return;
                    }
                    break;

                case Mod.View:
                    {
                        await LoadData();
                        ChangeProparteUI(Visibility.Hidden, Visibility.Visible, "عرض معلومات الشخص",true);
                    }
                    break;
                   
            }
        }


        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            person = new clsPerson(txtName.Text, txtNationalID.Text, txtPhoneNumber.Text, txtAddress.Text);

            if (clsValidationHelper.IsNotEmpty(txtName.Text))
            {
                if (await person.SaveAsync())
                {
                    MessageBox.Show("تم الحفظ بنجاح");
                }
                else
                    MessageBox.Show("حدث خطـأ!!");
            }


        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            ChangeProparteUI(title:"تعديل البيانات");
            _mod = Mod.Update;
        }

       
    }
}
