   M S B A M L   `   `   `  ����B  >Apoteka, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Apoteka.FormeApoteka.Forme  5   	       Apoteka.Forme.Proizvod   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:Apoteka.Forme     5   	     � Title$  Proizvod��5   	   $	��450��6   $	��400��6'   .��5	      � .+�5
   
   �� $��Naziv: ��6   $
��Left=�6"   $��	20,18,0,0q�6=   $	��Top=�6P   $	��100��6h   �� 5   
   $��Proizvodjac:��6   $
��Left=�6'   $��	20,57,0,0q�6B   $	��Top=�6U   $	��100��6m   �� 5   
   $��Rok Trajanja:��6   $
��Left=�6(   $��	20,96,0,0q�6C   $	��Top=�6V   $	��100��6n   �� 5   
   -   5         ��Name$ txtNaziv��5      $
��Left=�6$   $��
120,22,0,0q�6?    �� TextWrapping$
 Wrap=�6S   $	��Top=�6g   $	��120��6   �� 5   
   -   5        $ txtProizvodjac��5      $
��Left=�6*   $��
120,61,0,0q�6E   $
 Wrap=�6Y   $	��Top=�6m   $	��120��6�   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ dpRokTrajanja��5      $
��Left=�6,   $��
120,97,0,0q�6G   $	��Top=�6[   $	��120��6s   �� 5   
   -   5         ��Name$ 
btnSacuvaj��5      $��Sacuvaj��6%   $
��Left=�67   $��
10,385,0,0q�6R   $	��Top=�6f    J� RenderTransformOrigin$ -0.005,0.603&�6~   $��35��6�   $��69��6�   6�   �� 5   
   -   5        $ 	btnOtkazi��5      $��Otkazi��6$   $
��Left=�65   $��321,385,0,0q�6P   $	��Top=�6e   $��35��6}   $��69��6�   6�   �� 5   
   $��Cena:��6   $
��Left=�6    $��
20,136,0,0q�6;   $	��Top=�6O   $	��100��6g   �� 5   
   $��Opis:��6   $
��Left=�6    $��
20,174,0,0q�6;   $	��Top=�6O   $	��100��6g   �� 5   
   -   5        $ txtCena��5      $
��Left=�6#   $��120,140,0,0q�6>   $	��Top=�6S   $	��120��6k   �� 5   
   -   5        $ txtOpis��5      $
��Left=�6#   $��120,178,0,0q�6>   $
 Wrap=�6S   $	��Top=�6g   $	��259��6   $	��120��6�   �� 5   
   $��
Dobavljac:��6   $
��Left=�6%   $��
20,325,0,0q�6@   $	��Top=�6T   �� 5   
   -   5         ��Name$ cbDobavljac��5      $
��Left=�6(   $��120,327,0,0q�6C   $	��Top=�6X   $	��120��6p    �� DisplayMemberPath$ NazivDobavljaca��5       �� SelectedValuePath$ DobavljacID��6/   5      5      