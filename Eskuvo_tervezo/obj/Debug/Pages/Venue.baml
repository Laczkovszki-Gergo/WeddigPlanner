   M S B A M L   `   `   `  ����I  EEskuvo_tervezo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Eskuvo_tervezo.PagesEskuvo_tervezo.Pages  5      E'clr-namespace:Eskuvo_tervezo.PropertiesEskuvo_tervezo.Properties  5           Eskuvo_tervezo.Pages.Venue   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:Eskuvo_tervezo.Pages     5      >
properties'clr-namespace:Eskuvo_tervezo.Properties     5      b ^MaterialDesignThemes.Wpf, Version=3.2.0.1979, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      -   5          N� 
FontFamily$  Comic Sans MS)�5       N� Title$ Venue��5       � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $D >pack://application:,,,/ResourceDictionary/LabelDictionary.xaml?�6%   6o   �� 5      $H Bpack://application:,,,/ResourceDictionary/TextblockDictionary.xaml?�6%   6s   �� 5      $G Apack://application:,,,/ResourceDictionary/ComboBoxDictionary.xaml?�6%   6r   %    5      .��5      �  � ColumnDefinitions 5   
   �� 5   
   $
��2.5* �6   �� 5   
   $
��1.5* �6   �� 5   
   $��1* �6   �� 5   
   $��1* �6    � RowDefinitions 5    
   �� 5!      $
��1.2* �6   �� 5"      $��2* �6   �� 5#      $��2* �6   �� 5$      $
��8.8* �6   �� 5%      $��1* �6   .+�5(   
   �� -   5         ��Name$ LB_Venue��5(      $��0��6   $��0��60   $��4��6@   $	��Top=�6T   $��5q�6l   $��Center=�6w   *   $Eskuvo_tervezo.Properties.ResourceHu   LB_Venue#��Z 5)      $��34(�6E   �� 5*   
   -   5        $ LB_VenueName��5*   .   $��1��6   $��0��6   	   LB_VenueName#��Z	 6D   $��Stretch=�6|   $��0,0,0,40q�6�   $
��auto��6�   $��Center=�6�   $��Center=�6�   $��Center=�6�   �� 5-   
   -   5        
 ��Name$
 TB_Venue��5.   R    $   TextBoxAndPasswordUnderlineBrush.  (MaterialDesignThemes.Wpf.TextFieldAssist   UnderlineBrush# �   5-      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$  ��6x   $��Center=�6�   $��Stretch=�6�   $��20(�6�   $
��auto��5.      $��1��6!   $��0��6.   $��
40,20,40,0q�6>    �� 	MaxLength$ 60��6d    � Cursor$	 Pen��5/        TextBorderBrush#���  6      TextCaretBrush �� 
CaretBrush# �  6P     TextBoxSelectionBrush �� SelectionBrush# �  6~     TextForeColors#���  6�   6�   '  !MaterialDesignThemes.Wpf.PackIcon  50   
      Kind$ PartyPopper=�6"   $��Center=�65   $��	10,25,0,0q�6P   $��30��6c   $��30��6n   $��1��6{   $��0��6�    K� ZIndex$ 9��6�     SpecialIconBackGround#���  6�   6�   �� 52   
   -   5        $ 
LB_Address��52      $��1��6   $��1��62   $��2��6B      
LB_Address#��Z 6V   $��Stretch=�6�   $��0,0,0,40q�6�   $
��auto��6�   $��Center=�6�   $��Center=�6�   $��Center=�6  �� 55   
   -   5        $
 
TB_Address��56   f   # �   55      $  ��6x   $��Center=�6�   $��Stretch=�6�   $��20(�6�   $
��auto��56      $��1��6!   $��1��6.   $��2��6>   $��
40,20,40,0q�6R   $ 60��6z   $	 Pen��57      #���  6    # �  6P   # �  6~   #���  6�   $ 1��6�   6�     58   
   $ Place=�6"   $��Center=�6/   $��	10,25,0,0q�6J   $��30��6]   $��30��6h   $��1��59   "   $��1��6/   $ 9��6?   #���  6P   5:      �� 5<   
   -   5         ��Name$ BT_Save��5<   .   $��1��6   $��3��6      BT_Save#��Z 6?   $��Center=�6s   $
 Hand��6�   $ 2��6�   +  %MaterialDesignThemes.Wpf.ButtonAssist   CornerRadius$ 20��5=      $��
10,20,10,0q�6C   $	��120��6W   $��40��6c   $��
UltraBlack"�6o   $��Center=�5>      $��Center=�67   $��20(�6Y   $��Center=�6g   #���  5?      #���  6E     ButtonBackGround#���  6s   6�   �� 5A   
   -   5        $ BT_BrowsePicture��5A   .   $��2��6   $��1��6      BT_BrowsePicture#��Z 6H   $��Stretch=�6�   $��40��6�   $
 Hand��6�   $ 4��6�   $ 20��5B      $��
10,20,10,0q�6C   $��
UltraBlack"�6W   $��Center=�5C      $��Center=�67   $��20(�6Y   $��Center=�6g   #���  5D      #���  6E   #���  6s   6�   �� 5F   
   -	   5        $ 	BT_Delete��5F   .   $��2��6   $��3��6      	BT_Delete#��Z 6A   $��Center=�6w   $
 Hand��6�   $ 6��6�   $ 20��5G      $��
10,20,10,0q�6C   $	��160��6W   $��40��6c   $��
UltraBlack"�6o   $��Center=�5H      $��Center=�67   $��20(�6Y   $��Center=�6g   #���  5I      #���  6E   #���  6s   6�   �� 5K   
   -
   5        $ BT_Modification��5K   .   $��2��6   $��2��6      BT_Modification#��Z 6G   $��Center=�6�   $
 Hand��6�   $ 5��6�   $ 20��5L      $��
10,20,10,0q�6C   $	��160��6W   $��40��6c   $��
UltraBlack"�6o   $��Center=�5M      $��Center=�67   $��20(�6Y   $��Center=�6g   #���  5N      #���  6E   #���  6s   6�   �� 5P   
   -   5        $ 	LB_Venues��5P   .   $��2��6   $��0��6      	LB_Venues#��Z 6A   $��Stretch=�6v   $��40,-20,40,40q�6�   $
��auto��6�   $��Center=�6�   $��Center=�6�   �� 5S   
   -   5         ��Name$ CB_Venue��5S      $��2��6%   $��0��62   $
��auto��6B   $��40��6O   $��Center=�6[   $��Stretch=�6v   $��
20,20,20,0q�6�   $��20(�6�    �� IsDropDownOpen .  $ 3��6�   ��5U      �� 5V      .R�5W      ��  �� Orientation$ 
Horizontal=�6!   .+�5X      �� $	��5,0q�6:   $��Stretch=�6G   $��Center=�6e   $��20(�6�   ��6    �� *Venue+6�   �� 5Y      $	��5,0q�6<   $��Stretch=�6I   $��Center=�6g   $��20(�6�   ��6    �� *	Address+6�   5Z      5[      5]      �� 5_      -   5         ��Name$ PicsScrollViewer��5_      $
��Auto=�65   $
��Auto=�6Z   $��3��6}   $��0��6�   $��4��6�   $��	20,0,20,0q�6�   ��.  $ 7��6�   .��5`      �� -   5         ��Name$ PicsBox��5`      $��Stretch=�6(   $	��Top=�6F   ��5a      �� 5b        �� 
TargetType"  ��6   .�5c      �� ! �� Property! � ��" �� Value$" Center=�6G   5d      ��5f      �� 5g      .R�5h      A� # A� Columns$# 3��6&   $��Stretch=�62   $��Stretch=�6P   5i      ��5k      �� 5l      .R�5m      �� $ ��Name$$ 	ItemPanel��6%   $��Stretch=�68   $��Stretch=�6T   .+�5n      �� $ 
Horizontal=�6)   $��Stretch=�6B   $��Right=�6^   .+�5o   "     %  Name$% Tooltip_Download��6:   -   5        $ Download=�5o   T   $	��Top=�6   $��Right=�6�   $��30��6�   $��30��6�   $��20(�6�   #���  5p   6   $
 Hand��6k   & � DataContext& 5o   d   �� *ID+5p   �     5q   "   $% Tooltip_Delete��6:   -   5        $ DeleteForever=�5q   S   $	��Top=�6�   $��Right=�6�   $��30��6�   $��30��6�   $��20(�6�   #���  5r   6   $
 Hand��6k   & 5q   h   �� *ID+5r   �   5s      �� 5u      -   5        $��Stretch=�5u   A   $	��Top=�6_   $��UniformToFill=�6w   $
 Hand��6   ��6$   �� *	ImageData+��6�   �� *Wid+��6�   �� *Hei+6   �� 5v      $��Center=�6?   $��Bottom=�6\     InputImageTblStyle#��[ 6�   ��6(   �� *Title+��6w   �� *Wid+��6�   �� *Hei+6�   5w      5x      5z      5{      �� 5}   
   -   5        $ BT_DownloadPicture��5}   B   $��4��6   $��0��6   $��4��6.   '   BT_DownloadPicture#��Z' 6^   $��Center=�6�   $
 Hand��6�   $ 5��6�   $ 20��5~      $	��280��6C   $��40��6O   $��
UltraBlack"�6[   $��Center=�5      $��Center=�67   $��20(�6Y   $��Center=�6g   #���  5�      #���  6E   #���  6s   6�   5�      5�      