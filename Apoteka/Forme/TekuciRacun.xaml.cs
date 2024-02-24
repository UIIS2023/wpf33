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
    /// Interaction logic for TekuciRacun.xaml
    /// </summary>
    public partial class TekuciRacun : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool azuriraj;
        private DataRowView? red;

        public TekuciRacun()
        {
            InitializeComponent();
            txtStanje.Focus();
            konekcija = kon.KreirajKonekciju();

            PopuniPadajuceListe();

        }
        public TekuciRacun(bool azuriraj, DataRowView red)
        {
            InitializeComponent();
            txtStanje.Focus();
            konekcija = kon.KreirajKonekciju();
            this.azuriraj = azuriraj;
            this.red = red;

            PopuniPadajuceListe();

        }

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }




        private void PopuniPadajuceListe()
        {
            try
            {
                konekcija.Open();

                string vratiKupce = @"SELECT KupacID, ImeKupca + ' ' + PrezimeKupca as Kupac FROM tblKupac";
                SqlDataAdapter daKupac = new SqlDataAdapter(vratiKupce, konekcija);
                DataTable dtKupac = new DataTable();
                daKupac.Fill(dtKupac);
                cbKupac.ItemsSource = dtKupac.DefaultView;
                daKupac.Dispose();
                dtKupac.Dispose();

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
            konekcija = kon.KreirajKonekciju();
            try
            {
                if (konekcija.State != ConnectionState.Open)
                {
                    konekcija.Open();
                }

                SqlCommand cmd = new SqlCommand
                {
                    Connection = konekcija
                };





                cmd.Parameters.Add("ImePostojecegKupca", SqlDbType.NVarChar).Value = txtImePostojecegKupca.Text;
                cmd.Parameters.Add("PrezimePostojecegKupca", SqlDbType.NVarChar).Value = txtPrezimePostojecegKupca.Text;
                cmd.Parameters.Add("@Stanje", SqlDbType.Float).Value = txtStanje.Text;
                cmd.Parameters.Add("@KupacID", SqlDbType.Int).Value = cbKupac.SelectedValue;



                if (azuriraj)
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"UPDATE tblTekuciRacun SET ImePostojecegKupca=@ImePostojecegKupca,PrezimePostojecegKupca=@PrezimePostojecegKupca, Stanje=@Stanje,KupacID=@KupacID
                                       WHERE TekuciRacunID=@id";
                    red = null;
                }
                else
                {
                    cmd.CommandText = @"insert into tblTekuciRacun (ImePostojecegKupca,PrezimePostojecegKupca,Stanje,KupacID)
                                           VALUES(@ImePostojecegKupca,@PrezimePostojecegKupca,@Stanje,@KupacID)";
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
        private void cbKupac_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cbKupac.SelectedItem != null)
                {
                    DataRowView selectedKupac = cbKupac.SelectedItem as DataRowView;
                    if (selectedKupac != null)
                    {
                        int kupacID = Convert.ToInt32(selectedKupac["KupacID"]);

                        string sqlUpit = "SELECT ImeKupca, PrezimeKupca FROM tblKupac WHERE KupacID = @KupacID";

                        using (SqlCommand command = new SqlCommand(sqlUpit, konekcija))
                        {
                            if (konekcija.State == ConnectionState.Closed)
                            {
                                konekcija.Open();
                            }

                            command.Parameters.AddWithValue("@KupacID", kupacID);

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    txtImePostojecegKupca.Text = reader["ImeKupca"].ToString();
                                    txtPrezimePostojecegKupca.Text = reader["PrezimeKupca"].ToString();
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Niste popunili padajuce liste! Greška: " + ex.Message, "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija.State == ConnectionState.Open)
                {
                    konekcija.Close();
                }
            }
        }
    }
}