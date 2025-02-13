using System.Windows;
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

        private void rdAnalytics_Click(object sender, RoutedEventArgs e)
        {
            frameContent.Navigate(new Analytics1());
        }

        private void rdMessages_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void rdCollections_Click(object sender, RoutedEventArgs e)
        {
            frameContent.Navigate(new Collections1());
        }

        private void rdUsers_Click(object sender, RoutedEventArgs e)
        {
          
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