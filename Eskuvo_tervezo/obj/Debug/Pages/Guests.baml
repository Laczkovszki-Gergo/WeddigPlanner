   M S B A M L   `   `   `  ����I  EEskuvo_tervezo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Eskuvo_tervezo.PagesEskuvo_tervezo.Pages  5      E'clr-namespace:Eskuvo_tervezo.PropertiesEskuvo_tervezo.Properties  5      !    Eskuvo_tervezo.Pages.Guests   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:Eskuvo_tervezo.Pages     5      >
properties'clr-namespace:Eskuvo_tervezo.Properties     5      b ^MaterialDesignThemes.Wpf, Version=3.2.0.1979, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      -   5          N� Title$  Guests��5
       N� 
FontFamily$ Comic Sans MS)�5       � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $D >pack://application:,,,/ResourceDictionary/LabelDictionary.xaml?�6%   6o   %    5      .��5      �  � ColumnDefinitions 5   
   �� 5      $��2* �6   �� 5      $��1* �6   �� 5      $��2* �6   �� 5      $��1* �6    � RowDefinitions 5   
   �� 5      $��1* �6   �� 5       $��3* �6   �� 5!      $��6* �6   �� 5"      $��1* �6   .+�5%   
   �� -   5         ��Name$ 	LB_Guests��5%      $��0��6   $��0��61   $��4��6A   $	��Top=�6U   $��10q�6m   $��Center=�6y   *   $Eskuvo_tervezo.Properties.ResourceHu   	LB_Guests#��Z 5&      $��34(�6F   �� 5'   
   -   5        $ LB_Bride��5'      $��1��6   $��0��60   $��2��6@   $	��Top=�6T   $��Center=�6l   	   LB_Bride#��Z	 5(      $��34(�6E   �� 5)   
   -   5        $ LB_Groom��5)      $��1��6   $��2��60   $��2��6@   $	��Top=�6T   $��Center=�6l   
   LB_Groom#��Z
 5*      $��34(�6E   �� 5,   
   -   5        $ LB_BrideName��5,      $��1��6   $��0��64   $��Center=�6D   $��Center=�6_   $��
10,0,20,40q�6|      LB_BrideName#��Z 5-      �� 5.   
   -   5         ��Name$ TB_BrideGuests��5/   R    $   TextBoxAndPasswordUnderlineBrush.  (MaterialDesignThemes.Wpf.TextFieldAssist   UnderlineBrush# �   5.      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$  ��6x   $��Center=�6�   $��Stretch=�6�   $��20(�6�   $
��auto��5/      $��1��6!   $��0��6.   $��
40,20,20,0q�6>    �� 	MaxLength$ 50��6j    � Cursor$	 Pen��50        TextBorderBrush#���  6      TextCaretBrush �� 
CaretBrush# �  6P     TextBoxSelectionBrush �� SelectionBrush# �  6~     TextForeColors#���  6�   6�   '  !MaterialDesignThemes.Wpf.PackIcon  51   
      Kind$ 	PeopleAdd=�6"   $��Center=�63   $��5,25,0,0q�6N   $��30��6`   $��30��6k   $��1��6y   $��0��6�    K� ZIndex$ 9��6�     SpecialIconBackGround#���  6�   6�   �� 52   
   -   5        $ LB_BrideCount��52      $��1��6   $��1��65   $��Center=�6E   $��Center=�6`   $��
10,0,20,40q�6}      LB_BrideCount#��Z 53      �� 54   
   -   5        $ TB_BrideGuestsCount��55   R   # �   54      $  ��6x   $��Center=�6�   $��Stretch=�6�   $��20(�6�   $
��auto��55      $��1��6!   $��1��6.   $��
40,20,40,0q�6>   $	 Pen��56      #���  6    # �  6P   # �  6~   #���  6�   $ 1��6�   6�     58   
   $ Counter=�6"   $��Center=�61   $
��Left=�6L   $��	5,25,20,0q�6g   $��30��6z   $��30��6�   $��1��6�   $��1��6�   $ 9��6�   #���  6�   6�   �� 59   
   -	   5         ��Name$ BT_SaveBride��59      $��1��6      BT_SaveBride#��Z 64   $��Center=�6m   $
 Hand��6�   $ 2��6�   $��1��6�   +  %MaterialDesignThemes.Wpf.ButtonAssist   CornerRadius$ 20��5:      $��20,20,20,10q�6A   $	��120��6V   $��40��6b   $��
UltraBlack"�6n   $��Center=�5;      $��Center=�67   $��20(�6Y   $��Bottom=�6g     ButtonBorderBrush#���  5<        ButtonBackGround#���  6G   #���  6w   6�   �� 5=   
   -
   5        $ LB_BrideGuestCount��5=      $��1��6   $��0��6:   $��Bottom=�6J   $��10,10,10,10q�6e   $��Center=�6z      LB_BrideGuestCount#��Z 5>      �� 5?   
   -   5        $ LB_GroomName��5?      $��1��6   $��2��64   $��Center=�6D   $��Center=�6_   $��
10,0,20,40q�6|      LB_GroomName#��Z 5@      �� 5A   
   -   5        $ TB_GroomGuests��5B   R   # �   5A      $  ��6x   $��Center=�6�   $��Stretch=�6�   $��20(�6�   $ 50��6�   $
��auto��5B      $��1��6!   $��2��6.   $��
40,20,40,0q�6>   $	 Pen��5C      #���  6    # �  6P   # �  6~   #���  6�   $ 3��6�   6�     5D   
   $ 	PeopleAdd=�6"   $��Center=�63   $��5,25,0,0q�6N   $��30��6`   $��30��6k   $��1��6y   $��2��6�   $ 9��6�   #���  6�   6�   �� 5E   
   -   5        $ LB_GroomCount��5E      $��1��6   $��3��65   $��Center=�6E   $��Center=�6`   $��
10,0,20,40q�6}      LB_GroomCount#��Z 5F      �� 5G   
   -   5        $ TB_GroomGuestsCount��5H   R   # �   5G      $  ��6x   $��Center=�6�   $��Stretch=�6�   $��20(�6�   $
��auto��5H      $��1��6!   $��3��6.   $��
40,20,40,0q�6>   $	 Pen��5I      #���  6    # �  6P   # �  6~   #���  6�   $ 4��6�   6�     5K   
   $ Counter=�6"   $��Center=�61   $
��Left=�6L   $��	5,25,20,0q�6g   $��30��6z   $��30��6�   $��1��6�   $��3��6�   $ 9��6�   #���  6�   6�   �� 5L   
   -   5        $ BT_SaveGroom��5L      $��1��6      BT_SaveGroom#��Z 64   $��Right=�6m   $
 Hand��6�   $ 5��6�   $��3��6�   $ 20��5M      $��20,25,20,10q�6C   $	��120��6X   $��40��6d   $��
UltraBlack"�6p   $��Center=�5N      $��Center=�67   $��20(�6Y   $��Bottom=�6g   #���  5O      #���  6G   #���  6w   6�   �� 5P   
   -   5        $ LB_GroomGuestCount��5P      $��1��6   $��2��6:   $��Bottom=�6J   $��10q�6e   $��Center=�6q      LB_GroomGuestCount#��Z 5Q      �� 5S   
   $��Stretch=�6   $
��Auto=�65   $
��Auto=�6Z   $��2��6}   $��0��6�   $��2��6�   $��20q�6�     ScrollViewerBorderBrush#���  6�   $��2q�6�   $ 6��6  .��5T      �� -   5         ��Name$ 
BrideItems��5T      $��1��6-   $��0��6:   $��2��6J   $��Stretch=�5U      $��Stretch=�67   5V      5W      �� 5X   
   $��Stretch=�6   $
��Auto=�65   $
��Auto=�6Z   $��2��6}   $��2��6�   $��2��6�   $��20q�6�   #���  6�   $��2q�6�   $ 9��6  .��5Y      �� -   5        $ 
GroomItems��5Y      $��1��6-   $��2��6:   $��2��6J   $��Stretch=�5Z      $��Stretch=�67   5[      5\      �� 5^   
   -   5        $ BT_ExportToExcel��5^   B   $��7��6   $��0��6   $��4��6.      BT_ExportToExcel#��Z 6\   $��Center=�6�   $
 Hand��6�   $ 4��6�   
������ $ 20��5_      $	��280��6C   $��40��6O   $��
UltraBlack"�6[   $y�Visible=�6s   $��Center=�5`      $��Center=�67   $��20(�6Y   $��Center=�6g   #���  5a      #���  6E   6s   5c      5d      