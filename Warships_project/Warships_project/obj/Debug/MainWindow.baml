   M S B A M L   `   `   `  ����K  GWarships_project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Warships_projectWarships_project  5   	   !    Warships_project.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:Warships_project     5   	   -   5          � Title$  Warships 1.0��5   	   $	��720��6   $
��1216��6+    � 	Resources 5
         FocusVisual �� Property �� Value J� SnapsToDevicePixels �� StrokeDashArray  Button.Static.Background �� Color  Button.Static.Border  Button.MouseOver.Background  Button.MouseOver.Border  Button.Pressed.Background  Button.Pressed.Border  Button.Disabled.Background  Button.Disabled.Border 	 Button.Disabled.Foreground �� 
TargetType �� 
TargetType	 ��Name
 ��Name �� Triggers P� Property P� Value �� 
TargetName��%  &	        &	 �     &	 �     &	 �     &	     &	 2    &	 U    &	 x    &	 �    &		 �    '�� �    7   7  7  7  7  7  7  7  7 	 �� 5   
   .�5      ��  � �� 5      �� 5      .R�5      �� $��2q�6$    . #��� ��6J   $��1��6�   $	 1 2R�6�   5      5      5      �� 5   
   $ 	#FFDDDDDD��6;   �� 5   
   $ 	#FF707070��67   �� 5   
   $ 	#FFBEE6FD��6>   �� 5   
   $ White��6:   �� 5   
   $ 	#FFC4E5F6��6<   �� 5   
   $ 	#FF2C628B��68   �� 5   
   $ 	#FFF4F4F4��6=   �� 5   
   $ 	#FFADB2B5��69   �� 5   
   $ 	#FF838383��6=   �� 5   
   " ��6   .�5      ��  �@�FocusVisualStyle8   61   �� 5       � ��8  6+   �� 5        � ��8  6,   �� 5!       � ��# � ��6+   �� 5"       � ��$ 1q�60   �� 5#       � ��$ Center=�6;   �� 5$       � ��$ Center=�69   �� 5%       � ��$ 1q�6(   �� 5&       � �� 5'      �� 5(      " ��6&   .R�5)      �� $	 border��6!   #��z��61   #��z��6]   #��z��6�    . ._�5*      �� $
 contentPresenter��6/   |�.  #��z��6[   #��z��6�   ��. # z 6�   #��z��6  5+       5,      P� 5-       �@��IsDefaulted . . �5.   "   �� $ border��6@   # � ��6T    � ��5/      P� 50       �@J�IsMouseOver . . �52   "   �� $ border��6@   8   � ��6T   54      P� 55       � �� . . �56   "   �� $ border��6?   8   � ��6S   �� 57   "   $ border��6@   8   � ��6T   58      P� 59       � {� .  . �5:   "   �� $ border��6?   8   � ��6S   �� 5;   "   $ border��6@   8   � ��6T   �� 5<   "   $ contentPresenter��6K   8   � ��6i   5=      5?      5A      5B      5C      .��5D      �� 
�������.+�5E   
   o� $	��Top=�6   $��25��6'   $��14(�63   $��SemiBold"�6A   .@�5F      m� $
��File��6   .@�5G      m� -   5        $��Reset settings��5G      63   m� 5H      -   5        $
��Help��5H      6)   m� 5I      -   5        $
��Exit��5I      6)   5J      m� 5K      -   5         m�Name$ SizeOfBoardSettings��5K      $��Size of board��64    � ToolTip$) #Vertical fields x horizontal fields��6K   .@�5L      m� -   5        $��10 x 10 fields��5L      63   m� 5M      -   5        $��15 x 15 fields��5M      63   m� 5N      -   5        $��20 x 20 fields��5N      63   5O      m� 5P      -	   5        $ AmountOfShipsSettings��5P      $��Amount of ships��66   $B <4 field ships, 3 field ships, 2 field ships, one field ships��6O   .@�5Q      m� -
   5        $ SmallAmount��5Q   /   $��
1, 2, 2, 3��6   6D   m� 5R      -   5        $ NormalAmount��5R   /   $��
1, 2, 3, 4��6   6E   m� 5S      -   5        $ 	BigAmount��5S   /   $��
1, 2, 4, 5��6   6B   5T      5U      � 5V   
   
������� � ColumnDefinitions 5W      �� 5X      $	��600 �6#   �� 5Y      $	��600 �6#   .+�5[      A� -   5         A�Name$ FirstUserGrid��5[      $��0��61   
����=H�$	��600��6\   $��Stretch=�6i   $��5 5 5 5q�6�   .+�5\      �� $��
User board��6   $��50(�6.   $��Center=�6<   $��Center=�6Y   5]      A� 5^      -   5        $ ComputerGrid��5^      $��1��60   
����=H�$	��600��6[   $��Stretch=�6h   $��5 5 5 5q�6�   .+�5_      �� $��Computer board��6   $��50(�62   $��Center=�6@   $��Center=�6]   5`      5a      � 5b   
   $��	70 0 10 0q�6    5c      �� 5d      $	��180 �6#   �� 5e      $	��140 �6#   �� 5f      $	��100 �6#   �� 5g      $��60 �6#   �� 5h      $	��100 �6#   �� 5i      $	��180 �6#   �� 5j      $	��140 �6#   �� 5k      $	��100 �6#   �� 5l      $��60 �6#   .+�5n      �� -   5         ��Name$ r4��5n      $	��160��6$   $��40��60   
�����F�$��0��6M   �� 5o      -   5         ��Name$	 r4a��5o      $��0��6%   $��Center=�65   $��16(�6R   $��0 5 0 0q�6`   $��	ExtraBold"�6q   $��Center=�6�   �� 5p      -   5        $ r3��5p      $	��120��6$   $��40��60   
�����F�$��1��6M   �� 5q      -   5        $	 r3a��5q      $��1��6%   $��Center=�65   $��16(�6R   $��0 5 0 0q�6`   $��	ExtraBold"�6q   $��Center=�6�   �� 5r      -   5        $ r2��5r      $��80��6$   $��40��6/   
�����F�$��2��6L   �� 5s      -   5        $	 r2a��5s      $��2��6%   $��Center=�65   $��16(�6R   $��0 5 0 0q�6`   $��	ExtraBold"�6q   $��Center=�6�   �� 5t      -   5        $ r1��5t      $��40��6$   $��40��6/   
�����F�$��3��6L   �� 5u      -   5        $	 r1a��5u      $��3��6%   $��Center=�65   $��16(�6R   $��0 5 0 0q�6`   $��	ExtraBold"�6q   $��Center=�6�   �� 5v      -   5        $ s4��5v      $	��160��6$   $��40��60   
�����F�$��5��6M   �� 5w      -   5        $	 r4b��5w      $��5��6%   $��Center=�65   $��16(�6R   $��0 5 0 0q�6`   $��	ExtraBold"�6q   $��Center=�6�   �� 5x      -   5        $ s3��5x      $	��120��6$   $��40��60   
�����F�$��6��6M   �� 5y      -   5        $	 r3b��5y      $��6��6%   $��Center=�65   $��16(�6R   $��0 5 0 0q�6`   $��	ExtraBold"�6q   $��Center=�6�   �� 5z      -   5        $ s2��5z      $��80��6$   $��40��6/   
�����F�$��7��6L   �� 5{      -   5        $	 r2b��5{      $��7��6%   $��Center=�65   $��16(�6R   $��0 5 0 0q�6`   $��	ExtraBold"�6q   $��Center=�6�   �� 5|      -   5        $ s1��5|      $��40��6$   $��40��6/   
�����F�$��8��6L   �� 5}      -   5        $	 r1b��5}      $��8��6%   $��Center=�65   $��16(�6R   $��0 5 0 0q�6`   $��	ExtraBold"�6q   $��Center=�6�   5~      5      5�      