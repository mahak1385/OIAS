   M S B A M L   `   `   `  ����?  ;OIAS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:CoreCore  5          Core.Core.QuarantinePage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      $localclr-namespace:Core     5        N� Title$  
Quarantine��5	      .��5
      � 
�������$��20q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��Quarantined Files��6   $��24(�5      
�������$
��Bold"�5      $��0 0 0 10q�6&   �� 5   
   -   5        $��QuarantineListView��5      $��1��6-   
�������
���   �$��0 0 0 10q�5       �� View 5      �� 5      .P�5      �� $��	File Name��6%    �� Width$	 200��68    �� DisplayMemberBinding 6D   �� *
FileName+�� 5      $
��Path��6%   $	 400��63    6?   �� *
FilePath+�� 5      $��Date Quarantined��6%   $	 150��6?    6K   �� *Date+5      5      �� 5    
    �� Orientation$ 
Horizontal=�6   $��2��6.   $��Right=�6;   .+�5!      �� -   5        $��Restore��5!      $	��100��6'   $��0,0,10,0q�63   6E   �� 5"      -   5        $��Delete��5"      $	��100��6&   62   5#      5%      5'      