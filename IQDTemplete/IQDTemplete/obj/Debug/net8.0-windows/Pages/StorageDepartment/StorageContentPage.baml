   M S B A M L   `   `   `  ����D  @Interface, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullU/clr-namespace:Interface.Pages.StorageDepartment!Interface.Pages.StorageDepartment  5      :    4Interface.Pages.StorageDepartment.StorageContentPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Alocal/clr-namespace:Interface.Pages.StorageDepartment     5        N� Title$  StorageContentPage��5	         AppFlowDirection#���   5
      .��5      �   SecundaryBackgroundColor#���  6    � RowDefinitions 5   
   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $��* �6   6)   .+�5   
   �� $��0��6   .+�5      �� $��Storage Content��6     PrimaryTextColor#���  60   $��25(�6`   $��Center=�6o   5      �� 5   
   -   5        $��MainContainer��5   /    �� Orientation$ 
Horizontal=�6   $��1��6D   #���  6Q   $��0,4,0,14q�6�   .+�5      o� #���  6   $��40��6K   $	��800��6W   .@�5      m� #���  6   $��98��6T   $��40��6_   ��5      �� 5      $ 
Horizontal=�6%   $	��100��6>   $��40��6J   .+�5      F� #���  6$     ADD#��[ 6N    �� Stretch$
 Fill=�6j   $��15��6y    J� RenderTransformOrigin$ 0.846,0.447&�6�   $��16��6�   $��5q�6�   �� 5!      $��Center=�6(   $	��Add��6C   $��15(�6N   #���  6\   $��74��6�   5"      5$      m� 5&      #���  6   $��98��6T   $��40��6_   ��5'      �� 5(      $ 
Horizontal=�6%   $	��100��6>   $��40��6J   .+�5*      F� #���  6#    
 Update#��[ 6M   $
 Fill=�6l   $��15��6{   $ 0.846,0.447&�6�   $��16��6�   $��5q�6�   �� 5,      $��Center=�6(   $��Update��6C   $��15(�6Q   #���  6_   $��74��6�   5-      5/      m� 51      #���  6   $��98��6T   $��40��6_   ��52      �� 53      $ 
Horizontal=�6%   $	��100��6>   $��40��6J   .+�55      F� #���  6#     Delete2#��[ 6M   $
 Fill=�6m   $��15��6|   $ 0.846,0.447&�6�   $��16��6�   $��5q�6�   �� 57      $��Center=�6(   $��Delete��6C   $��15(�6Q   #���  6_   $��74��6�   58      5:      5C      5E      �� 5F   
   $��2��6   $ 
Horizontal=�6"   .+�5H      �� -   5         ��Name$ FilterComboBox��5H      $	��200��5I      $��40��5J      $��16(�5K      $��55,8,0,0q�5L      $��	4,12,0,11q�5M      $��
ExtraLight"�5N       �� SelectedIndex$ 0��5O      .@�5P      �� $��أسم المنتج��6   �� 5Q      $��
السعر��6   �� 5R      $��الضمان��6   5T      �� 5V      -   5         ��Name$ SearchTextBox��5V      $��1��6-   $	��300��5W      $��40��5X      $��15(�5Y      $��10q�5Z      $��
ExtraLight"�5[       �� TextAlignment$ Center=�5\      $��Stretch=�5]      $
��Left=�5^      $��10,1,0,0q�5`      	 � ToolTip	 5a      �� *SelectedItem.Content+
 �� ElementName$
 FilterComboBox�� �� StringFormat$! بحث باستخدام {0}��.��6�   5c      5f      &   System.Windows.Controls.DataGrid  5h   
   -   5          Name$ InvoiceDataGrid��5h       $��3��6   #���   6:      AutoGenerateColumns$ False��5i         HeadersVisibility$ Column=�5j         
IsReadOnly$
 True��5k         SelectionMode$ Single=�5l         SelectionUnit$ FullRow=�5m         GridLinesVisibility$ 
Horizontal=�5n         RowBackground
 �����   AlternatingRowBackground �	#FF97ACCE$��1q�5r      $��2,5,2,0q�5s      $��20(�5t         Columns 5x      0  *System.Windows.Controls.DataGridTextColumn  5y      ,  &System.Windows.Controls.DataGridColumn   Header$ اسم المنتج��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6V   1  +System.Windows.Controls.DataGridBoundColumn   Binding 69   �� *
UserName+6V     5z      $ رقم المنتج��6%   $ * 6Y    69   �� *InvoiceDate+6Y     5{      $ الضمان��6%   $ * 6P    65   �� *Profit+6P     5|      $ 
السعر��6%   $ * 6T    64   �� *TotalAmount+6T   5      5�      5�      