   M S B A M L   `   `   `  ����D  @Interface, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Interface.PagesInterface.Pages  5      '    !Interface.Pages.AddSalesMunuePage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:Interface.Pages     5        N� Title$  AddSalesMunuePage��5	         AppFlowDirection#���   5
       N� 
FontFamily$ Poppins)�5      .��5      �   SecundaryBackgroundColor#���  6    � RowDefinitions 5   
   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $��* �6   6)   .+�5   
   �� $��0��6   .+�5      �� $��	Prouducts��6     PrimaryTextColor#���  6*   $��25(�6Z   $��Center=�6i   5      �� 5   
   -   5        $��MainContainer��5   /    �� Orientation$ 
Horizontal=�6   $��1��6D   #���  6Q   $��0,4,0,14q�6�   .+�5      o� #���  6   $��40��6K   $	��800��6W   .@�5      m� #���  6   $
��auto��6T   ��5      �� 5      $ 
Horizontal=�6%   $��Stretch=�6>   $��40��6\   .+�5       F� #���  6$     ADD#��[ 6N    �� Stretch$
 Fill=�6j   $��15��6y    J� RenderTransformOrigin$ 0.846,0.447&�6�   $��16��6�   $��5q�6�   �� 5"      $��Center=�6(   $��Add to menue��6C   $��15(�6W   #���  6e   $��Stretch=�6�   5#      5%      m� 5'      #���  6   $
��auto��6S   ��5(      �� 5)      $ 
Horizontal=�6%   $��Stretch=�6?   .+�5+      F� #���  6#    
 Update#��[ 6M   $
 Fill=�6l   $��15��6{   $ 0.846,0.447&�6�   $��16��6�   $��5q�6�   �� 5-      $��Center=�6(   $��	New menue��6C   $��15(�6T   #���  6b   $��Stretch=�6�   5.      50      5;      5=      �� 5>   
   $��2��6   $ 
Horizontal=�6"   .+�5@      �� -   5         ��Name$ FilterComboBox��5@      $	��200��5A      $��40��5B      $��16(�5C      $��55,8,0,0q�5D      $��	4,12,0,11q�5E      $��
ExtraLight"�5F       �� SelectedIndex$ 0��5G      .@�5H      �� $��أسم المنتج��6   �� 5I      $��المورد��6   �� 5J      $��نوعه��6   �� 5K      $��الضمان��6   5M      �� 5O      -   5         ��Name$ SearchTextBox��5O      $��1��6-   $	��300��5P      $��40��5Q      $��15(�5R      $��10q�5S      $��
ExtraLight"�5T      	 �� TextAlignment$	 Center=�5U      $��Stretch=�5V      $
��Left=�5W      $��10,1,0,0q�5Y      
 � ToolTip
 5Z      �� *SelectedItem.Content+ �� ElementName$ FilterComboBox�� �� StringFormat$! بحث باستخدام {0}��.��6�   5\      5_      &   System.Windows.Controls.DataGrid  5a   
   -   5          Name$ InvoiceDataGrid��5a       $��3��6   #���   6:      AutoGenerateColumns$ False��5b         HeadersVisibility$ Column=�5c         
IsReadOnly$
 True��5d         SelectionMode$ Single=�5e         SelectionUnit$ FullRow=�5f         GridLinesVisibility$ 
Horizontal=�5g         RowBackground
 �����   AlternatingRowBackground �	#FF97ACCE$��1q�5k      $��2,5,2,0q�5l      $��20(�5m         Columns 5q      0  *System.Windows.Controls.DataGridTextColumn  5r      ,  &System.Windows.Controls.DataGridColumn   Header$ اسم المنتج��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6V   1  +System.Windows.Controls.DataGridBoundColumn   Binding 69   �� *
UserName+6V     5s      $ 	نوعه ��6%   $ * 6T    64   �� *InvoiceDate+6T     5t      $ الضمان��6%   $ * 6P    65   �� *Profit+6P     5u      $ المورد��6%   $ * 6P    65   �� *Profit+6P   5y      5{      5|      