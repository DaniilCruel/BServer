   M S B A M L   `   `   `  ���� A  =Server, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:ServerServer  5clr-namespace:Server.ViewModelsServer.ViewModels      Server.ServerWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     V RMaterialDesignThemes.Wpf, Version=2.5.0.1205, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes   localclr-namespace:Server  0
ViewModelsclr-namespace:Server.ViewModels    � Title$  Server��$	��450��$	��800�� � WindowStartupLocation$ CenterScreen=�.��� -    �Name$ mainGrid�� � ColumnDefinitions �� $
��Auto ��� .+�� -   $ leftMenuGrid��$	��150��$��0��$
��Left=����	#FF32558B � RowDefinitions �� $
��Auto ��� $
��Auto �.+��� -    ��Name$ listViewMenu��$��Disabled=�$��0��
�������$��Microsoft JhengHei Light)�$��18(�.@��� $
��Auto��.����  �� Orientation$ 
Horizontal=�.+�'  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.5.0.1205, Culture=neutral, PublicKeyToken=null]]   Kind$ People=�$��25��$��25��$��Center=�$��5q��� $��Clients��$��17(�$��Center=�$	��5 0q��� $
��Auto��.���� $ 
Horizontal=�.+�  $ Files=�$��25��$��25��$��Center=�$��5q��� $��Files��$��17(�$��Center=�$	��5 0q��� $
��Auto��.���� $ 
Horizontal=�.+�  $ Pencil=�$��25��$��25��$��Center=�$��5q��� $	��Log��$��17(�$��Center=�$	��5 0q��� $
��Auto��.���� $ 
Horizontal=�.+�  $	 Key=�$��25��$��25��$��Center=�$��5q��� $	��Key��$��17(�$��Center=�$	��5 0q�� $��1�� �� $��20 ��� .+��� $��0��$ 
Horizontal=�� -   $��dataGrid��$��1��.+��� -   $��clientsListBox��   MaterialDesignListBox#��[  ���� *
ClientList+���� .R�]� ��. .+�  $
��Left=�$
 User=�$��17��$��17��$��Center=�$��5q��� $
��Left=�$��17(�$��Center=�$	��5 0q����� *Login+�� -   $��filesListBox��#��[  ���� *
FileList+���� .R�]� ��. .+�  $
��Left=�$
 File=�$��17��$��17��$��Center=�$��5q��� $
��Left=�$��17(�$��Center=�$	��5 0q����� *Name+�� -   $��
logTextBox��" �� VerticalScrollBarVisibility$
 Auto=�	 �� 
IsReadOnly	 . $��10 10 10 -2q�$	��Top=����� *Log+.��� -   $��keyBox��$��10q� �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��0��Session Key:�� -	   $	��Key��	 . $	��Top=�$��1��$	��250������ *Key+.���� $��4��$
��Left=�$��	0,2,0,103q�
 �� TextAlignment$
 Center=�$��Oblique$�$��Heavy"�$��WrapWithOverflow=�$	��400��$;��5Generate session key in file or select file with key!��$��15(�� $��4��$��Center=�$��
0,41,29,47q�$	��593��.+���  �� 	IsDefault . $��60��$��0,0,10,0q�$��
Creat file��$	��179��$��26������ *buttonGenerateKey_Click+��  . $��60��$��Seletc file��$��0,0,10,0q�$	��181��$��26������ *buttonLoadKey_Click+��  . $��60��$��	Save file��$	��181��$��26������ *buttonSave_Click+� $��4��$��Right=�$��0,90,48,-70q�