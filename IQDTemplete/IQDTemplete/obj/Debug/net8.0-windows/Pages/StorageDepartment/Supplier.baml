   M S B A M L   `   `   `  ����D  @Interface, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullU/clr-namespace:Interface.Pages.StorageDepartment!Interface.Pages.StorageDepartment  5      0    *Interface.Pages.StorageDepartment.Supplier   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Alocal/clr-namespace:Interface.Pages.StorageDepartment     5        N� Title$  Supplier��5	      .��5
      �    SecundaryBackgroundColor#���   6    � RowDefinitions 5   
   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $��* �6   6)   .+�5   
   �� $��0��6   .+�5      �� $��	Suppliers��6     PrimaryTextColor#���  6*   $��25(�6Z   $��Center=�6i   5      �� 5   
   $��1��6    �� Orientation$ 
Horizontal=�6"   .+�5      �� -   5         ��Name$ FilterComboBox��5      $	��200��5      $��40��5      $��16(�5      $��55,8,0,0q�5      $��	4,12,0,11q�5      $��
ExtraLight"�5       �� SelectedIndex$ 0��5      .@�5      �� $��أسم المورد��6   �� 5       $��العنوان��6   �� 5!      $��
الرقم��6   5#      �� 5%      -   5         ��Name$ SearchTextBox��5%      $��1��6-   $	��300��5&      $��40��5'      $��15(�5(      $��10q�5)      $��
ExtraLight"�5*       �� TextAlignment$ Center=�5+      $��Stretch=�5,      $
��Left=�5-      $��10,1,0,0q�5/       � ToolTip 50      �� *SelectedItem.Content+ �� ElementName$ FilterComboBox��	 �� StringFormat$!	 بحث باستخدام {0}��.��6�   52      55      &   System.Windows.Controls.DataGrid  57   
   -   5        
  Name$
 InvoiceDataGrid��57       $��2��6     AppFlowDirection#���  6:      AutoGenerateColumns$ False��58         HeadersVisibility$ Column=�59         
IsReadOnly$
 True��5:         SelectionMode$ Single=�5;         SelectionUnit$ FullRow=�5<         GridLinesVisibility$ 
Horizontal=�5=         RowBackground
 �����   AlternatingRowBackground �	#FF97ACCE$��1q�5A      $��2,5,2,0q�5B      $��20(�5C         Columns 5G      0  *System.Windows.Controls.DataGridTextColumn  5H      ,  &System.Windows.Controls.DataGridColumn   Header$ اسم المورد��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6V   1  +System.Windows.Controls.DataGridBoundColumn   Binding 69   �� *
UserName+6V     5I      $ رقم ��6%   $ * 6S    63   �� *InvoiceDate+6S     5J      $ 
عنوان��6%   $ * 6O    64   �� *Profit+6O   5N      5P      5Q      