   M S B A M L   `   `   `  ����D  @Interface, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:Interface.Pages.UserControlInterface.Pages.UserControl  5          IQDTemplete.Pages.Home1   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ;local)clr-namespace:Interface.Pages.UserControl     5         AppFlowDirection#���   5        N� Title$
  Home��5
       N� 
FontFamily$ Poppins)�6   .��5      �   SecundaryBackgroundColor#���  6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6    �� 5      $��* �6     � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   �� $��0��5       �� CornerRadius$ 20��5      $��7,4,7,0q�5      $	��150��5      $	��Top=�5         TertiaryBackgroundColor#���  5!      $��0��5"      $��1��5#      ._�5%      � .+�5&      1   +Interface.Pages.UserControl.BorderAnimation  -   5          Name$ AddPurchasesMenue��5&   (   1   +Interface.Pages.UserControl.BorderAnimation   	MouseDown$! AddPurchasesMenue_MouseDown��5+   #     addpurchesmenue   Text# �  5'   %     TertiaryTextColor   	TextColor# �  5(   %   	   BackgroundColor
	 ���� 
   
IconSource$
 /Assets/add.png��5*   %   5+   #   5-      5.      �� 51   
   $��1��52      $ 20��53      $��7,4,7,0q�54      $	��150��55      $	��Top=�56      #���  57      $��0��58      $��1��59      ._�5;      � .+�5<        -   5        $ DeploySalesMenue��5<   (   $  DeploySalesMenue_MouseDown��5B   #     addsalesmenue# �  5=   #   # �  5>   #   
	 ���� $"
 /Assets/DeploySalesMenue.png��5@   #   $��0,0,0,0q�5A   #   5B   #   5E      5F      �� 5J   
   $��2��5K      $ 20��5L      $��7,4,7,0q�5M      $	��150��5N      $	��Top=�5O      #���  5P      $��0��5Q      $��2��5R      ._�5T      � .+�5U        -   5        $ DebtsMangment��5U   (   $ DebtsMangment_MouseDown��5\   $     debitmangment# �  5V   #   # �  5X   #   
	 ���� $ 
 /Assets/DebitsMangment.png��5Z   #   $��0,0,0,0q�5[   #   5\   $   5`      5a      5g      5j      