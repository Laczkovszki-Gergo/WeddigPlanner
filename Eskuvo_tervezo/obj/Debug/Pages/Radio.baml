   M S B A M L   `   `   `  ����I  EEskuvo_tervezo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Eskuvo_tervezo.PagesEskuvo_tervezo.Pages  5      E'clr-namespace:Eskuvo_tervezo.PropertiesEskuvo_tervezo.Properties  5      [ WSystem.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089Y@clr-namespace:System.Windows.Forms;assembly=System.Windows.FormsSystem.Windows.Forms 5	           Eskuvo_tervezo.Pages.Radio   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:Eskuvo_tervezo.Pages     5      >
properties'clr-namespace:Eskuvo_tervezo.Properties     5      b ^MaterialDesignThemes.Wpf, Version=3.2.0.1979, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      Iwf@clr-namespace:System.Windows.Forms;assembly=System.Windows.Forms  5	      -   5          N� 
Foreground
  ����� N� 
FontFamily$ Comic Sans MS)�5       N� Title$ Radio��5       � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $G Apack://application:,,,/ResourceDictionary/ComboBoxDictionary.xaml?�6%   6r   �� 5      $D >pack://application:,,,/ResourceDictionary/LabelDictionary.xaml?�6%   6o   %    5      .��5      �  � ColumnDefinitions 5   
   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6    � RowDefinitions 5    
   �� 5!      $��1* �6   �� 5"      $��1* �6   �� 5#      $��1* �6   �� 5$      $��1* �6   �� 5%      $��1* �6   �� 5&      $��1* �6   �� 5'      $��1* �6   �� 5(      $��1* �6   .+�5,   
   �� -   5         ��Name$ LB_Radio��5,      $��0��6   $��0��60   $��4��6@   $	��Top=�6T   $��10q�6l   $��Center=�6x   *   $Eskuvo_tervezo.Properties.ResourceHu	   LB_Radio#��Z	 5-         TextForeColors#���   6E   $��34(�6s   �� 5/   
   -   5        $ LB_StreamLink��5/   B   $��1��6   $��1��6   $��2��6.   
   LB_StreamLink#��Z
 6Y   �� 50   
   -   5         ��Name$ TB_StreamLink��51   f    $  TextBoxAndPasswordUnderlineBrush.  (MaterialDesignThemes.Wpf.TextFieldAssist   UnderlineBrush# �  50      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$  ��6x   $��Center=�6�   $��Stretch=�6�   $��20(�6�   $��Center=�6�   $��Center=�6  $
��auto��51      $��1��6!   $��1��6.   $��2��6>   $��
45,20,45,0q�6R    �� 	MaxLength$	 120��6}    � Cursor$	 Pen��52        TextBorderBrush#���  53        TextCaretBrush �� 
CaretBrush# �  6G     TextBoxSelectionBrush �� SelectionBrush# �  6u   #���   6�   '  !MaterialDesignThemes.Wpf.PackIcon  54   
      Kind$ Radio=�6"   $��30��6/   $��30��6:   $��Center=�6F   $��	10,25,0,0q�6a   $��1��6u   $��1��6�   $��2��6�    K� ZIndex$ 9��6�     SpecialIconBackGround#���  6�   6�   �� 56   
   -   5        $ LB_RadioChannelName��56   B   $��2��6   $��1��6   $��2��6.      LB_RadioChannelName#��Z 6_   �� 57   
   -   5        $ TB_RadioChannelName��58   f   # �  57      $  ��6x   $��Center=�6�   $��Stretch=�6�   $��20(�6�   $��Center=�6�   $��Center=�6  $
��auto��58      $��2��6!   $��1��6.   $��2��6>   $��
45,20,45,0q�6R   $ 60��6�   $	 Pen��59      #���  5:      # �  6G   # �  6u   #���   6�     5;   
   $ MusicCircle=�6"   $��30��65   $��30��6@   $��Center=�6L   $��	10,25,0,0q�6g   $��2��6{   $��1��6�   $��2��6�   $ 9��6�   #���  6�   6�   �� 5>   
   -   5         ��Name$ BT_SaveChannel��5>   C   $��3��6   $��1��6   $��2��6.      BT_Save#��Z 6[   $��Center=�6�   $
 Hand��6�   $ 5��6�   +  %MaterialDesignThemes.Wpf.ButtonAssist   CornerRadius$ 20��5?      $��0q�6C   $	��140��6N   $��40��6Z   $��
UltraBlack"�6f   $��Center=�5@      $��Center=�67   $��20(�6Y   $��Center=�6g     ButtonBorderBrush#���  5A        ButtonBackGround#���  6G   #���   6w   5B      �� 5C   
   -   5        $ LB_RadioList��5C   Z   $��4��6   $��1��6   $��2��6.   $	��Top=�6B      LB_RadioList#��Z 6p   �� 5E   
   -	   5         ��Name$ CB_RadioList��5E      $��4��6)   $��1��66   $��2��6F   $
��auto��6Z   $��40��6g   $��Bottom=�6s   $��10,20q�6�   $��Stretch=�5F      $��20(�66    �� IsDropDownOpen .  5H        5J   
   -
   5          Name$ Tooltip_Delete��5J   "   $ Delete=�6:   $��40��6H   $��40��6S   $��4��6_   $��3��6l   $��	45,0,0,20q�6|   $��20(�6�   $
��Left=�5K   "   $��Bottom=�6=   #���  6X   $
 Hand��5L   i      Tooltip_Delete � ToolTip# Z 6w     5M   
   -   5        $ Tooltip_Modification��5M   "   $	 Pen=�6@   $��40��6K   $��40��6V   $��4��6b   $��3��6o   $��5,0,0,20q�6   $��20(�6�   $
��Left=�5N   #   $��Bottom=�6>   #���  6Y   $
 Hand��5O   o      Tooltip_Modification# Z 6}   ]� 5Q   
   $��1��6   $��5��6$   $��2��61   .+�5R      � .+�5S      �� -   5        $ 	LB_Volume��5S      $	��Top=�6+   $��Stretch=�6C      	LB_Volume#��Z 6a   $��20(�6�     5T      -   5        $ IconDecrease��5T   *   $
��-5,0q�6@   $ VolumeDecrease=�6N   $��40��6d   $��40��6o   $��Center=�6|   #���  6�   $
 Hand��6�   6C    5U      -   5        $ IconIncrease��5U   *   $
��-5,0q�6@   $��Right=�6N   $ VolumeIncrease=�6j   $��40��6�   $��40��6�   $��Center=�6�   #���  6�   $
 Hand��6�   6_  �� 5V      -   5          ��Name$  	ScbVolume��5V      $��Center=�6/   $��Bottom=�6L   $��25��6g   ! �� Orientation$! 
Horizontal=�6s   " � Maximum$	" 100Q�6�   # � Minimum$# 0Q�6�   $ � SmallChange$$ 1Q�6�   $
 Hand��6�   ��5W      �� *ActualWidth+% �� RelativeSource% �� *FindAncestor+& �� AncestorType"& ]�6�   5X      5Y      �� 5[   
   -   5        $ BT_SaveChosenChannel��5[   C   $��6��6   $��1��6   $��2��6.   '   BT_SaveChosenChannel#��Z' 6a   $��Center=�6�   $
 Hand��6�   $ 5��6�   $ 20��5\      $��0q�6C   $	��140��6N   $��40��6Z   $��
UltraBlack"�6f   $��Center=�5]      $��Center=�67   $��20(�6Y   $��Center=�6g   #���  5^      #���  6G   #���   6w   5_      5`      5a      