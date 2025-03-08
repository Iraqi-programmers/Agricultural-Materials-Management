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
using Interface.Properties;
using IQDTemplete.Languages;
using IQDTemplete.Themes;
using Microsoft.Data.SqlClient;
using Microsoft.Win32;


namespace IQDTemplete.Pages
{
    /// <summary>
    /// Interaction logic for Setting1.xaml
    /// </summary>
    public partial class Setting1 : Page
    {
        private string __backupPath { get; set; } = string.Empty;

        public Setting1()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (Settings.Default.Language == "EN")
            {
                rdEnglish.IsChecked = true;
            }
            else
            {
                rdArabic.IsChecked = true;
            }
            if (Settings.Default.Theme == "LightTheme")
            {
                rdLight.IsChecked = true;
            }
            else
            {
                rdDark.IsChecked = true;
            }
        }

        private void rdLight_Checked(object sender, RoutedEventArgs e)
        {
            ThemesController.SetTheme(ThemesController.ThemeTypes.Light);
        }

        private void rdDark_Checked(object sender, RoutedEventArgs e)
        {
            ThemesController.SetTheme(ThemesController.ThemeTypes.Dark);
        }

        private void rdEnglish_Checked(object sender, RoutedEventArgs e)
        {
            LanguageControler.SetLanguage(LanguageControler.enLanguage.English);
        }

        private void rdArabic_Checked(object sender, RoutedEventArgs e)
        {
            LanguageControler.SetLanguage(LanguageControler.enLanguage.Arabic);
        }

        private void btnChangeLogo_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image Files|*.png;*.jpg;*.jpeg;*.bmp;*.gif";

            if (dlg.ShowDialog() == true)
            {
                // حفظ المسار الجديد في الإعدادات
                Settings.Default.LogoPath = dlg.FileName;
                Settings.Default.Save();

                // تحديث الصورة في جميع الأماكن
                MainWindow.UpdateLogoPath();
            }
        }

        //private void btnChoosePath_Click(object sender, RoutedEventArgs e)
        //{
        //    OpenFileDialog dialog = new OpenFileDialog();

        //    if (dialog.ShowDialog() == true)
        //    {
        //        __backupPath = dialog.FileName; // حفظ المسار المختار
        //        txtBackupName.Visibility = Visibility.Visible; // إظهار حقل اسم النسخة الاحتياطية
        //        btnBackup.Visibility = Visibility.Visible; // إظهار زر النسخ الاحتياطي
        //        MessageBox.Show(__backupPath);
        //    }
        //}

        //private async void btnBackup_Click(object sender, RoutedEventArgs e)
        //{
        //    if (string.IsNullOrWhiteSpace(txtBackupName.Text))
        //    {
        //        MessageBox.Show("Please enter backup name.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        //        return;
        //    }

        //    string fileName = $"{txtBackupName.Text}.bak";
        //    string fullPath = System.IO.Path.Combine(__backupPath, fileName);

        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection("Your_Connection_String"))
        //        {
        //            await conn.OpenAsync();
        //            string query = $"BACKUP DATABASE YourDatabaseName TO DISK = '{fullPath}'";

        //            using (SqlCommand cmd = new SqlCommand(query, conn))
        //            {
        //                await cmd.ExecuteNonQueryAsync();
        //            }
        //        }

        //        MessageBox.Show("Backup Created Successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}

        private void BrowseFolderButton_Click(object sender, RoutedEventArgs e)
        {
            // إنشاء OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // تعيين خصائص OpenFileDialog
            openFileDialog.Filter = "جميع الملفات (*.*)|*.*"; // تصفية الملفات
            openFileDialog.Title = "اختيار مجلد";
            openFileDialog.ValidateNames = false; // تعطيل التحقق من صحة الاسم
            openFileDialog.CheckFileExists = false; // تعطيل التحقق من وجود الملف
            openFileDialog.FileName = "اختيار مجلد"; // نص افتراضي

            // فتح مربع الحوار وعرض النتيجة
            if (openFileDialog.ShowDialog() == true)
            {
                // الحصول على المسار من FileName
                string selectedPath = System.IO.Path.GetDirectoryName(openFileDialog.FileName)!;

                MessageBox.Show(selectedPath);

                // حفظ المسار في TextBox
                FolderPathTextBox.Text = selectedPath;
            }
        }

        private void CreateBackup_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(FolderPathTextBox.Text))
            {
                MessageBox.Show($"تم حفظ المسار: {FolderPathTextBox.Text}", "تأكيد", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("يرجى اختيار مجلد أولاً!", "تحذير", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

    }
}
