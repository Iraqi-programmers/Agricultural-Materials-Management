﻿using IQDTemplete;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Interface.Pages.InventoryDepartment
{
    /// <summary>
    /// Interaction logic for Inventory.xaml
    /// </summary>
    public partial class Inventory : Page
    {
        public Inventory()
        {
            InitializeComponent();
        }

        private void PurchaseInventory_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            MainWindow.MainFrameInstance.Content = new ProductInventory();
        }

        private void SaleInventory_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void DebtInventory_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void ProductInventory_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void PurchaseInventory_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.MainFrameInstance.Content = new ProductInventory();

        }

        private void SalesInvntory_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void DebtInventory_MouseDown_1(object sender, MouseButtonEventArgs e)
        {

        }

        private void ProductsInventory_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        //private void ProductInventory_Loaded(object sender, RoutedEventArgs e)
        //{

        //}
    }
}
