   M S B A M L   `   `   `  ����I  EEskuvo_tervezo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Eskuvo_tervezo.PagesEskuvo_tervezo.Pages  5      E'clr-namespace:Eskuvo_tervezo.PropertiesEskuvo_tervezo.Properties  5      #    Eskuvo_tervezo.Pages.DateView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:Eskuvo_tervezo.Pages     5      >
properties'clr-namespace:Eskuvo_tervezo.Properties     5      b ^MaterialDesignThemes.Wpf, Version=3.2.0.1979, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      -   5          N� Title$  DateView��5
       N� 
FontFamily$ Comic Sans MS)�5       � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $H Bpack://application:,,,/ResourceDictionary/RadioButtonResource.xaml?�6%   6s   �� 5      $Q Kpack://application:,,,/ResourceDictionary/DatePickerResourceDictionary.xaml?�6%   6|   �� 5      $D >pack://application:,,,/ResourceDictionary/LabelDictionary.xaml?�6%   6o   %    5      .��5      �  5   
      StandardBorderBrush �� Color  StandardBackgroundBrush  HoverBorderBrush  SelectedBackgroundBrush  SelectedForegroundBrush  NormalBrush  NormalBorderBrush  HorizontalNormalBrush  HorizontalNormalBorderBrush��%�  &	        &	 #     &	 F     &	 i     &	 �     &	 �     &	 �     &	 �     &	     �� 5      $
 #888��6:   6J   �� 5      $
 #FFF��6>   6M   �� 5      $
 #DDD��67   6G   �� 5      $
 Gray��6>   6N   �� 5      $ White��6>   6O   �� 5       $	 Red��62   6A   �� 5!      $
 Pink��68   6H   �� 5"      $ Black��6<   6M   �� 5#      $ Yellow��6B   6T   5$       � ColumnDefinitions 5&   
   �� 5'      $��1* �6   �� 5(      $��1* �6   �� 5)      $��1* �6   �� 5*      $
��0.5* �6    � RowDefinitions 5-   
   �� 5.      $��1* �6   �� 5/      $
��0.5* �6   �� 50      $
��0.5* �6   �� 51      $��2* �6   �� 52      $��2* �6   �� 53      $��2* �6   .+�56   
   �� -   5         ��Name$ LB_Diary��56      $��0��6   $��0��60   $��4��6@   $	��Top=�6T   $��10q�6l   $��Center=�6x   *   $Eskuvo_tervezo.Properties.ResourceHu	   LB_Diary#��Z	 57      $��34(�6E   �� 58   
   $��1��6   $��2��6"   $��Center=�63   $��Center=�6P   .+�59      	� -   5        
 	�Name$
 	RB_Actual��59   '   $��2��6   $��0��6:   $��Center=�6J   $��10,0,0,5q�6e   $��Stretch=�6w      	RB_Actual#��Z 5:      6d   	� 5;      -   5        $
 RB_All��5;   '   $��1��6   $��0��67   $��Center=�6G   $��10,0,0,5q�6b   $��Stretch=�6t      RB_All#��Z 5<      6^   	� 5=      -   5        $
 RB_Interval��5=   '   $��3��6   $��0��6<   $��Center=�6L   $
��10,0q�6g   $��Stretch=�6u      RB_Interval#��Z 5>      6h   5?      �� 5@   
   -   5        $ LB_StartingInterval��5@      $��1��6   $��1��6;   $��1��6K   $��2��6_      LB_StartingInterval#��Z 6p   $��Stretch=�6�   $��
40,8,40,49q�6�   $
��auto��6�   $��Center=�6�   $��Center=�6  $��Center=�6-  (  "System.Windows.Controls.DatePicker  5C   
   -   5          Name$ 	DateStart��5C      $��20(�6(   {�. $
��auto��5D      $��1��6!   $��1��6.   $��1��6>   $��2��6R      FirstDayOfWeek$ Monday=�6c    � Cursor$
 Hand��6{   $��40,39,15,19q�6�   $��Center=�6�   $��Center=�6�   $��Center=�6�      SelectedDateFormat$ Short=�6�    	 ButtonBorderBrush#��� 	 5E      $��0q�6G    
 TextForeColors#��� 
 6[    J� 	AllowDrop . 4  .System.Windows.Automation.AutomationProperties   IsColumnHeader .    IsRowHeader . |�.   K� ZIndex$ 2��5F      ��6,   Z�  5G      :  4System.Windows.Controls.Primitives.DatePickerTextBox �� 
TargetType �� Property �� Value ��Name $  TextBoxAndPasswordUnderlineBrush.  (MaterialDesignThemes.Wpf.TextFieldAssist   UnderlineBrush �� 
IsReadOnly)  #MaterialDesignThemes.Wpf.HintAssist   Hint  TextBorderBrush  TextCaretBrush �� 
CaretBrush  TextBoxSelectionBrush �� SelectionBrush  �� Path! �� StringFormat" �� RelativeSource# �� AncestorType��%�  '        �� 5H      "  6   .�5J      ��  � �� 5K      �� 5L      .R�5M   "   �� $ PART_TextBox��6�   # �  6+    . $  ��5O   )   $��Center=�6K   $��Stretch=�6f   $��20(�6�   $��Center=�6�   $��Center=�6�   $
��auto��5P   )   $	 Pen��5Q   )   #���  66   # �  6f   # �  5R   *   #��� 
 6c   ��5M   �   �� $  SelectedDate�$! dd MMM yyyy��" �� "#  ��5S   *   �� *ActualWidth+" �� *FindAncestor+"#  5U      5W      5X      5Y      5Z      �� 5[   
   -   5        $ LB_EndingInterval��5[      $��1��6   $��2��69   $��1��6I   $��2��6]   $   LB_EndingInterval#��Z$ 6n   $��Stretch=�6�   $��
40,8,40,49q�6�   $
��auto��6�   $��Center=�6�   $��Center=�6  $��Center=�6)    5^   
   -	   5        $ DateEnd��5^      $��20(�6&   $
��auto��5_      $��1��6!   $��2��6.   $��1��6>   $��2��6R   $ Monday=�6c   $
 Hand��6{   $��40,39,15,19q�6�   $��Center=�6�   $��Center=�6�   $��Center=�6�   $ Short=�6�   #��� 	 5`      $��0q�6G     ButtonBackGround#���  6[   #��� 
 6�    .  .  . |�.  $ 2��5a       5c      ��%�  '        �� 5d      "  6   .�5e      ��  � �� 5f      �� 5g      .R�5h   "   �� $ PART_TextBox��6�   # �  6+    . $  ��5j   )   $��Center=�6K   $��Stretch=�6f   $��20(�6�   $��Center=�6�   $��Center=�6�   $
��auto��5k   )   $	 Pen��5l   )   #���  66   # �  6f   # �  6�   #��� 
 6�   ��5h   �   �� $  SelectedDate�$! dd MMM yyyy��" �� "#  ��5l   �   �� *ActualWidth+" �� *FindAncestor+"#  5n      5p      5q      5r      5s      �� 5u   
   -
   5        % ��Name$% 	BT_Search��5u   ?   $��1��6   $��2��6   $��3��6/   &   	BT_Search#��Z& 6R   $��Center=�6�   $
 Hand��6�   $ 4��6�   
������ +  %MaterialDesignThemes.Wpf.ButtonAssist'   CornerRadius$' 20��5v      $	��120��6C   $��40��6O   $��
UltraBlack"�6[   $��Center=�5w      $��Center=�67   $��20(�6Y   $��Center=�6g   #���  5x      #��� 
 6E   6s   �� 5z   
   $
��Auto=�6   $
��Auto=�6<   $��3��6_   $��0��6l   $��4��6|   $��3��6�   $��29,10,11,30q�6�   .��5{      �� -   5        ( ��Name$( DataViewItems��5{      $��3��60   $��4��6=   $��0��6N   $��4��6^   $��Stretch=�5|      $��Stretch=�67   5}      5~      5�      5�      