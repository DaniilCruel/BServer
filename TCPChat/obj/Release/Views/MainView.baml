   M S B A M L   `   `   `  ���� B  >TCPChat, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:TCPChatTCPChat  7 clr-namespace:TCPChat.ViewModelsTCPChat.ViewModels      TCPChat.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    U QMaterialDesignThemes.Wpf, Version=3.0.1.920, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     'localclr-namespace:TCPChat     /vm clr-namespace:TCPChat.ViewModels        MaterialDesignBody#���   $��Regular"�$��13(�&   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�   TextRenderingMode$
 Auto=�  MaterialDesignPaper#���    MaterialDesignFont#���   � Title$ Simple Chat��$	��450��$	��800��.���  � ColumnDefinitions �� $	��65* ��� $	��35* � � RowDefinitions �� $	��10* ��� $	��75* ��� $	��15* �.+��� $��Center=�$��Center=�$��0 5 0 0q�y��� *ConnectIsVisible+.+���  �� Orientation$ 
Horizontal=�.+��� )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Username�� % !MaterialDesignFloatingHintTextBox#��[ $	��100��$��40������ *
Username+�� $��Connect��$��30��$	��100��$��	10 10 0 0q����� *ConnectCommand+�� $��Oblique$�
�������$
��Left=�$��Center=�$%��username has already been taken���� $ 
Horizontal=�$��15 5 0 0q�$��Center=�y��� *WarningVisibility+.+��� 
����������� *
Username+�� $��, ��
��������� 
�������$
��Bold"����� *
Receiver+�� $!�� will receive your messages��
��������� $��1��$��0��$��Center=�$��Disabled=�$��	0,0,300,0q�$��2������ *MessagessItems+����  �� 
TargetType" ��.���  �� Property � |�	 �� Value	 .  ��  � {�	 .  ���� .R�]� $��2q���.  $	��450��.+�� 
 � Orientation$
 
Horizontal=����� *Align+.+��� $
��Bold"����� *Sender+���� *	FontStyle+���� *Color+�� $��WrapWithOverflow=����� *	Message+���� *Color+���� *	FontStyle+�� $��2��$��0��$ 
Horizontal=�$��Center=�$��Center=����#fafafa$��0,10,0,0q�$	��515��$��53��.+��� $	��350��$��40��$��13 0 0 0q�$ Your Message��#��[ $��Center=�{��� *SendIsEnable+���� *TextMessage+�� $��20 0 0 0q�$	��100��$��30��$
��Send��{��� *SendIsEnable+���� *SendCommand+�� $��0��$��1��$��3��$	��277�����#394d8e
�������$��Right=�{��� *SendIsEnable+���� *Users+ �� SelectedItem �� *SelectedUser+