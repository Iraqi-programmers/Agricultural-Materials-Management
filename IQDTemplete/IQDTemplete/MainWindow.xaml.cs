using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Interface.Pages;
using IQDTemplete.Languages;
using IQDTemplete.Pages;
using IQDTemplete.Themes;
using System.IO;
using System.Windows.Media;
using Interface.Properties;

namespace IQDTemplete
{
    public partial class MainWindow : Window
    {
        public static Frame MainFrameInstance { get; private set; }
        private enum enLanguage { English, Arabic }

        public MainWindow()
        {
            InitializeComponent();
            MainFrameInstance = frameContent;
            frameContent.Content = new Home1();

            string savedLogoPath = Settings.Default.LogoPath;
            if (!string.IsNullOrEmpty(savedLogoPath) && File.Exists(savedLogoPath))
                imgLogo.Source = new BitmapImage(new Uri(savedLogoPath, UriKind.Absolute));
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void rdHome_Click(object sender, RoutedEventArgs e)
        {
            frameContent.Navigate(new Home1());
        }

        private void rdStorage_Click(object sender, RoutedEventArgs e)
        {
            frameContent.Navigate(new Storage());
        }

        private void rdInventory_Click(object sender, RoutedEventArgs e)
        {
            frameContent.Navigate(new Inventory());
        }

        private void rdNotifications_Click(object sender, RoutedEventArgs e)
        {
            frameContent.Navigate(new Notifications());
        }

        private void rdLegder_Click(object sender, RoutedEventArgs e)
        {
            frameContent.Navigate(new Ledger());
        }

        private void rdSettings_Click(object sender, RoutedEventArgs e)
        {
            frameContent.Navigate(new Setting1());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LanguageControler.SetLanguage(LanguageControler.enLanguage.English);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            LanguageControler.SetLanguage(LanguageControler.enLanguage.English);
        }

        private void rdSettings_Checked(object sender, RoutedEventArgs e)
        {
        }
    }

    //public partial class MainWindow : Window
    //{
    //    public static Frame MainFrameInstance { get; private set; }
    //    private enum enLanguage { English, Arabic }

    //    public MainWindow()
    //    {
    //        InitializeComponent();
    //        MainFrameInstance = frameContent;
    //        frameContent.Content = new Home1();

    //        string savedLogoPath = Properties.Settings.Default.LogoPath;
    //        if (!string.IsNullOrEmpty(savedLogoPath) && File.Exists(savedLogoPath))
    //        {
    //            imgLogo.Source = new BitmapImage(new Uri(savedLogoPath, UriKind.Absolute));
    //        }

    //    }

    //    private void btnClose_Click(object sender, RoutedEventArgs e)
    //    {
    //        Close();
    //    }

    //    private void btnRestore_Click(object sender, RoutedEventArgs e)
    //    {
    //        if (WindowState == WindowState.Normal)
    //            WindowState = WindowState.Maximized;
    //        else
    //            WindowState = WindowState.Normal;
    //    }

    //    private void btnMinimize_Click(object sender, RoutedEventArgs e)
    //    {
    //        WindowState = WindowState.Minimized;
    //    }

    //    private void rdHome_Click(object sender, RoutedEventArgs e)
    //    {
    //        frameContent.Navigate(new Home1());
    //    }

    //    private void rdStorage_Click(object sender, RoutedEventArgs e)
    //    {
    //        frameContent.Navigate(new Storage());
    //    }

    //    private void rdInventory_Click(object sender, RoutedEventArgs e)
    //    {
    //        frameContent.Navigate(new Inventory());
    //    }

    //    private void rdNotifications_Click(object sender, RoutedEventArgs e)
    //    {
    //        frameContent.Navigate(new Notifications());
    //    }

    //    private void rdLegder_Click(object sender, RoutedEventArgs e)
    //    {
    //        frameContent.Navigate(new Ledger());
    //    }

    //    private void rdSettings_Click(object sender, RoutedEventArgs e)
    //    {
    //        frameContent.Navigate(new Setting1());
    //    }


    //    private void Button_Click(object sender, RoutedEventArgs e)
    //    {
    //        LanguageControler.SetLanguage(LanguageControler.enLanguage.English);
    //    }

    //    private void Button_Click_1(object sender, RoutedEventArgs e)
    //    {
    //        LanguageControler.SetLanguage(LanguageControler.enLanguage.English);
    //    }

    //    private void rdSettings_Checked(object sender, RoutedEventArgs e)
    //    {

    //    }

    //    private void btnChangeLogo_Click(object sender, RoutedEventArgs e)
    //    {
    //        // فتح نافذة لاختيار الصورة
    //        Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
    //        {
    //            Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp",
    //            Title = "Choice new logo"
    //        };

    //        if (openFileDialog.ShowDialog() == true)
    //        {
    //            string newLogoPath = openFileDialog.FileName;

    //            // تحديث الشعار في الواجهة
    //            Uri logoUri = new Uri(newLogoPath, UriKind.Absolute);
    //            BitmapImage logoBitmap = new BitmapImage(logoUri);
    //            imgLogo.Source = logoBitmap;

    //            // حفظ المسار لاستخدامه لاحقًا (يمكن تخزينه في الإعدادات)
    //            Properties.Settings.Default.LogoPath = newLogoPath;
    //            Properties.Settings.Default.Save();
    //        }
    //    }

    //}
}