   M S B A M L   `   `   `  ����D  @Interface, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullY1clr-namespace:Interface.Pages.InventoryDepartment#Interface.Pages.InventoryDepartment  5      :    4Interface.Pages.InventoryDepartment.ProductInventory   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Clocal1clr-namespace:Interface.Pages.InventoryDepartment     5        N� Title$  ProductInventory��5	      .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $��* �6   6)    � 	Resources 5   
      FilteredInvoices �� Source��%J   &	        �� 5      -   5         5   <   �� *
Invoices+6X   5       � ColumnDefinitions 5   
   �� 5      $
��401* �6   �� 5      $
��399* �6   .+�5   
   ��   lblProductInventoryTitle#���  6   $��0��6F   $��35(�5        PrimaryBackgroundColor#���  5      $��
ExtraLight"�5        PrimaryTextColor#���  5       $��5,0,0,10q�5!      $��2��6!   �� 5$   
    �� Orientation$ 
Horizontal=�6   $��1��6.   $
��Left=�6;   $��	0,0,0,330q�6V   $��2��6i   .+�5%      �� -   5         ��Name$ FilterComboBox��5%      $	��200��5&      $��40��5'      $��16(�5(      $��55,8,0,0q�5)      $��	4,12,0,11q�5*      $��
ExtraLight"�5+       �� SelectedIndex$ 0��5,      .@�5-      �� $��رقم القائمة��6   �� 5.      $��تاريخ القائمة��6   �� 5/      $#��القوائم المدانة��6   �� 50      $%��القوائم المكتملة��6   52      53      �� 55   
   -   5         ��Name$ SearchTextBox��55      $��1��6)   $	��300��56      $��40��57      $��15(�58      $��10q�59      $��
ExtraLight"�5:      	 �� TextAlignment$	 Center=�5;      $
��Left=�5<      $��205,12,0,11q�5>      $��2��5?   q   
 � ToolTip
 6   �� *SelectedItem.Content+ �� ElementName$ FilterComboBox�� �� StringFormat$! بحث باستخدام {0}��6q   &   System.Windows.Controls.DataGrid  5A   
   -   5          Name$ InvoiceDataGrid��5A      $��2��6,     AppFlowDirection#���  69      AutoGenerateColumns$ False��5B         HeadersVisibility$ Column=�5C         
IsReadOnly$
 True��5D         SelectionMode$ Single=�5E         SelectionUnit$ FullRow=�5F         GridLinesVisibility$ 
Horizontal=�5G         RowBackground
 �����   AlternatingRowBackground �	#FF97ACCE$��1q�5J      $��2,5,2,0q�5K      $��20(�5L      $��2��5M   M   ��6   ��  �� Source# [     Columns 5P      0  *System.Windows.Controls.DataGridTextColumn  5Q        dgvhProductId,  &System.Windows.Controls.DataGridColumn   Header# �  6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6k   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6N   �� *	ListNum+6k     5R        dgvhProductName# �  6%   $ * 6p    6P   �� *InvoiceDate+6p     5S        
dgvhStatus# �  6%   $ * 6h    6K   �� *	ListNum+6h     5T        dgvhQuantity# �  6%   $ * 6m    6M   �� *
PirdAmount+6m     5U       	 dgvhTotalPrice# � 	 6%   $ * 6o    6O   �� *TotalAmount+6o    � ContextMenu 5X      �� 5Y      $
��auto��6   $
��auto��6+   $��2q�69   
�������.@�5[      m� -   5        $!��تفاصيل القائمة��5[       m� Icon 5\      �� 5]      $��/Assets/Logo.png��6$   $��16��6>   $��16��6I   5_      �� 5a      
�������$��5q�63   m� 5d      -   5        $��تصدير إلى Excel��5d       5e      �� 5f      $��/Assets/Logo.png��6$   $��16��6>   $��16��6I   5h      5i      5l      5n      5r      