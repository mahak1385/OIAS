   M S B A M L   `   `   `  ����?  ;OIAS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:CoreCore  5          Core.Core.ScanPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      $localclr-namespace:Core     5        N� Title$  ScanPage��5	      .��5      � ���#F5F7FA$��20q�6     � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��Virus Scanner��6   $��28(�5      $
��Bold"�6"   ���#2D2D2D$��0 0 0 20q�5      �� 5   
   -   5         ��Name$ ScanResults��5      $��1��6(   $��	0,10,0,10q�65   $	��250��6H    �� View 5      �� 5      .P�5      �� $
��File��6%    �� Width$	 500��63    �� DisplayMemberBinding 6?   �� *File+�� 5      $��Status��6%   $	 200��65    6A   �� *Status+5      5       �� 5#   
   $��2��6    �� Orientation$ 
Horizontal=�6"   $��Center=�6;   $
��0,10q�6V   .+�5$      �� -   5         ��Name$ 
StatusText��5$      $��14(�6,   ���#333$��0,0,20,0q�6L   �� 5%      -   5        $ ResultCount��5%      $��14(�6-   ���#5556;   5&      �� 5)   
   $��3��6   $ 
Horizontal=�6"   $��Right=�6;   $��0,20,0,0q�6W   .+�5*      � -   5         �Name$ ScanProgress��5*      $	��300��60   $��20��6<   $��0,0,10,0q�6H   �� 5+      -   5        $��Select Folder to Scan��5+      $	��200��5,      $��30��5-      ���#3A8DFF
�������$
��Bold"�50      $��0q�6'   $��4q�6;   51      52      53      