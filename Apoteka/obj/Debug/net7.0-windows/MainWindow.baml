   M S B A M L   `   `   `  ����B  >Apoteka, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:ApotekaApoteka  5   	       Apoteka.MainWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:Apoteka     5   	     � Title$  
MainWindow��5   	   $	��300��6   $	��900��6)   .��5	      � .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ dataGridCentralni��5      $��Center=�6.   $��Center=�6K   $
��auto��6f   $
��auto��6t      
IsReadOnly$
 True��6�      AlternatingRowBackground
 ��  �   CanUserAddRows$ False��5   3      SelectionUnit$ FullRow=�6J      SelectionMode$ Single=�6b      AutoGenerateColumns$
 True��6y   �� 5   
   $��Center=�6   $	��Top=�62   $��30��6J   $	��800��6V    �� Orientation$ 
Horizontal=�6b   .+�5      �� -   5        	 ��Name$	 btnZaposleni��5      $��	Zaposleni��6+   $	��100��6?   6K   �� 5      -   5        $	 btnKupac��5      $��Kupci��6'   $	��100��67   6C   �� 5      -   5        $	 btnTekuciRacun��5      $��TekuciRacun��6-   $	��100��6C   6O   �� 5      -   5        $	 btnProizvod��5      $��	Proizvodi��6*   $	��100��6>   6J   �� 5      -   5        $	 btnDobavljac��5      $��
Dobavljaci��6+   $	��100��6@   6L   �� 5      -   5        $	 btnRacun��5      $��Racun��6'   $	��100��67   6C   �� 5      -   5        $	 btnSastojak��5      $��Sastojak��6*   $	��100��6=   6I   �� 5      -	   5        $	 btnSastojakProizvod��5      $��SastojakProizvod��62   $	��100��6M   6Y   5      �� 5   
   $��Center=�6   $��50��62   $��Bottom=�6?   $	��450��6Z   $ 
Horizontal=�6f   .+�5      �� -
   5        $	 btnDodaj��5      $��Dodaj��6'   $	��150��67   
 J� RenderTransformOrigin$
 0.5,0.5&�6Z   �� 5      -   5        $	 	btnIzmeni��5      $��Izmeni��6(   $	��150��69   6E   �� 5      -   5        $	 	btnObrisi��5      $��Obrisi��6(   $	��150��69   6E   5      5"      5#      