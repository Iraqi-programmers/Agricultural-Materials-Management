   M S B A M L   `   `   `  ����D  @Interface, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null[2clr-namespace:Interface.Pages.AccountingDepartment$Interface.Pages.AccountingDepartment  5      <    6Interface.Pages.AccountingDepartment.ViewPurchasesList   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Dlocal2clr-namespace:Interface.Pages.AccountingDepartment     5        N� Title$  ViewPurchasesList��5	         AppFlowDirection#���   5
      .��5      �  � RowDefinitions 5      �� 5      $
��auto �6    60   �� 5      $
��auto �6    60   �� 5      $��* �6    6-    � 	Resources 5        FilteredInvoices �� Source��%J   &	       �� 5      -   5         5   @   �� *
Invoices+6\   5      .+�5      �� $#��قائمة المشتريات��6   $��0��6/   $��35(�5        PrimaryBackgroundColor#���  5      $��
ExtraLight"�5        PrimaryTextColor#���  5      $��5,0,0,10q�5      �� 5"       �� Orientation$ 
Horizontal=�6   $��1��62   $
��Left=�6?   $��Stretch=�6Z   .+�5#      �� -   5         ��Name$ FilterComboBox��5#      $	��200��5$      $��40��5%      $��16(�5&      $��55,8,0,0q�5'      $��	4,12,0,11q�5(      $��
ExtraLight"�5)       �� SelectedIndex$ 0��5*      .@�5+      �� $��رقم القائمة��6#   �� 5,      $��تاريخ القائمة��6#   �� 5-      $#��القوائم المدانة��6#   �� 5.      $%��القوائم المكتملة��6#   50      51      �� 53      -   5         ��Name$ SearchTextBox��53      $��1��6-   $	��300��54      $��40��55      $��15(�56      $��10q�57      $��
ExtraLight"�58       �� TextAlignment$ Center=�59      $��Stretch=�5:      $
��Left=�5;      $��	205,1,0,0q�5=      	 � ToolTip	 5>      �� *SelectedItem.Content+
 �� ElementName$
 FilterComboBox�� �� StringFormat$! بحث باستخدام {0}��.��6�   5@      &   System.Windows.Controls.DataGrid  5B   
   -   5          Name$ InvoiceDataGrid��5B      $��2��6,   #���   69      AutoGenerateColumns$ False��5C         HeadersVisibility$ Column=�5D         
IsReadOnly$
 True��5E         SelectionMode$ Single=�5F         SelectionUnit$ FullRow=�5G         GridLinesVisibility$ 
Horizontal=�5H         RowBackground
 �����   AlternatingRowBackground �	#FF97ACCE$��1q�5K      $��2,5,2,0q�5L      $��20(�5M      ��5N      ��  �� Source# [    Columns 5Q      0  *System.Windows.Controls.DataGridTextColumn  5R      ,  &System.Windows.Controls.DataGridColumn   Header$ رقم القائمة��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6W   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6:   �� *	ListNum+6W     5S      $ المبلغ الكلي��6%   $ * 6[    6;   �� *
PirdAmount+6[     5T      $! المبلغ المدفوع��6%   $ * 6]    6=   �� *TotalAmount+6]     5U      $ التاريخ��6%   $ * 6V    66   �� *InvoiceDate+6V    � ContextMenu 5X      �� 5Y      $
��auto��6   $
��auto��6+   $��2q�69   
�������.@�5[      m� -   5        $!��تفاصيل القائمة��5[       m� Icon 5\      �� 5]      $��/Assets/Logo.png��6$   $��16��6>   $��16��6I   5_      �� 5a      
�������$��5q�63   m� 5d      -   5        $��تصدير إلى Excel��5d       5e      �� 5f      $��/Assets/Logo.png��6$   $��16��6>   $��16��6I   5h      5i      5l      5n      5p      