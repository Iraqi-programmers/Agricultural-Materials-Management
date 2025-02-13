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
using IQDTemplete.Languages;

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LanguageControler.SetLanguage(LanguageControler.enLanguage.English);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            LanguageControler.SetLanguage(LanguageControler.enLanguage.Arabic);
        }
    }
}
