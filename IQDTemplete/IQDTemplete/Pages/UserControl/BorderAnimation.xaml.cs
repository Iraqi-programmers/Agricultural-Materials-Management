
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Interface.Pages.UserControl
{
    /// <summary>
    /// Interaction logic for BorderAnimation.xaml
    /// </summary>
    public partial class BorderAnimation : System.Windows.Controls.UserControl
    {
        // تعريف الخصائص القابلة للتخصيص
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(BorderAnimation), new PropertyMetadata(""));

        public static readonly DependencyProperty TextColorProperty =
            DependencyProperty.Register("TextColor", typeof(Brush), typeof(BorderAnimation), new PropertyMetadata(Brushes.White));

        public static readonly DependencyProperty BackgroundColorProperty =
            DependencyProperty.Register("BackgroundColor", typeof(Brush), typeof(BorderAnimation), new PropertyMetadata(Brushes.Transparent));

        public static readonly DependencyProperty IconSourceProperty =
            DependencyProperty.Register("IconSource", typeof(ImageSource), typeof(BorderAnimation), new PropertyMetadata(null));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public Brush TextColor
        {
            get { return (Brush)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }

        public Brush BackgroundColor
        {
            get { return (Brush)GetValue(BackgroundColorProperty); }
            set { SetValue(BackgroundColorProperty, value); }
        }

        public ImageSource IconSource
        {
            get { return (ImageSource)GetValue(IconSourceProperty); }
            set { SetValue(IconSourceProperty, value); }
        }

     

        //private void MainBorder_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    // تنفيذ أي عمل تريده عند النقر
        //    MessageBox.Show("تم النقر على Border!");
        //}


        public BorderAnimation()
        {
            InitializeComponent();
        }
    }
}
