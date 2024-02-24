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
using System.Diagnostics;

namespace Apoteka.Forme
{
    /// <summary>
    /// Interaction logic for Racun.xaml
    /// </summary>
    public partial class Racun : Window
    {

        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool azuriraj;
        private DataRowView? red;

        public Racun()
        {
            InitializeComponent();
            txtUkupnaCena.Focus();
            konekcija = kon.KreirajKonekciju();
            PopuniPadajuceListe();
        }
        public Racun(bool azuriraj, DataRowView red)
        {
            InitializeComponent();
            txtUkupnaCena.Focus();
            konekcija = kon.KreirajKonekciju();
            PopuniPadajuceListe();
            this.azuriraj = azuriraj;
            this.red = red;
            
        }

        private void PopuniPadajuceListe()
        {
            try
            {
                konekcija.Open();

                string vratiZaposlene = @"SELECT ZaposleniID, Ime + ' ' + Prezime as Zaposleni FROM tblZaposleni";
                SqlDataAdapter daZaposleni = new SqlDataAdapter(vratiZaposlene, konekcija);
                DataTable dtZaposleni = new DataTable();
                daZaposleni.Fill(dtZaposleni);
                cbZaposleni.ItemsSource = dtZaposleni.DefaultView;
                daZaposleni.Dispose();
                dtZaposleni.Dispose();


                string vratiKupce = @"SELECT KupacID, ImeKupca + ' ' + PrezimeKupca as Kupac FROM tblKupac";
                SqlDataAdapter daKupac = new SqlDataAdapter(vratiKupce, konekcija);
                DataTable dtKupac = new DataTable();
                daKupac.Fill(dtKupac);
                cbKupac.ItemsSource = dtKupac.DefaultView;
                daKupac.Dispose();
                dtKupac.Dispose();

                string vratiProizvode = @"SELECT ProizvodID, NazivProizvoda FROM tblProizvod";
                SqlDataAdapter daProizvod = new SqlDataAdapter(vratiProizvode, konekcija);
                DataTable dtProizvod = new DataTable();
                daProizvod.Fill(dtProizvod);
                cbProizvod.ItemsSource = dtProizvod.DefaultView;
                daProizvod.Dispose();
                dtProizvod.Dispose();

            }
            catch (SqlException)
            {
                MessageBox.Show("Niste popunili padajuce liste!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
            }
        }
        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                konekcija.Open();
                DateTime? selectedDate = dpRacun.SelectedDate;
                if (dpRacun.SelectedDate != null)
                {
                    DateTime date = (DateTime)dpRacun.SelectedDate;
                    string datum = date.ToString("yyyy-MM-dd");                 
                    SqlCommand cmd = new SqlCommand
                    {
                        Connection = konekcija
                    };
                    cmd.Parameters.Add("@DatumIzdavanja", SqlDbType.DateTime).Value = datum;
                    cmd.Parameters.Add("@UkupnaCena", SqlDbType.Float).Value = txtUkupnaCena.Text;
                    cmd.Parameters.Add("@ZaposleniID", SqlDbType.Int).Value = cbZaposleni.SelectedValue;
                    cmd.Parameters.Add("@KupacID", SqlDbType.Int).Value = cbKupac.SelectedValue;
                    cmd.Parameters.Add("@ProizvodID", SqlDbType.Int).Value = cbProizvod.SelectedValue;

                    if (azuriraj)
                    {
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                        cmd.CommandText = @"UPDATE tblRacun SET DatumIzdavanja=@DatumIzdavanja, UkupnaCena=@UkupnaCena,
                            ZaposleniID=@ZaposleniID, KupacID=@KupacID, ProizvodID=@ProizvodID  WHERE RacunID=@id";
                        red = null;
                    }
                    else
                    {

                        cmd.CommandText = @"INSERT INTO tblRacun (DatumIzdavanja, UkupnaCena, ZaposleniID, KupacID, ProizvodID)
                                VALUES (@DatumIzdavanja, @UkupnaCena, @ZaposleniID, @KupacID, @ProizvodID)";
                    }

                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Morate odabrati datum izdavanja.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Error);
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

