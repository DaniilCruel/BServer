   M S B A M L   `   `   `  ����A  =Server, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:ServerServer  5   	   5clr-namespace:Server.ViewModelsServer.ViewModels  5   	       Server.ServerWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   V RMaterialDesignThemes.Wpf, Version=2.5.0.1205, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	    localclr-namespace:Server  5   	   0
ViewModelsclr-namespace:Server.ViewModels  5   	     � Title$  Server��5
   	   $	��450��6   $	��800��6%    � WindowStartupLocation$ CenterScreen=�61   .��5      � -   5         �Name$ mainGrid��5       � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      .+�5   
   � -   5        $ leftMenuGrid��5      $	��150��6%   $��0��61   $
��Left=�6A   ���	#FF32558B � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    .+�5      �� -   5         ��Name$ listViewMenu��5   \   $��Disabled=�6   $��0��6N   
�������$��Microsoft JhengHei Light)�5   .   $��18(�6U   .@�5      �� $
��Auto��6   .��5      ��  �� Orientation$ 
Horizontal=�6!   .+�5      '  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.5.0.1205, Culture=neutral, PublicKeyToken=null]]   Kind$ People=�62   $��25��6@   $��25��6K   $��Center=�6W   $��5q�6r   �� 5      $��Clients��6$   $��17(�63   $��Center=�6A   $	��5 0q�6\   5      5      �� 5      $
��Auto��6   .��5      �� $ 
Horizontal=�6!   .+�5        $ Files=�62   $��25��6?   $��25��6J   $��Center=�6V   $��5q�6q   �� 5       $��Files��6$   $��17(�61   $��Center=�6?   $	��5 0q�6Z   5!      5"      �� 5#      $
��Auto��6   .��5$      �� $ 
Horizontal=�6!   .+�5%        $ Pencil=�62   $��25��6@   $��25��6K   $��Center=�6W   $��5q�6r   �� 5&      $	��Log��6$   $��17(�6/   $��Center=�6=   $	��5 0q�6X   5'      5(      �� 5)      $
��Auto��6   .��5*      �� $ 
Horizontal=�6!   .+�5+        $	 Key=�62   $��25��6=   $��25��6H   $��Center=�6T   $��5q�6o   �� 5,      $	��Key��6$   $��17(�6/   $��Center=�6=   $	��5 0q�6X   5-      5.      5/      50      � 51   
   $��1��6    52      �� 53      $��20 �6    �� 54      .+�56      �� $��0��6   $ 
Horizontal=�6'   58      � 59      -   5        $��dataGrid��59      $��1��6#   .+�5:      �� -   5        $��clientsListBox��5:         MaterialDesignListBox#��[  60   ��6_   �� *
ClientList+��5;      �� 5<      .R�5>      ]� ��. .+�5?   "     $
��Left=�6:   $
 User=�6P   $��17��6\   $��17��6g   $��Center=�6s   $��5q�6�   �� 5@   "   $
��Left=�6,   $��17(�6Y   $��Center=�6g   $	��5 0q�6�   ��6B   �� *Login+6�   5B      5C      5E      �� 5F      -   5        $��filesListBox��5F      #��[  6.   ��6]   �� *
FileList+��5G      �� 5H      .R�5J      ]� ��. .+�5K   "     $
��Left=�6:   $
 File=�6P   $��17��6\   $��17��6g   $��Center=�6s   $��5q�6�   �� 5L   "   $
��Left=�6,   $��17(�6X   $��Center=�6f   $	��5 0q�6�   ��6B   �� *Name+6�   5N      5O      5Q      �� 5R      -   5        $��
logTextBox��5R      " �� VerticalScrollBarVisibility$
 Auto=�6,   	 �� 
IsReadOnly	 . $��10 10 10 -2q�6a   $	��Top=�5S      ��64   �� *Log+.��6I   5T      � 5U      -   5        $��keyBox��5U      $��10q�6%    5V      �� 5W      $
��Auto �6(   �� 5X      $
��Auto �6(   �� 5Y      $
��Auto �6(   �� 5Z      $
��Auto �6(   �� 5[      $
��Auto �6(   .+�5^      �� $��0��6    Session Key:6;   �� 5_      -	   5        $	��Key��5_      	 . $	��Top=�6P   $��1��6h   $	��250��6u   ��6;   �� *Key+.��6�   5a      �� 5b      $��4��6    $
��Left=�6-   $��	0,2,0,103q�6H   
 �� TextAlignment$
 Center=�6[   $��Oblique$�6r   $��Heavy"�6�   $��WrapWithOverflow=�6�   $	��400��6�   $;��5Generate session key in file or select file with key!��6�   $��15(�6  � 5d      $��4��6    $��Center=�6-   $��
0,41,29,47q�6J   $	��593��6^   .+�5e      ��  �� 	IsDefault . $��60��6`   $��0,0,10,0q�6n   $��
Creat file��6�   $	��179��6�   $��26��6�   ��64   �� *buttonGenerateKey_Click+6�   �� 5f       . $��60��6\   $��Seletc file��6j   $��0,0,10,0q�6�   $	��181��6�   $��26��6�   ��64   �� *buttonLoadKey_Click+6�   �� 5g       . $��60��6Y   $��	Save file��6g   $	��181��6{   $��26��6�   ��64   �� *buttonSave_Click+6�   5h      � 5i      $��4��6    $��Right=�6-   $��0,90,48,-70q�6I   5k      5m      5n      5o      5p      