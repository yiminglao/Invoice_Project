   M S B A M L   `   `   `  ����J  FInvoice_Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:CS3280_Group5_ProjectCS3280_Group5_Project  5   	   &     CS3280_Group5_Project.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:CS3280_Group5_Project     5   	     � Title$  CS 3280 Group 5 Project��5   	   $	��515��6)   $	��875��66      InvoiceWindow#��[  6B    � WindowStartupLocation$ CenterScreen=�6i   .��5	      � .+�5
   
   �� -   5         ��Name$ btnAddInvoice��5
      $��Add Invoice��6(   $
��Left=�6>   $��	24,11,0,0q�6Y   $	��Top=�6l   $��32��6�   $��14(�6�   $��96��6�   $
��Bold"�6�     InvoiceButton#���  6�   6�   �� 5   
   -   5        $ btnDelInvoice��5      $��Delete Invoice��6(   $
��Left=�6A   $��
125,11,0,0q�6\   $	��Top=�6p   $	��108��6�   $��32��6�   $��14(�6�   $
��Bold"�6�   #���  6�   6�   �� 5   
   -   5        $ btnEditInvoice��5      $��Edit Invoice��6)   $
��Left=�6@   $��
238,11,0,0q�6[   $	��Top=�6o   $��98��6�   $��32��6�   $��14(�6�   $
��Bold"�6�   #���  6�   6�   �� 5   
   -   5        $ btnSearchInvoice��5      $��Search for Invoice��6+   $
��Left=�6H   $��
341,11,0,0q�6c   $	��Top=�6w   $	��138��6�   $��32��6�   $��14(�6�   $
��Bold"�6�   #���  6�   6�   �� 5   
   -   5        $ btnUpdateItem��5      $��	Item Defs��6(   $
��Left=�6<   $��
484,11,0,0q�6W   $	��Top=�6k   $��32��6�   $��14(�6�   $	��109��6�   $
��Bold"�6�   #���  6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dgItem��5      $
��Left=�6#      
IsReadOnly .   InvoiceDataGrid#��[ 6P   $	��417��6y   $��	24,51,0,0q�6�   $	��Top=�6�   $	��569��6�      AutoGenerateColumns .     Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ 	Item Code��6C      Width5  /System.Windows.Controls.DataGridLengthConverter$ 1* 6V   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 6%   �� *
ItemCode+6V     5      $
 Cost��6U   $ 1* 6c   	 6%   �� *Cost+
 �� StringFormat$
 $ 0.00��6c     5      $ 	Item Desc��6C   $ 4* 6V   	 6%   �� *
ItemDesc+6V   5      �� 5   
   -   5         ��Name$ 	gbInvoice��5      $��Invoice��6&   $��598,10,9.8,289.6q�67   .��5      �  � ColumnDefinitions 5      �� 5      $��1* �6'   �� 5      $��1* �6'    � RowDefinitions 5      �� 5      $��1* �6$   �� 5      $��1* �6$   �� 5      $��1* �6$   �� 5       $��1* �6$   �� 5!      $
��1.2* �6$   .+�5#      �� -   5         ��Name$ 
lblInvoice��5#      $��Invoice Number:��6,   $��0��6F   �� 5$      -	   5        $ lblInvoiceNum��5$       �� ContentStringFormat$ 	Inv- 0000��60   $��1��6P   �� 5%      -
   5        $ lblInvoiceDate��5%      $��Invoice Date:��60   $��2��6H   (  "System.Windows.Controls.DatePicker  5&      -   5          Name$ dpInvoiceDate��5&      $��3��64   $��2��6A   �� 5'      -   5        $ btnSaveInvoice��5'      $��Save Invoice��61   $��4��6H   $��5q�6r   �� 5(      -   5        $ btnCancelInvoice��5(      $��Cancel Invoice��63   $��4��6L   $��1��6Y   $��5q�6j   6u   5)      5*      �� 5,   
   -   5        $ gbItem��5,      $��Add Item��6#   $
��Left=�65   $	��268��6P   $��598,201,0,0q�6]   $	��Top=�6r   $	��262��6�   .��5-      � $��0,10,-0.2,-0.2q�6    5.      �� 5/      �� 50      $��124.8 �6'    52      �� 53      $��1* �6$   �� 54      $��1* �6$   �� 55      $��1* �6$   �� 56      $��1* �6$   �� 57      $��1* �6$   �� 58      $
��1.2* �6$   .+�5;      �� -   5        $ lblSelectItem��5;      $��Select Item��6/   $��0��6E   �� 5<      -   5         ��Name$ cbItem��5<      $��1��6+   $��2��68   6L   �� 5>      -   5        $ lblItemPrice��5>      $��Item Price: ��6.   $��2��6E   $��Center=�6R   �� 5?      -   5        $ lblMoney��5?      $��2��6*   $��1��67   $ $ 0.00��6G   $��Center=�6d   �� 5A      -   5        $ lblTotal��5A      $��Total:��6*   $��3��6;   $��Center=�6I   �� 5B      -   5        $ lblPriceNum��5B      $ $ 0.00��6-   $��2��6J   $��1��6W   $
��Left=�6g   $��57.2,37.2,0,37.4q�6�   $��3��6�   �� 5D      -   5        $ 
btnAddItem��5D      $��Add Item��6-   $��4q�6A   $��4��6L   #���  6Y   6�   �� 5E      -   5        $ 
btnDelItem��5E      $��Delete Item��6-   $��4q�6C   $��4��6N   $��1��6[   #���  6k   6�   �� 5F      -   5        $ btnSaveItem��5F      $
��Save��6.   $��4q�6=   $��5��6H   $��2��6U     InvoiceCancelButton#���  6i   6�   5H      5I      5L      5M      