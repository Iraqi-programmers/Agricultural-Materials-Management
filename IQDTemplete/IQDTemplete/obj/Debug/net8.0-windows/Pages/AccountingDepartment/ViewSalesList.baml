   M S B A M L   `   `   `  ����D  @Interface, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null[2clr-namespace:Interface.Pages.AccountingDepartment$Interface.Pages.AccountingDepartment  5      8    2Interface.Pages.AccountingDepartment.ViewSalesList   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Dlocal2clr-namespace:Interface.Pages.AccountingDepartment     5        N� Title$  ViewSalesList��5	         AppFlowDirection#���   5
      .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      $��* �6   6)    � 	Resources 5   
     FilteredInvoices �� Source��%J   &	       �� 5      -   5         5   <   �� *
Invoices+6X   5      .+�5   
   �� $!��قائمة المبيعات��6   $��0��6*   $��35(�5      $��
ExtraLight"�5        PrimaryTextColor#���  5      $��5,0,0,10q�5      �� 5    
    �� Orientation$ 
Horizontal=�6   $��1��6.   $
��Left=�6;   $��Stretch=�6V   .+�5!   
   �� -   5         ��Name$ FilterComboBox��5!      $	��200��5"      $��40��5#      $��16(�5$      $��55,8,0,0q�5%      $��	4,12,0,11q�5&      $��
ExtraLight"�5'       �� SelectedIndex$ 0��5(      .@�5)      �� $��أسم المستخدم��6   �� 5*      $��تاريخ القائمة��6   �� 5+      $��ربح القائمة��6   �� 5,      $��المبلغ الكلي��6   �� 5-      $��اسم الزبون��6   5.      5/      �� 51   
   -   5         ��Name$ SearchTextBox��51      $��1��6)   $	��300��52      $��40��53      $��15(�54      $��10q�55      $��
ExtraLight"�56       �� TextAlignment$ Center=�57      $��Stretch=�58      $
��Left=�59      $��	205,1,0,0q�5;      	 � ToolTip	 5<      �� *SelectedItem.Content+
 �� ElementName$
 FilterComboBox�� �� StringFormat$! بحث باستخدام {0}��.��6�   5>      &   System.Windows.Controls.DataGrid  5A      -   5          Name$ InvoiceDataGrid��5A      $��2��60     SPAppFlowDirection#���  6=      AutoGenerateColumns$ False��5B         HeadersVisibility$ Column=�5C         
IsReadOnly$
 True��5D         SelectionMode$ Single=�5E         SelectionUnit$ FullRow=�5F         GridLinesVisibility$ 
Horizontal=�5G         RowBackground
 �����   AlternatingRowBackground �	#FF97ACCE$��1q�5J      $��2,5,2,0q�5K      $��20(�5L      ��5M      ��  �� Source# [    Columns 5P      0  *System.Windows.Controls.DataGridTextColumn  5Q      ,  &System.Windows.Controls.DataGridColumn   Header$ اسم المستخدم��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6X   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6;   �� *
UserName+6X     5R      $ تاريخ القائمة��6)   $ * 6`    6@   �� *InvoiceDate+6`     5S      $ المبلغ الكلي��6)   $ * 6_    6?   �� *TotalAmount+6_     5T      $ اسم الزبون��6)   $ * 6^    6=   �� *CustomerName+6^     5U      $ رقم القائمة��6)   $ * 6Z    6>   �� *	LisrNum+6Z    � ContextMenu 5Y      �� 5Z      $
��auto��6   $
��auto��6+   $��2q�69   
�������.@�5\      m� -   5        $!��تفاصيل القائمة��5\       m� Icon 5]      �� 5^      $��/Assets/Logo.png��6$   $��16��6>   $��16��6I   5`      �� 5b      
�������$��5q�63   m� 5e      -   5        $��تصدير إلى Excel��5e       5f      �� 5g      $��/Assets/Logo.png��6$   $��16��6>   $��16��6I   5i      5j      5l      5m      5n      