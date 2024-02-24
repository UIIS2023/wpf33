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
    /// Interaction logic for SastojakProizvod.xaml
    /// </summary>
    public partial class SastojakProizvod : Window
    {

        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool azuriraj;
        private DataRowView? red;

        public SastojakProizvod()
        {
            InitializeComponent();
            txtKolicina.Focus();
            konekcija = kon.KreirajKonekciju();
            PopuniPadajuceListe();
        }
        public SastojakProizvod(bool azuriraj, DataRowView red)
        {
            InitializeComponent();
            txtKolicina.Focus();
            konekcija = kon.KreirajKonekciju();
            this.azuriraj = azuriraj;
            this.red = red;
            PopuniPadajuceListe();
        }

        private void PopuniPadajuceListe()
        {
            try
            {
                konekcija.Open();

                string vratiSastojke = @"SELECT SastojakID, Ime FROM tblSastojak";
                SqlDataAdapter daSastojak = new SqlDataAdapter(vratiSastojke, konekcija);
                DataTable dtSastojak = new DataTable();
                daSastojak.Fill(dtSastojak);
                cbSastojak.ItemsSource = dtSastojak.DefaultView;
                daSastojak.Dispose();
                dtSastojak.Dispose();


                string vratiProizvode = @"SELECT ProizvodID, NazivProizvoda as Proizvod FROM tblProizvod";
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

                    SqlCommand cmd = new SqlCommand
                    {
                        Connection = konekcija
                    };
                    cmd.Parameters.Add("@Kolicina", SqlDbType.Float).Value = txtKolicina.Text;
                    cmd.Parameters.Add("@SastojakID", SqlDbType.Int).Value = cbSastojak.SelectedValue;
                    cmd.Parameters.Add("@ProizvodID", SqlDbType.Int).Value = cbProizvod.SelectedValue;

                    if (azuriraj)
                    {
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                        cmd.CommandText = @"UPDATE tblSastojak_Proizvod SET Kolicina=@Kolicina, SastojakID=@SastojakID, ProizvodID=@ProizvodID
                                       WHERE Sastojak_ProizvodID=@id";
                        red = null;
                    }
                    else
                    {
                        cmd.CommandText = @"insert into tblSastojak_Proizvod (Kolicina, SastojakID, ProizvodID)
                                          VALUES(@Kolicina,@SastojakID,@ProizvodID)";
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
