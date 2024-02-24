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
    /// Interaction logic for Kupac.xaml
    /// </summary>
    public partial class Kupac : Window
    {

        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool azuriraj;
        private DataRowView? red;


        public Kupac()
        {
            InitializeComponent();
            txtIme.Focus();
            konekcija = kon.KreirajKonekciju();
        }
        public Kupac(bool azuriraj, DataRowView red)
        {
            InitializeComponent();
            txtIme.Focus();
            konekcija = kon.KreirajKonekciju();
            this.azuriraj = azuriraj;
            this.red = red;
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                konekcija.Open();

                DateTime? selectedDate = dpDatumRodjenja.SelectedDate;

                if (dpDatumRodjenja.SelectedDate != null)
                {

                    DateTime date = (DateTime)dpDatumRodjenja.SelectedDate;
                    string datum = date.ToString("yyyy-MM-dd");

                    SqlCommand cmd = new SqlCommand
                    {
                        Connection = konekcija
                    };
                    cmd.Parameters.Add("@ImeKupca", SqlDbType.NVarChar).Value = txtIme.Text;
                    cmd.Parameters.Add("@PrezimeKupca", SqlDbType.NVarChar).Value = txtPrezime.Text;
                    cmd.Parameters.Add("@DatumRodjenjaKupca", SqlDbType.DateTime).Value = datum;
                    cmd.Parameters.Add("@Pol", SqlDbType.NVarChar).Value = txtPol.Text;


                    if(azuriraj)
                    {
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                        cmd.CommandText = @"UPDATE tblKupac SET ImeKupca=@ImeKupca, PrezimeKupca=@PrezimeKupca,
                            DatumRodjenjaKupca=@DatumRodjenjaKupca, Pol=@Pol
                                       WHERE KupacID=@id";
                        red = null;
                    }
                    else
                    {
                        cmd.CommandText = @"insert into tblKupac (ImeKupca, PrezimeKupca, DatumRodjenjaKupca, Pol)
                                        VALUES(@ImeKupca, @PrezimeKupca, @DatumRodjenjaKupca, @Pol)";
                    }


                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Morate odabrati datum rođenja.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Error);
                }


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
