   M S B A M L   `   `   `  ����D  @Interface, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:Interface.Pages.UserControlInterface.Pages.UserControl  5      3    -Interface.Pages.InventoryDepartment.Inventory   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ;local)clr-namespace:Interface.Pages.UserControl     5        N� Title$  	Inventory��5
      .��5      ��  �� CornerRadius$ 8��6      SecundaryBackgroundColor#���   6   ._�5   
   �   AppFlowDirection#���  6    � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $��* �6$   �� 5      $��* �6$    � RowDefinitions 5      �� 5      $	��164 �6    �� 5      $	��150 �6    �� 5      $��* �6    .+�5      �� $��0��5   
   $ 20��5   
   $��7,7,7,0q�5   
   $	��150��5   
   $	��Top=�5    
     TertiaryBackgroundColor#���  5!   
   $��0��5"   
   $��1��5#   
   ._�5%      � .+�5&      1   +Interface.Pages.UserControl.BorderAnimation  -   5          Name$ PurchaseInventory��5&   ,   1   +Interface.Pages.UserControl.BorderAnimation   	MouseDown$# PurchaseInventory_MouseDown_1��5+   (     rdPurchasIinventory   Text# �  5'   (     TertiaryTextColor   	TextColor# �  5(   (      BackgroundColor
 ���� 	   
IconSource$	 /Assets/Purchases.png��5*   (   5+   (   5-      5.      �� 50      $��1��51   
   $ 20��52   
   $��7,7,7,0q�53   
   $	��150��54   
   $	��Top=�55   
   #���  56   
   $��0��57   
   $��1��58   
   ._�5:      � .+�5;        -   5        $ SalesInventory��5;   ,   $ SaleInventory_MouseDown��5@   (     rdSalesInventory# �  5<   (   # �  5=   (   
 ���� $	 /Assets/sale.png��5?   (   5@   (   5B      5C      �� 5D      $��0��5E   
   $ 20��5F   
   $��7,0,7,0q�5G   
   $	��150��5H   
   $	��Top=�5I   
   #���  5J   
   $��1��5K   
   $��1��5L   
   ._�5N      � .+�5O        -   5        $ DebtInventory��5O   ,   $ DebtInventory_MouseDown��5T   (     rdDebtInventory# �  5P   (   # �  5Q   (   
 ���� $ 	 /Assets/DebitsMangment.png��5S   (   5T   (   5V      5W      �� 5X      $��2��5Y   
   $ 20��5Z   
   $��7,7,7,0q�5[   
   $	��150��5\   
   $	��Top=�5]   
   #���  5^   
   $��0��5_   
   $��1��5`   
   ._�5b      � .+�5c        -   5        $ ProductInventory��5c   ,   $  ProductInventory_MouseDown��5h   (   
   Loaded$
 ProductInventory_Loaded��6O     rdProductInventory# �  5d   (   # �  5e   (   
 ���� $	 /Assets/inventory.png��5g   (   5h   O   5j      5k      5v      5w      5x      