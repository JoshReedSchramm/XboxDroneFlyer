   M S B A M L   `   `   `  ����O  KJ2i.Net.XinputClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:J2i.Net.XinputClient.ConvertersJ2i.Net.XinputClient.Converters  5   	   %    J2i.Net.XinputClient.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   D
converters-clr-namespace:J2i.Net.XinputClient.Converters     5   	     � Title$  
MainWindow��5   	   $	��400��6   $	��379��6)   $	��400��5   
   $	��390��6   $	��400��5   
   $	��390��6    � 	Resources 5
      I  CJ2i.Net.XinputClient.Converters.BatteryTypeValueToBatteryTypeString '  #BatteryTypeValueToBatteryTypeString��%   &	          5   
   69   5      .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��	2,10,2,10q�6.   .+�5      �� $��5q�6   Controller Index:66   �� 5      -   5        $��50��5      .@�5      ��  �� 
IsSelected . .��61   064   �� 5      .��6   16"   �� 5      .��6   26"   �� 5      .��6   36"   5      5      � 5      $	��300��6   $	��350��6   $��1��6%    � ColumnDefinitions 5   
   �� 5      $
��auto �6   �� 5       $
��auto �6   �� 5!      $��* �6    5#   
   �� 5$      $
��auto �6   $	��200��6*   �� 5%      $
��auto �6    �� 5&      $
��auto �6    �� 5'      $��* �6    .+�5*   
   �  5+      �� 5,      $��* �6#   �� 5-      $��* �6#    5/      �� 50      $
��auto �6    �� 51      $
��auto �6    �� 52      $
��auto �6    �� 53      $
��auto �6    �� 54      $
��auto �6    �� 55      $
��auto �6    �� 56      $
��auto �6    �� 57      $
��auto �6    �� 58      $
��auto �6    .+�5<      �� -   5         ��Name$ CheckboxAButton��5<      $��0��60    d� 	IsChecked 6=   �� *SelectedController.IsAPressed+ �� Mode$ OneWay=��� 5=      $��A Button��6   $��0��6(   $��1��65   �� 5?      -   5        $ CheckboxBButton��5?      $��1��61    6>   �� *SelectedController.IsBPressed+$ OneWay=��� 5@      $��B Button��6   $��1��6(   $��1��65   �� 5B      -   5        $ CheckboxXButton��5B      $��2��61    6?   �� *SelectedController.IsXPressed+$ OneWay=��� 5C      $��X Button��6   $��2��6(   $��1��65   �� 5E      -   5        $ CheckboxYButton��5E      $��3��61    6?   �� *SelectedController.IsYPressed+$ OneWay=��� 5F      $��Y Button��6   $��3��6(   $��1��65   �� 5I      -   5        $  CheckboxLeftShoulderButton��5I      $��4��6<    6J   �� **(SelectedController.IsLeftShoulderPressed+$ OneWay=��� 5J      $��Left Shoulder Button��6   $��4��64   $��1��6A   �� 5L      -   5        $! CheckboxRightShoulderButton��5L      $��5��6=    6K   �� *+)SelectedController.IsRightShoulderPressed+$ OneWay=��� 5M      $��Right Shoulder Button��6   $��5��65   $��1��6B   �� 5O      -   5        $ CheckboxLeftStickButton��5O      $��6��69    6G   �� *'%SelectedController.IsLeftStickPressed+$ OneWay=��� 5P      $��Left Stick Button��6   $��6��61   $��1��6>   �� 5S      -	   5        $ CheckboxRightStickButton��5S      $��7��6:    6I   �� *(&SelectedController.IsRightStickPressed+$ OneWay=��� 5T      $��Right Stick Button��6   $��7��62   $��1��6?   � 5W      $��50��6   $��50��6   $��8��6*   $��2��68   .+�5X      �� -
   5        $ CheckboxDPadUpButton��5X      $��Center=�6:   $	��Top=�6W    6p   �� *$"SelectedController.IsDPadUpPressed+$ OneWay=��� 5Y      -   5        $ CheckboxDPadDownButton��5Y      $��Center=�6=   $��Bottom=�6Z    6v   �� *&$SelectedController.IsDPadDownPressed+$ OneWay=��� 5Z      -   5        $ CheckboxDPadLeftButton��5Z      $
��Left=�6<   $��Center=�6W    6s   �� *&$SelectedController.IsDPadLeftPressed+$ OneWay=��� 5[      -   5        $ CheckboxDPadRightButton��5[      $��Right=�6=   $��Center=�6Y    6u   �� *'%SelectedController.IsDPadRightPressed+$ OneWay=�5\      5^      � 5`   
   $��1��6   $	��200��6    5a      �� 5b      $��* �6    �� 5c      $��* �6    �� 5d      $��* �6    �� 5e      $��* �6    �� 5f      $��* �6    �� 5g      $��* �6    �� 5h      $��* �6    �� 5i      $��* �6    �� 5j      $��* �6    �� 5k      $��* �6    �� 5l      $��* �6    �� 5m      $��* �6    .+�5p      �� $��Left X Axis ��6   $��0��6,   � 5q      $��1��6   	 � Minimum$	 -32768Q�6'   
 � Maximum$
 32767Q�68    � Value 6I   �� *%#SelectedController.LeftThumbStick.X+$ OneWay=��� 5s      $��Left Y Axis��6   $��2��6+   � 5t      $��3��6   $	 -32768Q�6(   $
 32767Q�69    6J   �� *%#SelectedController.LeftThumbStick.Y+$ OneWay=��� 5v      $��Right X Axis��6   $��4��6,   � 5w      $��5��6   $	 -32768Q�6(   $
 32767Q�69    6J   �� *&$SelectedController.RightThumbStick.X+$ OneWay=��� 5y      $��Right Y Axis��6   $��6��6,   � 5z      $��7��6   $	 -32768Q�6'   $
 32767Q�68    6I   �� *&$SelectedController.RightThumbStick.Y+$ OneWay=��� 5|      $��Left Trigger��6   $��8��6,   � 5}      $��9��6   $	 0Q�6(   $	
 255Q�64    6D   �� * SelectedController.LeftTrigger+$ OneWay=��� 5      $��Right Trigger��6   $��10��6-   � 5�      $��11��6   $	 0Q�6)   $	
 255Q�65    6C   �� *!SelectedController.RightTrigger+$ OneWay=�5�      � 5�      $��2��6   $��4��6     5�      �� 5�      $
��auto �6$   �� 5�      $
��auto �6$    5�      �� 5�      $��* �6'   �� 5�      $��* �6'   �� 5�      $��* �6'   �� 5�      $��* �6'   .+�5�      �� $��1��6   Vibration Motors6;   �� 5�      $��1��6   
Left Motor68   �� 5�      $��2��6   Right Motor69   �� 5�      -   5        $��3��5�      $��1��6)   $
��Send��66   6E   �� 5�      -   5         ��Name$ LeftMotorSpeed��5�   v   $��2q�6   $��1��6$   $��1��65   $��Center=�6B   $	 0Q�6]   $
 1Q�6i   6v   �� 5�      -   5        $ RightMotorSpeed��5�   u   $��2q�6   $��2��6$   $��1��65   $��Center=�6B   $	 0Q�6]   $
 1Q�6i   6u   5�      � 5�      $��3��6   $��4��6     5�      �� 5�      $
��auto �6'   �� 5�      $��* �6'   .+�5�      �� $��Battery Info:��6   �� 5�      $��1��6   ��6-   �� *.,SelectedController.BatteryInformationGamepad+5�      5�      5�      5�      