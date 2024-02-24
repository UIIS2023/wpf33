using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Interaction logic for Dobavljac.xaml
    /// </summary>
    public partial class Dobavljac : Window
    {

        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool azuriraj;
        private DataRowView? red;

        public Dobavljac()
        {
            InitializeComponent();
            txtNaziv.Focus();
            konekcija = kon.KreirajKonekciju();
        }
        public Dobavljac(bool azuriraj, DataRowView red)
        {
            InitializeComponent();
            txtNaziv.Focus();
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
                    cmd.Parameters.Add("@NazivDobavljaca", SqlDbType.NVarChar).Value = txtNaziv.Text;
                    cmd.Parameters.Add("@KontaktDobavljaca", SqlDbType.NVarChar).Value = txtKontakt.Text;



                     if (azuriraj)
                     {
                            cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                            cmd.CommandText = @"UPDATE tblDobavljac SET NazivDobavljaca=@NazivDobavljaca, KontaktDobavljaca=@KontaktDobavljaca
                                       WHERE DobavljacID=@id";
                            red = null;
                     }
                     else
                     {
                             cmd.CommandText = @"insert into tblDobavljac (NazivDobavljaca, KontaktDobavljaca)
                             VALUES(@NazivDobavljaca, @KontaktDobavljaca)";
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
