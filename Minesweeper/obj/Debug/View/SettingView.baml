   M S B A M L   `   `   `  ����F  BMinesweeper, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Minesweeper.ViewMinesweeper.View  5      =#clr-namespace:Minesweeper.ViewModelMinesweeper.ViewModel  5      "    Minesweeper.View.SettingView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:Minesweeper.View     5      2vm#clr-namespace:Minesweeper.ViewModel     5      
�������  � DataContext  5      ,   &Minesweeper.ViewModel.ViewModelLocator  5      .��5      �   6   �� *	Setting+ � ColumnDefinitions 5   
   �� 5      �� 5       � RowDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      .+�5   
   �� $"��Кол-во клеточек��6   $��Center=�5      $��Center=�5      
�������
���   �$��1q�5      �� 5   
   $��1��6   $��Center=�5!      $��Center=�5"      
�������
���   �$��1q�5%      ��5       �� *CountOfCells+5%      �� 5&   
   $��1��6   $'��!Уровень сложности��5'      $��Center=�5(      $��Center=�5)      
���   �$��1q�5+      �� 5,   
   -   5         ��Name$ DifficulComboBox��52      $��1��5,      $��1��5-      $��Center=�50      $��Center=�51      
�������
���   �$��1q�56      ��5.      �� *DifficultCollection+ �� Mode$ OneWay=� �� UpdateSourceTrigger$ PropertyChanged=� �� SelectedItem 5/      �� *	Difficult+56      �� 59   
   $��2��6   $��Рекорд��5:      $��Center=�5;      $��Center=�5<      
���   �$��1q�5>      �� 5?   
   $��2��6   $��2��5@      $��Center=�5B      $��Center=�5C      
���   �$��1q�5E      ��5A      �� *
userRecord+$ OneWay=�$ PropertyChanged=�5E      �� 5F   
   $��3��6   $+��%Сохранить изменения��5G      $��Center=�5H      $��Center=�5I      
�������
���   �$��1q�5L      ��5M      �� *ChangeSettings+$ OneWay=�$ PropertyChanged=��� 5N   
   $��2��6   $��3��5O      $0��*Настройки по умолчанию��5P      $��Center=�5Q      $��Center=�5R      
�������
���   �$��1q�5U      ��5V      �� *ResetSettings+$ OneWay=�$ PropertyChanged=�5W      5X      