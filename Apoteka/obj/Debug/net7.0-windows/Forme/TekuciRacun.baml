   M S B A M L   `   `   `  ����B  >Apoteka, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Apoteka.FormeApoteka.Forme  5   	       Apoteka.Forme.TekuciRacun   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:Apoteka.Forme     5   	     � Title$  TekuciRacun��5   	   $	��200��6   $	��300��6*   .��5	      � .+�5   
   �� $��Stanje racuna:��6   $
��Left=�6)   $��
10,101,0,0q�6D   $	��Top=�6X   $	��110��6p   �� 5   
   -   5         ��Name$ 
btnSacuvaj��5      $��Sacuvaj��6%   $
��Left=�67   $��
10,139,0,0q�6R   $	��Top=�6f    J� RenderTransformOrigin$ -0.005,0.603&�6~   $��35��6�   $��69��6�   6�   �� 5   
   -   5        $ 	btnOtkazi��5      $��Otkazi��6$   $
��Left=�65   $��221,139,0,0q�6P   $	��Top=�6e   $��35��6}   $��69��6�   6�   �� 5   
   -   5         ��Name$ 	txtStanje��5      $
��Left=�6%   $��130,105,0,0q�6@    �� TextWrapping$
 Wrap=�6U   $	��Top=�6i   $	��150��6�   �� 5   
   -   5        $ txtImePostojecegKupca��5       �� 
IsReadOnly$
 True��62   $
��Left=�6D   $��
130,54,0,0q�6_   $
 Wrap=�6s   $	��Top=�6�   $	��150��6�   �� 5   
   -   5        $ txtPrezimePostojecegKupca��5      $
 True��65   $
��Left=�6G   $��
130,74,0,0q�6b   $
 Wrap=�6v   $	��Top=�6�   $	��150��6�   �� 5   
   $��Kupac :��6   $
��Left=�6"   $��	10,20,0,0q�6=   $	��Top=�6P   $	��110��6h   �� 5   
   -   5         ��Name$ cbKupac��5      $
��Left=�6$   $��
130,23,0,0q�6?   $	��Top=�6S   $	��150��6k    �� DisplayMemberPath$ Kupac��5       �� SelectedValuePath$ KupacID��6%   6A   5      5      