   M S B A M L   `   `   `  ����B  >TCPChat, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:TCPChatTCPChat  5   	   7 clr-namespace:TCPChat.ViewModelsTCPChat.ViewModels  5   	       TCPChat.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   U QMaterialDesignThemes.Wpf, Version=3.0.1.920, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:TCPChat     5   	   /vm clr-namespace:TCPChat.ViewModels     5   	      MaterialDesignBody#���   5   	   $��Regular"�5   	   $��13(�5   	   &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�5   	      TextRenderingMode$
 Auto=�5   	     MaterialDesignPaper#���  5	   	     MaterialDesignFont#���  5
   	    � Title$ Simple Chat��5   	   $	��450��6   $	��800��6*   .��5      �  � ColumnDefinitions 5   
   �� 5      $	��65* �6   �� 5      $	��35* �6    � RowDefinitions 5   
   �� 5      $	��10* �6   �� 5      $	��75* �6   �� 5      $	��15* �6   .+�5!   
   �� $��Center=�6   $��Center=�62   $��0 5 0 0q�6M   y�5"      �� *ConnectIsVisible+.+�5#      ��  �� Orientation$ 
Horizontal=�6   .+�5$      �� )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Username��5&       % !MaterialDesignFloatingHintTextBox#��[ 5'      $	��100��5(      $��40��6$   ��5%      �� *
Username+5(   $   �� 5)      $��Connect��6   $��30��5*      $	��100��6!   $��	10 10 0 0q�5+      ��5,      �� *ConnectCommand+6:   5-      �� 5.      $��Oblique$�5/      
�������$
��Left=�51      $��Center=�6/   $%��username has already been taken��52      53      �� 55   
   $ 
Horizontal=�6   $��15 5 0 0q�6.   $��Center=�56   >   y�6   �� *WarningVisibility+.+�57      �� 
���������6   �� *
Username+62   �� 58      $��, ��6   
�������6"   �� 59      
�������$
��Bold"�6D   ��6   �� *
Receiver+6D   �� 5:      $!�� will receive your messages��6   
�������6;   5;      �� 5>   
   $��1��6   $��0��6   $��Center=�5?      $��Disabled=�5@      $��	0,0,300,0q�5B      $��2��6%   ��5A      �� *MessagessItems+��5C      �� 5D       �� 
TargetType" ��6   .�5E      ��  �� Property � |�	 �� Value	 .  62   �� 5F       � {�	 .  62   5G      ��5I      �� 5J      .R�5K      ]� $��2q�6    ��.  $	��450��6A   .+�5L      � 
 � Orientation$
 
Horizontal=�6$   ��6=   �� *Align+.+�5M      �� $
��Bold"�5O   $   ��5M   (   �� *Sender+��5N   $   �� *	FontStyle+��5P   $   �� *Color+�� 5Q      $��WrapWithOverflow=�5T   $   ��5Q   (   �� *	Message+��5R   $   �� *Color+��5S   $   �� *	FontStyle+5T   $   5V      5W      5X      5Z      �� 5]   
   $��2��6   $��0��6"   $ 
Horizontal=�5^      $��Center=�5_      $��Center=�5`      ���#fafafa$��0,10,0,0q�5a   *   $	��515��5b      $��53��6!   .+�5c      �� $	��350��6   $��40��6"   $��13 0 0 0q�5d      $ Your Message��5e      #��[ 5f      $��Center=�5g      {�5h      �� *SendIsEnable+��5i      �� *TextMessage+�� 5j      $��20 0 0 0q�6   $	��100��5k      $��30��6!   $
��Send��5n      {�5l      �� *SendIsEnable+��5m      �� *SendCommand+5n      5o      �� 5r   
   $��0��6   $��1��6   $��3��5s      $	��277��5u      ���#394d8e
�������$��Right=�5x      {�5t      �� *SendIsEnable+��5y      �� *Users+ �� SelectedItem 5z      �� *SelectedUser+5|      5}      