   M S B A M L   `   `   `  ����I  EEskuvo_tervezo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Eskuvo_tervezo.PagesEskuvo_tervezo.Pages  5      E'clr-namespace:Eskuvo_tervezo.PropertiesEskuvo_tervezo.Properties  5      (    "Eskuvo_tervezo.Pages.CalendarItems   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:Eskuvo_tervezo.Pages     5      >
properties'clr-namespace:Eskuvo_tervezo.Properties     5      b ^MaterialDesignThemes.Wpf, Version=3.2.0.1979, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5        N� 
Foreground
  ����� N� 
FontFamily$ Comic Sans MS)�5       N� Title$ CalendarItems��5       � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $D >pack://application:,,,/ResourceDictionary/LabelDictionary.xaml?�6%   6o   %    5      .��5      �  � ColumnDefinitions 5   
   �� 5      $��1* �6    � RowDefinitions 5   
   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5       $��1* �6   �� 5!      $��1* �6   �� 5"      $��1* �6   �� 5#      $��1* �6   .+�5&   
   �� -   5         ��Name$ LB_Diary��5&      $��0��6   $��0��60   $��3��6@   $	��Top=�6T   $��10q�6l   $��Center=�6x   *   $Eskuvo_tervezo.Properties.ResourceHu	   
LB_Contact#��Z	 5'         TextForeColors#���   6G   $��34(�6u   �� 5(   
   -   5        $ LB_Date��5(      $��0��6   $��0��6/   $��3��6?   $��Bottom=�6S   $��10q�6n   $��Center=�6z   
   LB_Diary#��Z
 5)      #���   6E   $��30(�6s   �� 5*   
   -   5        $ LB_Entry��5*      $��1��6      LB_Entry#��Z 60   $��Stretch=�6d   $��40,0,160,40q�6�   $
��auto��6�   $��Center=�6�   $��Center=�6�   $��Center=�6�   #���   5+      �� 5-   
   -   5         ��Name$ TB_LogEntry��5.   C    $  TextBoxAndPasswordUnderlineBrush.  (MaterialDesignThemes.Wpf.TextFieldAssist   UnderlineBrush# �  5-      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$  ��6x   $��Center=�6�   $��Stretch=�6�   $��20(�6�   $
��auto��5.      $��1��6!   $��40,20,160,0q�6.    � Cursor$	 Pen��5/        TextBorderBrush#���  6      TextCaretBrush �� 
CaretBrush# �  6P     TextBoxSelectionBrush �� SelectionBrush# �  6~   #���   6�    K� ZIndex$ 1��6  '  !MaterialDesignThemes.Wpf.PackIcon  50   
      Kind$ BookAdd=�6"   $��Center=�61   $��	10,25,0,0q�6L   $��30��6_   $��30��6j   $��1��6w     SpecialIconBackGround#���  6�   $��20(�6�   6�   �� 51   
   -   5         ��Name$ BT_Save��51      $��1��6      BT_Save#��Z 6/   $��Right=�6c   $
 Hand��6   $ 2��6�   +  %MaterialDesignThemes.Wpf.ButtonAssist   CornerRadius$ 20��52      $��
20,25,20,0q�6C   $	��120��6W   $��40��6c   $��
UltraBlack"�6o   $��Center=�53      $��Center=�67   $��20(�6Y   $��Center=�6g     ButtonBorderBrush#���  54        ButtonBackGround#���  6G   #���   6w   6�   �� 55   
   $��Stretch=�6   $��Disabled=�65   $
��Auto=�6^   $��2��6�   $��0��6�   $��6��6�   $��20q�6�     ScrollViewerBorderBrush#���  6�   $��2q�6�   $ 3��6  .��56      �� -   5         ��Name$ Items��56      $��1��6(   $��10��65   $��2��6G   $��Stretch=�57      $��Stretch=�67   58      59      5:      5;      