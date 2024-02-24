using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Apoteka.Forme
{
    /// <summary>
    /// Interaction logic for Zaposleni.xaml
    /// </summary>
    public partial class Zaposleni : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool azuriraj;
        private DataRowView? red;

        public Zaposleni()
        {
            InitializeComponent();
            txtJMBG.Focus();
            konekcija = kon.KreirajKonekciju();
        }
        public Zaposleni(bool azuriraj, DataRowView red)
        {
            InitializeComponent();
            txtJMBG.Focus();
            konekcija = kon.KreirajKonekciju();
            this.azuriraj = azuriraj;
            this.red = red;
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                konekcija.Open();

                SqlCommand cmd = new SqlCommand
                {
                    Connection = konekcija
                };
                cmd.Parameters.Add("@Ime", SqlDbType.NVarChar).Value = txtIme.Text;
                cmd.Parameters.Add("@Prezime", SqlDbType.NVarChar).Value = txtPrezime.Text;
                cmd.Parameters.Add("@Grad", SqlDbType.NVarChar).Value = txtGrad.Text;
                cmd.Parameters.Add("@JMBG", SqlDbType.NVarChar).Value = txtJMBG.Text;
                cmd.Parameters.Add("@Adresa", SqlDbType.NVarChar).Value = txtAdresa.Text;
                cmd.Parameters.Add("@KontaktZaposlenog", SqlDbType.NVarChar).Value = txtKontaktZaposlenog.Text;
                cmd.Parameters.Add("@KorisnickoIme", SqlDbType.NVarChar).Value = txtKorisnickoIme.Text;
                cmd.Parameters.Add("@Lozinka", SqlDbType.NVarChar).Value = txtLozinka.Text;

                if (azuriraj)
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"UPDATE tblZaposleni SET Ime=@Ime,Prezime=@Prezime,Grad=@Grad,JMBG=@JMBG,Adresa=@Adresa,KontaktZaposlenog=@KontaktZaposlenog,KorisnickoIme=@KorisnickoIme,Lozinka=@Lozinka 
                                       WHERE ZaposleniID=@id";
                    red = null;
                }
                else
                {
                    cmd.CommandText = @"insert into tblZaposleni (Ime,Prezime,Grad,JMBG,Adresa,KontaktZaposlenog,KorisnickoIme,Lozinka)
                             VALUES(@Ime,@Prezime,@Grad,@JMBG,@Adresa,@KontaktZaposlenog,@KorisnickoIme,@Lozinka)";
                }


                cmd.ExecuteNonQuery();
                cmd.Dispose();
                this.Close();

            }
            catch (SqlException)
            {
                MessageBox.Show("Unos odredjenih vrednosti nije validan!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            catch (FormatException)
            {
                MessageBox.Show("Unete vrednosti nisu u odredjenom formatu!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                konekcija.Close();
            }
        }

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
