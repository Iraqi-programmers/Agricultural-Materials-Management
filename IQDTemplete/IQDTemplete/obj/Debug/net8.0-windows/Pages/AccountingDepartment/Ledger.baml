   M S B A M L   `   `   `  ����D  @Interface, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:Interface.Pages.UserControlInterface.Pages.UserControl  5          Interface.Pages.Ledger   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ;local)clr-namespace:Interface.Pages.UserControl     5        N� Title$  Ledger��5	       N� 
FontFamily$ Poppins)�5
      .��5      �    SecundaryBackgroundColor#���   6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6    �� 5      $��* �6     � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   �� $��0��5   
    �� CornerRadius$ 20��5   
   $��7,4,7,0q�5   
   $	��150��5   
   $	��Top=�5    
     TertiaryBackgroundColor#���  5!   
   $��0��5"   
   $��1��5#   
   ._�5%      � .+�5&      1   +Interface.Pages.UserControl.BorderAnimation  -   5          Name$ 	Purchases��5&   (   1   +Interface.Pages.UserControl.BorderAnimation   	MouseDown$ Purchases_MouseDown��5+   (     rdPurchases   Text# �  5'   (     TertiaryTextColor   	TextColor# �  5(   (   	   BackgroundColor
	 ���� 
   
IconSource$
 /Assets/Purchases.png��5*   (   5+   (   5-      5.      �� 51   
   $��1��52   
   $ 20��53   
   $��7,4,7,0q�54   
   $	��150��55   
   $	��Top=�56   
   #���  57   
   $��0��58   
   $��1��59   
   ._�5;      � .+�5<        -   5        $ Sales��5<   (   $ Sales_MouseDown��5B   (     rdSales# �  5=   &   # �  5>   &   
	 ���� $
 /Assets/sale.png��5@   &   $��0,0,0,0q�5A   &   5B   (   5E      5F      �� 5J   
   $��2��5K   
   $ 20��5L   
   $��7,4,7,0q�5M   
   $	��150��5N   
   $	��Top=�5O   
   #���  5P   
   $��0��5Q   
   ._�5T      � .+�5U        -   5        $ Debts��5U   (     rdDebts# �  5V   &   # �  5W   &   
	 ���� $
 /Assets/Debits3.png��5Y   &   $��0,0,0,0q�5Z   &   5^      5_      �� 5b   
   $��2��5c   
   $ 20��5d   
   $��	7,-40,7,0q�5e   
   $	��150��5f   
   $	��Top=�5g   
   #���  5h   
   $��1��5i   
   ._�5l      � .+�5m        -   5        $ Revenue��5m   (     	rdRevenue# �  5n   (   # �  5o   (   
	 ���� $
 /Assets/Revenew.png��5q   (   $��0,-7,0,0q�5r   (   5v      5w      5{      5~      