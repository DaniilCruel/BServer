   M S B A M L   `   `   `  ����A  =Server, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:ServerServer  5   	   &     Server.Views.AuthorizationWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:Server     5   	     � Title$  Server settings��5	   	    � SizeToContent$ Width=�6!    � WindowStartupLocation$ CenterScreen=�67   $��276.474��6\   $��296.76��6m   .��5
      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   Enter IP Address:64   �� 5   
   -   5        $��addressTextBox��5      $	��Top=�6(   $��1��6@   $	��250��6M   .��6\   ��5      �� 5       �� Path$ Address�6    �� ValidationRules 5      5  /System.Windows.Controls.DataErrorValidationRule  5      5      .��5      5      �� 5   
   $��2��6   $��0,18,0,0q�6!   Enter port:6@   �� 5    
   -   5        $��portTextBox��5       $	��Top=�6%   $��3��6=   $	��250��6J   .��6Y   ��5!      �� 5"      $
 Port�6    5#        5$      5&      .��5'      5(      � 5,   
   $��4��6   $��Center=�6!   $��	20,80,3,3q�6>   $	��230��6Q   .+�5-      �� -   5         �� 	IsDefault . $	��100��5-   ;   $��0,0,10,0q�6J   $	��101��6\   .��6h   OK6l   �� 5.       �� IsCancel . $��60��6%   $��Cancel��63   $	��100��6D   5/      �� 51   
   $��4��6   $��Center=�6!   $��	0,2,10,79q�6>    �� TextAlignment$ Center=�6Q   $��Oblique$�6h   $��Heavy"�6|   $��WrapWithOverflow=�6�   $	��259��6�   $;��5Generate session key in file or select file with key!��6�   $��15(�6�   � 52   
   $��4��6   $��Center=�6!   $��
20,47,3,41q�6>   $	��230��6R   .+�53      �� -   5         . $��60��53   F   $��0,0,10,0q�6T   $��
Creat file��6f   $	��100��6{   $��26��6�   �� 54      -   5         . $��60��54   C   $��Seletc file��6Q   $	��100��6g   $��26��6s   55      57      58      