   M S B A M L   `   `   `  ����I  EEskuvo_tervezo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Eskuvo_tervezo.PagesEskuvo_tervezo.Pages  5      E'clr-namespace:Eskuvo_tervezo.PropertiesEskuvo_tervezo.Properties  5      %    Eskuvo_tervezo.Pages.FirstSteps   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:Eskuvo_tervezo.Pages     5      >
properties'clr-namespace:Eskuvo_tervezo.Properties     5      b ^MaterialDesignThemes.Wpf, Version=3.2.0.1979, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      -   5          N� Title$  
FirstSteps��5       N� 
FontFamily$ Comic Sans MS)�5       � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $Q Kpack://application:,,,/ResourceDictionary/DatePickerResourceDictionary.xaml?�6%   6|   �� 5      $D >pack://application:,,,/ResourceDictionary/LabelDictionary.xaml?�6%   6o   %    5      .��5      � -   5         �Name$ FirstStepsGrid��5       � ColumnDefinitions 5   
   �� 5      $��1* �6   �� 5      $��2* �6   �� 5      $��2* �6   �� 5      $��2* �6   �� 5      $��2* �6   �� 5       $��1* �6    � RowDefinitions 5#   
   �� 5$      $��1* �6   �� 5%      $��1* �6   �� 5&      $��1* �6   �� 5'      $��1* �6   �� 5(      $��1* �6   �� 5)      $��2* �6   .+�5,   
   �� -   5         ��Name$ LB_FirstSteps��5,      $��0��6   $��0��65   $��8��6E   $	��Top=�6Y   $��0,10,0,0q�6q   $��Center=�6�   *   $Eskuvo_tervezo.Properties.ResourceHu	   LB_FirstSteps#��Z	 5-      $��34(�6J   �� 5/   
   -   5        $ LB_Bride��5/      $��1��6   
   LB_Bride#��Z
 60   $��Stretch=�6d   $��
40,9,40,48q�6�   $
��auto��6�   $��Center=�6�   $��Center=�6�   $��Center=�6�   $��1��50      $��2��6!   �� 52   
   -   5         ��Name$ TB_BrideName��53   S    $   TextBoxAndPasswordUnderlineBrush.  (MaterialDesignThemes.Wpf.TextFieldAssist   UnderlineBrush# �   52      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$  ��6x   $��Center=�6�   $��Stretch=�6�   $��20(�6�    �� 	MaxLength$ 50��6�   $
��auto��53      $��1��6!   $��1��6.   $��40,39,40,18q�6>    � Cursor$	 Pen��54        TextBorderBrush#���  6      TextCaretBrush �� 
CaretBrush# �  6P     TextBoxSelectionBrush �� SelectionBrush# �  6~     TextForeColors#���  6�   $��2��6�   6�   '  !MaterialDesignThemes.Wpf.PackIcon  55   
      Kind$ Woman=�6"   $��Center=�6/   $��30��6J   $��30��6U   $��
15,45,0,20q�6a   $��1��6u   $��1��6�    K� ZIndex$ 9��6�     SpecialIconBackGround#���  6�   6�   �� 57   
   -   5        $ LB_Groom��57      $��1��6   $��3��60   $��2��6@      LB_Groom#��Z 6T   $��Stretch=�6�   $��
40,9,40,48q�6�   $
��auto��6�   $��Center=�6�   $��Center=�6�   $��Center=�6  �� 5:   
   -   5        $ TB_GroomName��5;   g   # �   5:      $  ��6x   $��Center=�6�   $��Stretch=�6�   $��20(�6�   $
��auto��5;      $��1��6!   $��3��6.   $��2��6>   $��40,39,40,18q�6R   $ 50��6}   $	 Pen��5<      #���  6    # �  6P   # �  6~   #���  6�   $ 1��6�   6�     5=   
   $	 Man=�6"   $��Center=�6-   $��30��6H   $��30��6S   $��
15,45,0,20q�6_   $��1��6t   $��3��6�   $ 9��6�   #���  6�   6�   �� 5@   
   -   5        $ 	LB_Budget��5@      $��3��6   $��2��61   $��2��6A      	LB_Budget#��Z 6U   $��Stretch=�6�   $��
40,8,40,49q�6�   $
��auto��6�   $��Center=�6�   $��Center=�6�   $��Center=�6  �� 5C   
   -	   5        $ 	TB_Budget��5D   g   # �   5C      $  ��6x   $��Center=�6�   $��Stretch=�6�   $��20(�6�   $
��auto��5D      $��3��6!   $��2��6.   $��2��6>   $��40,39,40,18q�6R   $��Center=�6z   $��Center=�6�   $	 Pen��5E      #���  6    # �  6P   # �  6~   #���  6�   $ 3��6  60    5F   
   $ 	WalletAdd=�6"   $��Center=�63   $��Right=�6N   $��30��6j   $��30��6u   $��	0,45,5,20q�6�   $��3��6�   $��3��6�   $ 9��6�   #���  6�   6�   �� 5H   
   -
   5        $ LB_WeddingDay��5H      $��2��6   $��2��65   $��2��6E      LB_WeddingDay#��Z 6Y   $��Stretch=�6�   $��
40,8,40,49q�6�   $
��auto��6�   $��Center=�6�   $��Center=�6�   $��Center=�6  (  "System.Windows.Controls.DatePicker  5M   
   -   5          Name$ DateP��5M      $��20(�6$   $
��auto��5N      $��2��6#   $��2��60   $��2��6@      FirstDayOfWeek$ Monday=�6T   $
 Hand��6l   $��
40,0,15,24q�6z   $��Bottom=�6�   $��Center=�6�   $��Center=�6�      SelectedDateFormat$ Short=�6�   |�.  $ 2��5O   '   $��0q�6^     ButtonBorderBrush#���  6r   #���  6�   ��6�   Z�  5R      :  4System.Windows.Controls.Primitives.DatePickerTextBox �� 
TargetType �� Property �� Value �� 
IsReadOnly �� Path �� StringFormat  �� RelativeSource! �� AncestorType��%�  '        �� 5S      "  6   .�5T      ��  � �� 5U      �� 5V      .R�5W   "   �� $ PART_TextBox��6�   # �   6+    . $  ��5Y   )   $��Center=�6K   $��Stretch=�6f   $��20(�6�   $��Center=�6�   $��Center=�6�   $
��auto��5Z   )   $	 Pen��5[   )   #���  66   # �  6f   # �  6�   #���  6�   ��5W   �   �� $ SelectedDate�$ dd MMM yyyy��  �� "!  ��5[   �   �� *ActualWidth+  �� *FindAncestor+"!  5]      5_      5`      5b      5d      �� 5f   
   " �� Orientation$" Vertical=�6   $��4��6,   $��2��69   $��2��6I   $ 4��6]   .+�5g      �� $" 
Horizontal=�6   $��Center=�62   .+�5h      �� -   5        # ��Name$# BT_BrowsePicture��5h   J   $��4��6   $��2��6&   $��2��66   $   BT_BrowsePicture#��Z$ 6d   $��Center=�6�   $
 Hand��6�   $ 4��6�   
������ +  %MaterialDesignThemes.Wpf.ButtonAssist%   CornerRadius$% 20��5i      $	��200��6C   $��40��6O   $��
UltraBlack"�6[   $��40,10,10,10q�6s   $��Center=�5j      $��Center=�67   $��20(�6Y   $��Center=�6g   #���  5k      #���  6E   6s     5l      -   5        &  Name$& Tooltip_ImageReset��5l   *   $ DeleteCircleOutline=�6F   $��Center=�6a   $��Right=�6|   $��30��6�   $��30��6�   #���  6�   '   Tooltip_ImageReset( � ToolTip#( Z' 5m   *   $
 Hand��6�   $��20(�6�   6�   5n      �� 5q      -   5        $# BT_Save��5q   F   $��4��6   $��2��6"   $��2��62   )   BT_Save#��Z) 6W   $��Center=�6�   $
 Hand��6�   $ 5��6�   
������ $% 20��5r      $	��200��6C   $��40��6O   $��
UltraBlack"�6[   $��10q�6s   $��Center=�5s      $��Center=�67   $��20(�6Y   $��Center=�6g   #���  5t      #���  6E   6s   5u      5v      5w      