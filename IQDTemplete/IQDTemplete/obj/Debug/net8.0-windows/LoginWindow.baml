   M S B A M L   `   `   `  ����D  @Interface, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Interface	Interface  5   	       Interface.LoginWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:Interface     5   	     � Title$  LoginWindow��5   	   $	��450��6   $	��800��6*    � WindowStartupLocation$ CenterScreen=�5
   	    � WindowStyle$
 None=�6.   
������  � AllowsTransparency$
 True��6Z    � 
ResizeMode$ NoResize=�6t    � WindowState$ Normal=�6�   $��Poppins)�6�      AppFlowDirection#���   6�   .��5      ��  �� Padding$ 20q�6    �� CornerRadius$ 8��6     SecundaryBackgroundColor#���  6+   ._�5   
   �  � ColumnDefinitions 5      �� 5      �� 5      .+�5      �� $��1��6   .+�5      �� .+�5      �� $��Right=�6!   $	��Top=�6=   $��0,5,10,0q�6U   $��30��6g   	 �� Orientation$	 
Horizontal=�6s   #���   6�   .+�5      �� -   5        
 ��Name$
 btnClose��5   !     IconButtonsStyle#��[ 63    	 close#���  6]   
 � Tag$ IsCloseButton��6�   5      5      � 5      $	��374��6   .+�5      �� $��50q�6!   ��5      �� 5       �� Opacity$ 50Q�6)     AppLogo �� ImageSource# �  66    j� Stretch$
 Fill=�6^   5       5!      5"      �� 5$      $��20,0,0,0q�6   $��0��6+   .+�5%      �� $��-87,20,0,40q�6   $��Center=�62   .+�5&      �� -   5         ��Name$ imgLogo��5&      #���  6-   $
��Fill=�6P   $��36��6_   $��36��6k   $��-6,0,0,5q�6v   �� 5'        PrimaryTextColor#���  6    $��20(�6P     company#���  6^   $��Italic$�6   5(      �� 5*      $��Stretch=�6   .+�5+      �� $��0,10,0,0q�6"   $��Stretch=�64   .+�5,      �� #���  6%   $��48,2,2,5q�6U   $��15(�6g     UserName#���  6u   $��Italic$�6�   �� 5-      $	 
Horizontal=�6%   .+�5.      F� #���  6#    	 users#��[ 6M    �� Stretch$
 Fill=�6k   $��22��6z    J� RenderTransformOrigin$ 0.846,0.447&�6�   $��23��6�   $��5,5,10,5q�6�   �� 5/       	 ProTextBoxStyle#��� 	 6&   $	��200��6P   50      51      �� 53      $��0,25,0,0q�6!   $��Stretch=�63   .+�54      �� #���  6$   $��48,2,2,5q�6T   $��15(�6f    
 Pasword#��� 
 6t   $��Italic$�6�   �� 55      $	 
Horizontal=�6%   .+�56      F� #���  6#     	Password3#��[ 6M   $
 Fill=�6o   $��22��6~   $ 0.846,0.447&�6�   $��23��6�   $��5,5,10,5q�6�   G� 5:        ProPasswordBoxStyle#���  6*   $	��200��6X    G� PasswordChar$ *��6d   5;      5<      �� 5>        ThemeBackColor#���  6   $��
48,25,0,30q�6M   $��Remember me ��6a   #���  6x   $	��348��6�   �� 5B      -   5        $
 btnLogin��5B      $	��150��6/   $��Login��6=     ProButtonStyle#���  6M   $��-1,-1,85,-2q�6�   5C      5D      5E      5F      5G      