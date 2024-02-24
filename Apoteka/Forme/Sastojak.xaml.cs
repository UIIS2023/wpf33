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
    /// Interaction logic for Sastojak.xaml
    /// </summary>
    public partial class Sastojak : Window
    {

        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool azuriraj;
        private DataRowView? red;

        public Sastojak()
        {
            InitializeComponent();
            txtSastojak.Focus();
            konekcija = kon.KreirajKonekciju();
        }
        public Sastojak(bool azuriraj, DataRowView red)
        {
            InitializeComponent();
            txtSastojak.Focus();
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



                cmd.Parameters.Add("@Ime", SqlDbType.NVarChar).Value = txtSastojak.Text;

                if (azuriraj)
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"UPDATE tblSastojak SET Ime=@Ime
                                       WHERE SastojakID=@id";
                    red = null;
                }
                else
                {
                    cmd.CommandText = @"insert into tblSastojak (Ime)
                    VALUES(@Ime)";
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

