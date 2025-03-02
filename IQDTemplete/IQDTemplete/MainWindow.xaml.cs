using System.Windows;
using Interface.Pages;
using IQDTemplete.Languages;
using IQDTemplete.Pages;
using IQDTemplete.Themes;


namespace IQDTemplete
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Themes_Click(object sender, RoutedEventArgs e)
        {
            if (Themes.IsChecked == true)
                ThemesController.SetTheme(ThemesController.ThemeTypes.Dark);
            else
                ThemesController.SetTheme(ThemesController.ThemeTypes.Light);
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
        private enum enLanguage { English, Arabic }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LanguageControler.SetLanguage(LanguageControler.enLanguage.English);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            LanguageControler.SetLanguage(LanguageControler.enLanguage.English);
        }

       
    }
}