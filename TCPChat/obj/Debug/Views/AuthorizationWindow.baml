   M S B A M L   `   `   `  ����B  >TCPChat, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:TCPChatTCPChat  5   	   '    !TCPChat.Views.AuthorizationWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:TCPChat     5   	     � Title$  Client settings��5   	    � SizeToContent$ Width=�6!    � WindowStartupLocation$ CenterScreen=�67   $��214.589��6\   .��5	      � $��10q�6    � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   Enter IP Address:64   �� 5   
   -   5        $��addressTextBox��5      $	��Top=�6(   $��1��6@   $	��250��6M   .��6\   ��5      �� 5       �� Path$ Address�6    �� ValidationRules 5      5  /System.Windows.Controls.DataErrorValidationRule  5      5      .��5      5      �� 5   
   $��2��6   $��0,18,0,0q�6!   Enter port:6@   �� 5   
   -   5        $��portTextBox��5      $	��Top=�6%   $��3��6=   $	��250��6J   .��6Y   ��5       �� 5!      $
 Port�6    5"        5#      5%      .��5&      5'      � 5)   
   $��4��6   $��Right=�6!   $��	0,20,57,0q�6=   .+�5*      �� -   5         �� 	IsDefault . $��60��5*   ;   $��0,0,10,0q�6I   $��OK��6[   �� 5+       �� IsCancel . $��60��6%   $��Cancel��63   5,      5.      5/      