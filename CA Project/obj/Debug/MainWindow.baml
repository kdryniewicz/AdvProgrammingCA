   M S B A M L   `   `   `  ����E  ACA Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:CA_Project
CA_Project  5   	       CA_Project.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:CA_Project     5   	   -   5          � Title$  Mining Game��5	   	   $	��400��6   $	��410��6*    � 
ResizeMode$ NoResize=�66    � SizeToContent$ WidthAndHeight=�6L   .��5
      � .+�5   
   �� -   5        .@�5      �� ��5      �� 5       �� Orientation$ 
Horizontal=�6!   .+�5      �� $��/Resources/mining.png��6    $��25��6?   $��25��6J   �� 5      $��	Main Game��6$   
����  �$��5q�6G   5      .��5      �� $ Vertical=�6   $	��320��64   .+�5      �� $ 
Horizontal=�6!   $��Center=�6:   .+�5      �� -   5         ��Name$ btnMineResources��5      $	��100��67   $��60��6C   $	��top=�6O   $��10q�6g   .��5      �� .+�5      �� $��/Resources/btnMine.png��6$   $��30��6D   $��30��6O   $��0,5,0,0q�6[   �� 5      -   5         ��Name$ lbMineResources��5   (   $��Mine Resources��6A   
���**��$��5q�6j   5      5       �� 5!      -   5        $ btnStartMiners��5!      $	��100��66   $��60��6B   $	��Top=�6N   $��Right=�6f   $��10q�6�   .��5"      �� .+�5#   "   �� $ ��/Resources/startMiners.png��6(   $��50��6L   $��50��6W   $��0,0,0,0q�6c   �� 5$      $��Start Miners��6(   
���"�"�$
��Bold"�6V   $��	5,-10,5,5q�6h   5%      5&      �� 5'      -   5        $ btnStopMiners��5'   !   $	��100��69   $��60��6E   $	��Top=�6Q   $��Right=�6i   $��10q�6�   .��5(      �� .+�5)   "   �� $��/Resources/checkbox.png��6(   $��30��6I   $��30��6T   $��0,5,0,0q�6`   �� 5*   "   $��Stop Miners��6,   
����  �$��5q�6Q   5+      5,      5.      �� 50      $ 
Horizontal=�6!   .+�51      �� $ Vertical=�6%   $	��240��6<   .+�52      �� $+��%Hover over 'miners' to see more info.��6$   
���   �$
��Bold"�6h   $��12(�6z   $	��240��6�   $��0,0,0,0q�6�   $��Center=�6�   �� 54       � ToolTip$T NMiners can be unlocked through mining resources and purchasing from 'shop' tab��6$   $��Miner A:��6}   
��� ���$
��bold"�6�   $��16(�6�   $��75��6�   $��30��6�   $��Center=�6�   � 55      -   5         �Name$ 	barMinerA��55   �   $T NMiners can be unlocked through mining resources and purchasing from 'shop' tab��6*   $��20��6�   $	��130��6�   $��Center=�6�   6�   �� 56      -   5        $��txMinerAValue��56   $   $T NMiners can be unlocked through mining resources and purchasing from 'shop' tab��6:   $��+0��6�   $��16(�6�   $��34��6�   $��30��6�   $��Right=�6�   $��
0,-30,10,0q�6�   �� 59      $T NMiners can be unlocked through mining resources and purchasing from 'shop' tab��6$   $��Miner B:��6}   
��� ���$
��bold"�6�   $��16(�6�   $��75��6�   $��30��6�   $��Center=�6�   $��0,5,0,0q�6�   � 5:      -	   5        $ 	barMinerB��5:   �   $T NMiners can be unlocked through mining resources and purchasing from 'shop' tab��6*   $��20��6�   $	��130��6�   $��Center=�6�   �� 5;      -
   5        $��txMinerBValue��5;   $   $T NMiners can be unlocked through mining resources and purchasing from 'shop' tab��6:   $��+0��6�   $��16(�6�   $��34��6�   $��30��6�   $��Right=�6�   $��
0,-30,10,0q�6�   �� 5=      $T NMiners can be unlocked through mining resources and purchasing from 'shop' tab��6$   $��Miner C:��6}   
��� E��$
��bold"�6�   $��16(�6�   $��75��6�   $��30��6�   $��Center=�6�   $��0,5,0,0q�6�   � 5>      -   5        $ 	barMinerC��5>   �   $T NMiners can be unlocked through mining resources and purchasing from 'shop' tab��6*   $��20��6�   $	��130��6�   $��Center=�6�   �� 5?      -   5        $��txMinerCValue��5?   $   $T NMiners can be unlocked through mining resources and purchasing from 'shop' tab��6:   $��+0��6�   $��16(�6�   $��34��6�   $��30��6�   $��Right=�6�   $��
0,-30,10,0q�6�   5B      �� 5C      .+�5D      �� -   5        $��lbLogs��5D   $   $��History:��62   $��Center=�6E   $
��Bold"�6b   �� 5E      -   5         ��Name$ lbxLogs��5E   &   $	��150��67   $	��150��6C   $��0,10,0,0q�6P   5F      5G      5K      5N      �� 5Q      ��5R      �� 5S      $ 
Horizontal=�6!   .+�5T      �� $��/Resources/shop.png��6    $��25��6=   $��25��6H   �� 5U      $
��Shop��6$   
��� � �$��5q�6C   5V      .��5X      �� $ Vertical=�6   $	��300��65   $	��380��6B   $	��Top=�6N   .+�5Y      �� $��/Resources/merchant.png��6   $��60��6=   $��10,10,260,0q�6I   $	��Top=�6^   �� 5Z      $<��6Please select from below to see what you can purchase.��6    $
��Wrap=�6^   
����  �$��50��6�   $	��Top=�6�   $��130,-50,10,0q�6�   �� 5\      -   5        $��txbShopItems��5\      $	��200��62   $
��auto��6?   $	��Top=�6L   $��5q�6d   .@�5^      �� .��5_      �� -   5        $��btnBuyMiner��5_   %   $	��335��68   $��50��6D   .��5`   "   �� $ 
Horizontal=�6-   .+�5b   &   �� $��/Resources/adventure.png��6,   $��40��6N   $��40��6Y   $
��Left=�6e   $��	10,0,10,0q�6�   �� 5d   &   -   5        $ txtBuyMiner��5d   0   $3��-Unlock the Miners which work to earn gold. - ��6F   $��14(�6{   $
��Wrap=�6�   $
��Bold"�6�   $	��250��6�   $��40��6�   5e   #   5f      5g      �� 5i      .��5j      �� -   5        $ btnUpgradeClickRate��5j   %   $	��335��6B   $��50��6N   .��5k   "   �� $ 
Horizontal=�6-   .+�5m   &   �� $��/Resources/adventure.png��6,   $��40��6N   $��40��6Y   $
��Left=�6e   $��	10,0,10,0q�6�   �� 5o   &   -   5        $ txtUpgClickRate��5o   0   $I��CUpgrade Manual earn rate (+ 1 to manual earn rate) - Costs 20 Gold.��6I   $��14(�6�   $
��Wrap=�6�   $
��Bold"�6�   $	��250��6�   $��40��6�   5p   #   5q      5r      �� 5t      .��5u      �� -   5        $ btnUpgradeMinerSpeed��5u   %   $	��335��6C   $��50��6O   .��5v   "   �� $ 
Horizontal=�6-   .+�5x   &   �� $��/Resources/adventure.png��6,   $��40��6N   $��40��6Y   $
��Left=�6e   $��	10,0,10,0q�6�   �� 5z   &   $E��?Upgrade the Miner Earn Speed (+ 0.1 to speed). - Costs 35 Gold.��60   $��14(�6w   $
��Wrap=�6�   $
��Bold"�6�   $	��250��6�   $��40��6�   5{   #   5|      5}      �� 5~      .��5      �� -   5        $ btnUpgradeMinerEarnRate��5   %   $	��335��6F   $��50��6R   .��5�   "   �� $ 
Horizontal=�6-   .+�5�   &   �� $��/Resources/adventure.png��6,   $��40��6N   $��40��6Y   $
��Left=�6e   $��	10,0,10,0q�6�   �� 5�   &   $L��FUpgrade Miner Earn Rate (+ 0.1 to how much they earn) - Costs 50 Gold.��60   $��14(�6~   $
��Wrap=�6�   $
��Bold"�6�   $	��250��6�   $��40��6�   5�   #   5�      5�      5�      5�      5�      5�      �� 5�   
   $ 
Horizontal=�6   $��Bottom=�6.   $��35��6I   $��5q�6U   .+�5�      �� $��/Resources/gold.png��6   $��30��61   $��30��6<   $��	100,0,0,0q�6H   �� 5�      $��Gold:��6   
��� ���$
��bold"�6;   $��16(�6M   $��60��6[   $��30��6f   $��5,0,0,0q�6r   �� 5�      -   5        	 ��Name$	 txtGold��5�      $��12345678��6%   
��� ���$
��bold"�6O   $��16(�6a   $
��auto��6o   $��30��6|   $��	-10,0,0,0q�6�   5�      5�      5�      