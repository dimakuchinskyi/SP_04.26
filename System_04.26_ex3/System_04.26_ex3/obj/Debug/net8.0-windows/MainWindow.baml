   M S B A M L   `   `   `  ����  System_04.26_ex3"    System_04._26_ex3.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$/  )Копіювання директорій��5   	   $	��300��6'   $	��500��64   .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5	      $
��Auto �6   �� 5
      $��* �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   � $��0��6   $��0��6   $��5q�6,   .+�5      �� -   5         ��Name$ SourcePathTextBox��5      �� 5      $=��7Шлях до початкової директорії��6    J� IsHitTestVisible$ False��5      
�������$��Center=�5      $��5,0,0,0q�5      y�5      �� *Text+ �� ElementName$ SourcePathTextBox��   StringToVisibilityConverter �� 	Converter# [  5      �� 5   
   -   5        $��Обрати��5      $��0��6"   $��1��6/   $��5q�6?   6J   � 5   
   $��1��6   $��0��6   $��5q�6,   .+�5      �� -   5        $ DestinationPathTextBox��5      �� 5      $*��$Шлях для копіювання��6   $ False��5       
�������$��Center=�5"      $��5,0,0,0q�5#      y�5$      �� *Text+$ DestinationPathTextBox��# [  5%      �� 5&   
   -   5        $��Обрати��5&      $��1��6"   $��1��6/   $��5q�6?   6J   �� 5(   
   $��2��6   $��0��6"   $��2��62    �� Orientation$ 
Horizontal=�6F   $��5q�6_   .+�5)      �� $(��"Кількість потоків:��6   $��Center=�62   $��0,0,5,0q�6M   �� 5*      -   5        $ ThreadCountTextBox��5*      $��50��62   $��Center=�6=   5+      � 5-   
   -   5         �Name$ CopyProgressBar��5-      $��3��6/   $��0��6<   $��2��6L   $��25��6`   $��5q�6l   �� 5/   
   -   5        $��
Старт��5/      $��4��6!   $��0��6.   $��2��6>   $��30��6R   $��5q�6^   6i   50      51      