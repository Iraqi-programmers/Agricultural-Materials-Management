﻿using System.Windows;
using System.Windows.Input;

namespace Interface.Pages.UserControl
{
   
    public partial class ctrlPurchesesListDetils : System.Windows.Controls.UserControl
    {
        public ctrlPurchesesListDetils()
        {
            InitializeComponent();
        }

        private  void UserInfo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            LoadingControl.StartAnimation();
        }

      
        private void SupplierName_Click(object sender, RoutedEventArgs e)
        {
            LoadingControl.StopAnimation();
        }

      
    }
}
