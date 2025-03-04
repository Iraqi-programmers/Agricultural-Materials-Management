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

namespace IQDTemplete.Pages
{
    /// <summary>
    /// Interaction logic for Setting1.xaml
    /// </summary>
    public partial class Setting1 : Page
    {
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

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    LanguageControler.SetLanguage(LanguageControler.enLanguage.English);
        //}

        //private void Button_Click_1(object sender, RoutedEventArgs e)
        //{
        //    LanguageControler.SetLanguage(LanguageControler.enLanguage.Arabic);
        //}


    }
}
