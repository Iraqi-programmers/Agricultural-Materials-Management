   M S B A M L   `   `   `  ����D  @Interface, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:IQDTempleteIQDTemplete  5   	       IQDTemplete.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:IQDTemplete     5   	   -   5          �Name$
  home��5       � Title$ 
MainWindow��5	   	   $	��720��6   $
��1100��6)    � WindowStartupLocation$ CenterScreen=�5
   	    � WindowStyle$
 None=�6.   
������  � AllowsTransparency$
 True��5   "    � 
ResizeMode$ NoResize=�5   	    � WindowState$ Normal=�6   $��Poppins)�64      AppFlowDirection#���   5   	   .��5      ��  �� CornerRadius$ 10��6     PrimaryBackgroundColor#���  6   ._�5   
   �  � ColumnDefinitions 5      �� 5      $	��190 �6#   �� 5      $��* �6#   .+�5      �� $ 	10 0 0 10��6   	 �� Padding$	 10q�5        SecundaryBackgroundColor#���  5      ._�5      � -   5        
 �Name$
 gridMenu��5       � RowDefinitions 5      �� 5      $��60 �6(   �� 5      $��* �6(   .+�5!      �� $��0��6!    �� Orientation$ 
Horizontal=�6.   .+�5"      �� -   5         ��Name$ imgLogo��5"         AppLogo#���  63   $
��Fill=�6V   $��36��6g   $��36��6u   $��Center=�6�   $��10 0 0 0q�6�   �� 5#      $��Orizon��6      SecundaryBlueColor#���  61   $
��Bold"�6c   $��24(�6u   $��Center=�6�   $
��Left=�6�   $
��Left=�6�   $��5 0 0 0q�6�   5$      �� 5&      $��1��6!   .+�5'      	� -   5         	�Name$ rdHome��5'   &    
 rdHome#���  66     MenuRadioButtonStyle#���  6Y     home
 � Tag# �  6�   $��0 30 0 0q�6�   6�   	� 5(      -   5        $ 	rdStorage��5(   &     	rdStorage#���  69   #���  6_    	 Storage# � 	 6�   $��0 1 0 1q�6�   6�   	� 5)      -   5        $ rdLegder��5)   &    
 rdLedger#��� 
 68   #���  6]    
 Ledger# �  6�   6�   	� 5*      -   5        $ rdInventory��5*   &     rdInventory#���  6;   #���  6c     	Inventory# �  6�   $��0 1 0 1q�6�   6�   	� 5+      -   5        $ rdNotifications��5+   &     rdNotifications#���  6?   #���  6k     notifications# �  6�   $��0 1 0 1q�6�   6�   �� 5,      $��20 20,0,290q�6$     TertiaryBackgroundColor#���  69   	� 5-      -	   5        $ 
rdSettings��5-   &     	rdSetting#���  6:   #���  6`     settings# �  6�   6�   5.      5/      50      � 53      $��1��6    54      �� 55      $��60 �6$   �� 56      .+�59      �� $��0��6   $��4��6+   $��Right=�6;   $	��Top=�6W   $��0,5,10,0q�6o   $��30��6�   $ 
Horizontal=�6�     SPAppFlowDirection#���  6�   .+�5:      �� -
   5         ��Name$ btnClose��5:        IconButtonsStyle#��[ 6/    	 close#���  6Y   $ IsCloseButton��6�   �� 5;      -   5        $ 
btnRestore��5;      $	��5 0q�61   ��5<      �� 5=       �� 
TargetType" ��6$    �� BasedOn# [ 6A   .�5>   "   ��  �� Property � ��  maximize �� Value# �  6<    �� Triggers 5?   "   �� 5@   &    �� Binding 62   ��  �� ElementName$
 home�� �� Path$ WindowState� �� Value 	Maximized6i   .a�5A   *   ��  � ��  restore# �  6D   5B   '   5D      5F      �� 5G      -   5        $ btnMinimize��5G      #���  62     minimize#���  6]   6�   5H      �� 5L      $��1��6   $ 0 0 5 0��6&   
������ ._�5M       � -   5          �Name$ frameContent��5M        � NavigationUIVisibility$ Hidden=�62   $��10q�6R   5N      5P      5Q      5R      5S      