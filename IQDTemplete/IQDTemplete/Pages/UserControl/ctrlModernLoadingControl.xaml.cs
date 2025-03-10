using System.Windows;
using System.Windows.Media.Animation;

namespace Interface.Pages.UserControl
{
   
    public partial class ctrlModernLoadingControl : System.Windows.Controls.UserControl
    {
        public ctrlModernLoadingControl()
        {
            InitializeComponent();
           // this.Loaded += OnLoaded; 
        }

        public void StartAnimation()
        {
            var storyboard = (Storyboard)Resources["RotateAnimation"];
            storyboard.Begin();
            this.Visibility = Visibility.Visible;
            
        }

        public void StopAnimation()
        {
            var storyboard = (Storyboard)Resources["RotateAnimation"];
            storyboard.Stop();
            this.Visibility = Visibility.Collapsed;

        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
          //  StartAnimation(); 
        }
    }
}



