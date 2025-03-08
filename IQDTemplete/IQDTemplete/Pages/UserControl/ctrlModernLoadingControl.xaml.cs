using System.Windows;
using System.Windows.Media.Animation;

namespace Interface.Pages.UserControl
{
   
    public partial class ctrlModernLoadingControl : System.Windows.Controls.UserControl
    {
        public ctrlModernLoadingControl()
        {
            InitializeComponent();
            this.Loaded += OnLoaded; 
        }

        private void StartAnimation()
        {
            var storyboard = (Storyboard)Resources["RotateAnimation"];
            storyboard.Begin();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            StartAnimation(); 
        }
    }
}



