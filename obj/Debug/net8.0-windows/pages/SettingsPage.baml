   M S B A M L   `   `   `  ����?  ;OIAS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:CoreCore  5          Core.Core.SettingsPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      $localclr-namespace:Core     5        N� Title$  SettingsPage��5	       � 	Resources 5         InteractiveButton �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius �� Triggers P� Property	 P� Value��%�  &	        �� 5   
   " ��6*   .�5      ��  � �� �#3A8DFF6+   �� 5       � ��
 �����6+   �� 5       � ��$ SemiBold"�6+   �� 5       � ��$	 8,4q�6(   �� 5       � ��$ 5q�6'   �� 5       � ��$ 0q�60   �� 5       �@�Cursor$
 Hand��6'   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� #��z��6!   $ 8��6K   ._�5      �� $��Center=�6/   $��Center=�5   /   5      5      5       5      P� 5        �@J�IsMouseOver$
	 True��61   . �5!      ��  � �� �#559DFF63   5"      P� 5#       � ��$
	 True��6/   . �5$      ��  � �� �#2C6ED563   5%      5'      5(      .��5*      � ���#F5F7FA$��20q�6    
 � RowDefinitions
 5+   
   �� 5,      $
��Auto �6   �� 5-      $��* �6   �� 5.      $
��Auto �6   .+�52   
   �� $��Antivirus Settings��6   $��28(�53      $
��Bold"�6"   ���#2D2D2D$��0 0 0 20q�54      �� 57   
   $��1��6   
�������$ 16��61    �� Padding$ 30q�6C   ���#E0E0E0$��1q�58   '   ._�59      ��  �� Orientation$ Vertical=�6   $	��400��60   .+�5<      �� $ 
Horizontal=�6   $��0 0 0 15q�66   .+�5=      �� $��
Scan Type:��6    $	��130��62   ���#444$��Center=�6P   �� 5>      -   5         ��Name$ ScanTypeBox��5>      $	��220��64   ���#F0F4F8���#2D2D2D���#CCCCCC$��4q�5@   5   .@�5A      �� $��Quick��6'   �� 5B      $
��Full��6'   �� 5C      $��Custom��6'   5D      5E      �� 5H      $ 
Horizontal=�6   $��0 0 0 15q�66   .+�5I      �� $��Auto Update:��6    $	��130��64   ���#444$��Center=�6R   �� 5J      -   5         ��Name$ AutoUpdateCheck��5J      $��Center=�68   5K      �� 5N      $ 
Horizontal=�6   $��0 0 0 15q�66   .+�5O      �� $��
Scan Time:��6    $	��130��62   ���#444$��Center=�6P   �� 5P      -   5         ��Name$ ScanTimeBox��5P      $	��220��63   ���#F0F4F8���#2D2D2D���#CCCCCC$��4q�5R   4   5S      5T      5U      �� 5X   
   -   5        $��Save Settings��5X      $��2��5Y      $	��180��5Z      $��Right=�5[      $��0,20,0,0q�5\      #��[  5]      5^      5_      5`      