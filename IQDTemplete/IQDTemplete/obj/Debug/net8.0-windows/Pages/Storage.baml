   M S B A M L   `   `   `  ����D  @Interface, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:Interface.Pages.UserControlInterface.Pages.UserControl  5          Interface.Pages.Storage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ;local)clr-namespace:Interface.Pages.UserControl     5        N� Title$  Storage��5	         AppFlowDirection#���   5
   	    N� 
FontFamily$ Poppins)�5      .��5      �   SecundaryBackgroundColor#���  6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6    �� 5      $��* �6     � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   �� $��0��5       �� CornerRadius$ 20��5      $��7,4,7,0q�5      $	��150��5       $	��Top=�5!        TertiaryBackgroundColor#���  5"      $��0��5#      ._�5&      � .+�5'      1   +Interface.Pages.UserControl.BorderAnimation  -   5          Name$ StorageContent��5'   (   1   +Interface.Pages.UserControl.BorderAnimation   	MouseDown$ StorageContent_MouseDown��5,   $     	Suppliers   Text# �  5(   $     TertiaryTextColor   	TextColor# �  5)   $   	   BackgroundColor
	 ���� 
   
IconSource$
 /Assets/supplier.png��5+   $   5,   $   5.      5/      �� 52   
   $��1��53      $ 20��54      $��7,4,7,0q�55      $	��150��56      $	��Top=�57      #���  58      $��0��59      ._�5<      � .+�5=        -   5        $ AddMenueTostorage��5=   (     AddMenuetoStorage# �  5>   "   # �  5?   "   
	 ���� $
 /Assets/addmenue (2).png��5A   "   $��0,0,0,0q�5B   "   5F      5G      �� 5K   
   $��2��5L      $ 20��5M      $��7,4,7,0q�5N      $	��150��5O      $	��Top=�5P      #���  5Q      $��0��5R      ._�5U      � .+�5V        -   5        $ 	Suppliers��5V   (   $ Suppliers_MouseDown��5]   !     StorageContain# �  5W   "   # �  5Y   "   
	 ���� $ 
 /Assets/StorageContent.png��5[   "   $��0,0,0,0q�5\   "   5]   !   5a      5b      �� 5d   
   $��0��5e      $ 20��5f      $��	7,-40,7,0q�5g      $	��150��5h      $	��Top=�5i      #���  5j      $��1��5k      ._�5n      � .+�5o        -   5        $ ReturnItemToStorage��5o   (     ReturnItemToStorage# �  5p      # �  5r      
	 ���� $
 /Assets/Return_Items.png��5t      $��0,0,0,0q�5u      5z      5{      �� 5}   
   $��1��5~      $ 20��5      $��	7,-40,7,0q�5�      $	��150��5�      $	��Top=�5�      #���  5�      $��1��5�      ._�5�      � .+�5�        -   5        $ DeploySalesMenue��5�   (   $  DeploySalesMenue_MouseDown��5�        addsalesmenue# �  5�      # �  5�      
	 ���� $"
 /Assets/DeploySalesMenue.png��5�      $��0,0,0,0q�5�      5�      5�      5�      5�      5�      