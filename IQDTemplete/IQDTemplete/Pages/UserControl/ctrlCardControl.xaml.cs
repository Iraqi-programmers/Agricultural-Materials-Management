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
    /// Interaction logic for ctrlCardControl.xaml
    /// </summary>
    public partial class ctrlCardControl : System.Windows.Controls.UserControl
    {

        public ctrlCardControl()
        {
            InitializeComponent();
        }
        public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register("Title", typeof(string), typeof(ctrlCardControl), new PropertyMetadata(""));
        public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register("Text", typeof(string), typeof(ctrlCardControl), new PropertyMetadata(""));
        public static readonly DependencyProperty Icon =
        DependencyProperty.Register("Icon", typeof(ImageSource), typeof(ctrlCardControl), new PropertyMetadata(null));


        public ImageSource IconSource
        {
            get { return (ImageSource)GetValue(Icon); }
            set { SetValue(Icon, value); }
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

    }
}
