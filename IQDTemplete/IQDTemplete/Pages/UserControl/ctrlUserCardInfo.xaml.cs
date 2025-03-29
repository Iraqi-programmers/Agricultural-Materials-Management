using BLL;
using Interface.Helper;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Interface.Pages.UserControl
{
    /// <summary>
    /// Interaction logic for ctrlUserCardInfo.xaml
    /// </summary>
    public partial class ctrlUserCardInfo : System.Windows.Controls.UserControl ,INotifyPropertyChanged
    {
       public enum Mod { AddNew,Update,View};
       public Mod mod;

        private ctrlPersonCardInfo? ctrlPersonCard {  get; set; }

        private int? __UserId = null;

        private int? __PersonId;

        private clsUsers? __UserInfo = null;

        private System.Windows.Controls.UserControl __PurchesesListDetils;


        private string? __title;
        public string?  Title
        {
            get { return __title; }
            set 
            { 
                __title = value;
                OnPropertyChanged();
            }
        }

            
        private Visibility __visibleBtnEdit;
        public Visibility VisibleBtnEdit
        {
            get { return __visibleBtnEdit; }
            set 
            { __visibleBtnEdit = value;
                OnPropertyChanged();
            }
        }

        
        private Visibility __VisblteBtnSave;
        public Visibility VisibleBtnSave
        {
            get { return __VisblteBtnSave; }
            set 
            { 
                __VisblteBtnSave = value;
                OnPropertyChanged();
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public ctrlUserCardInfo(Mod mod,int userId, System.Windows.Controls.UserControl purchesesListDetils)
        {
            InitializeComponent();
            DataContext = this;
            __UserId = userId;
            this.mod = mod;
            __PurchesesListDetils = purchesesListDetils;
        }

        private void LoadPersonCardUI()
        {
            var mode = mod == Mod.View ? ctrlPersonCardInfo.Mod.View :
            mod == Mod.Update ? ctrlPersonCardInfo.Mod.Update :
                                ctrlPersonCardInfo.Mod.AddNew;

            ctrlPersonCard = new ctrlPersonCardInfo(mode, __PersonId);


            ctrlPersonCard.btnSave.Visibility = Visibility.Collapsed;
            ctrlPersonCard.btnEdit.Visibility = Visibility.Collapsed;
            ctrlPersonCard.btnClose.Visibility = Visibility.Collapsed;
            PersonInfoGrid.Children.Add(ctrlPersonCard);
            PersonInfoGrid.Visibility = Visibility.Visible;

        }
       
        private async Task LoadUserCradInfo()
        {
            if (mod == Mod.View || mod == Mod.Update)
            {
                if (__UserId == -1 || __UserId == null)
                    return;

                __UserInfo = await clsUsers.GetByIdAsync(__UserId ?? -1);
                
                if (__UserInfo == null)
                    return;

                txtUsername.Text = __UserInfo.UserName;
                txtPassword.Password = __UserInfo.Password;
                __PersonId = __UserInfo?.Person?.Id;
                return;
            }
            __UserInfo = new clsUsers(txtUsername.Text,txtPassword.Password,new clsPerson(""));

           

        }

        private void ChangeVisblteText()
        {
            if(mod==Mod.View)
            {
                txtPassword.IsEnabled = false;
                txtUsername.IsReadOnly = true;
                return;
            }
            txtUsername.IsReadOnly = false;
            txtPassword.IsEnabled = true;
        }

        private void ChangeProperties(Visibility btnSave,Visibility btnEdit,string Title)
        {
            VisibleBtnSave=btnSave;
            VisibleBtnEdit=btnEdit;
            this.Title = Title;
            ChangeVisblteText();
        }

        private async Task ModUIView()
        {
            await LoadUserCradInfo();

            switch (mod)
            {
                case Mod.AddNew:
                    {
                        ChangeProperties(Visibility.Visible, Visibility.Collapsed, "اضافة مستخدم جديد");
                    }
                    break;
                
                case Mod.Update:
                    {
                        ChangeProperties(Visibility.Visible, Visibility.Collapsed, "تعـديل معلومات المستخـدم");

                    }
                    break;
                
                case Mod.View:
                    {
                        ChangeProperties(Visibility.Collapsed, Visibility.Visible, "معـلـومات المستخدم");

                    }
                    break;
            }
        }


        private async void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            btnEdit.Visibility = Visibility.Collapsed;
            btnSave.Visibility = Visibility.Visible;
            mod = Mod.Update;
            await ModUIView();
            ctrlPersonCard!._mod = ctrlPersonCardInfo.Mod.Update;
            await ctrlPersonCard.ChooseMode();

        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!clsValidationHelper.IsNotEmpty(txtUsername.Text) ||
                !clsValidationHelper.IsNotEmpty(txtPassword.Password))
            {
                MessageBox.Show("الرجاء إدخال اسم المستخدم وكلمة المرور.", "تنبيه",MessageBoxButton.OK,MessageBoxImage.Warning);
                return;
            }

            if (__UserInfo == null) return;

            bool isUpdated = txtUsername.Text != __UserInfo.UserName ||
                             txtPassword.Password != __UserInfo.Password;

            if (isUpdated)
            {
                __UserInfo.UserName = txtUsername.Text;
                __UserInfo.Password = txtPassword.Password;

                if (await __UserInfo.SaveAsync())
                {
                    MessageBox.Show(mod == Mod.AddNew ? "تم الإضافة بنجاح" : "تم التحديث بنجاح", "تم");
                }
                else
                {
                    MessageBox.Show("لم يتم حفظ البيانات!!", "خطأ");
                }
            }

            ctrlPersonCard?.btnSave_Click(sender, e);

        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadPersonCardUI();
            await ModUIView();  

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility= Visibility.Collapsed;
            if(__PurchesesListDetils is ctrlPurchesesListDetils ctrlPurchesesListDetils)
            {
                ctrlPurchesesListDetils.MainGrid.Visibility= Visibility.Visible;
                ctrlPurchesesListDetils.SubGrid.Visibility = Visibility.Collapsed;

            }
        }
    }
}
