   M S B A M L   `   `   `  ����?  ;OIAS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:CoreCore  5      !    Core.Core.VirusDatabasePage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      $localclr-namespace:Core     5        N� Title$  VirusDatabase��5	       � 	Resources 5         InteractiveButton �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius J� SnapsToDevicePixels �� Triggers	 P� Property
 P� Value��%�  &	        �� 5   
   " ��6*   .�5      ��  � �� �#3A8DFF6+   �� 5       � ��
 �����6+   �� 5       � ��$ SemiBold"�6+   �� 5       � ��$	 8,4q�6(   �� 5       � ��$ 5q�6'   �� 5       � ��$ 0q�60   �� 5       �@�Cursor$
 Hand��6'   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� #��z��6!   $ 6��5   !   $
 True��5   !   ._�5      �� $��Center=�6/   $��Center=�5   /   5      5      5        5!      P� 5"      	 �@J�IsMouseOver$

 True��61   . �5#      ��  � �� �#559DFF63   5$      P� 5%      	 � ��$

 True��6/   . �5&      ��  � �� �#2C6ED563   5'      5)      5*      .��5,      � 
�������$��20q�6    � RowDefinitions 5-   
   �� 5.      $
��Auto �6   �� 5/      $��* �6   �� 50      $
��Auto �6   .+�54   
   �� $��Virus Signature Database��6   $��28(�55      $
��Bold"�6"   
���   �$��0 0 0 20q�56      �� 59   
    �� Orientation$ Vertical=�6   $��	0,40,0,20q�6,   .+�5:      �� $��Add New Virus Entry��6   $
��Bold"�63   
���   �$��16(�6X   �� 5<      -   5         ��Name$ VirusNameBox��5<      $	��300��6,   $	��0,8q�68   $��6q�6E   
���   ����#F5F5F5���#AAA$��
Virus Name��5>      �� 5?      -   5        $ VirusHashBox��5?      $	��300��6,   $	��0,8q�68   $��6q�6E   
���   ����#F5F5F5���#AAA$��Virus Hash (MD5)��5A      �� 5C      -   5        $��	Add Virus��5C      $	��150��5D      #��[  5E      5F      5G      �� 5J   
   -   5        $��VirusListView��5J      $��1��5K      
�������
���   �$��0 0 0 20q�5N       �� View 5O      �� 5P      .P�5Q      �� $��
Virus Name��6%    �� Width$	 200��69    �� DisplayMemberBinding 6E   �� *Name+�� 5R      $��Signature Hash��6%   $	 300��6=    6I   �� *Hash+�� 5S      $��
Date Added��6%   $	 150��69    6E   �� *	DateAdded+5T      5V      �� 5Y   
   -   5        $��Update Database��5Y      $��2��5Z      $	��180��5[      $��Right=�5\      #��[  5]      5^      5_      5`      