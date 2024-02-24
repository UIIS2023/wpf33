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
    /// Interaction logic for Proizvod.xaml
    /// </summary>
    public partial class Proizvod : Window
    {

        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool azuriraj;
        private DataRowView? red;



        public Proizvod()
        {
            InitializeComponent();
            txtNaziv.Focus();
            konekcija = kon.KreirajKonekciju();
            PopuniPadajuceListe();
        } 
        public Proizvod(bool azuriraj, DataRowView red)
        {
            InitializeComponent();
            txtNaziv.Focus();
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

                string vratiDobavljace = @"SELECT DobavljacID, NazivDobavljaca as NazivDobavljaca FROM tblDobavljac";
                SqlDataAdapter daDobavljac = new SqlDataAdapter(vratiDobavljace, konekcija);
                DataTable dtDobavljac = new DataTable();
                daDobavljac.Fill(dtDobavljac);
                cbDobavljac.ItemsSource = dtDobavljac.DefaultView;
                daDobavljac.Dispose();
                dtDobavljac.Dispose();


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

                DateTime? selectedDate = dpRokTrajanja.SelectedDate;

                if (dpRokTrajanja.SelectedDate != null)
                {

                    DateTime date = (DateTime)dpRokTrajanja.SelectedDate;
                    string datum = date.ToString("yyyy-MM-dd");



                    SqlCommand cmd = new SqlCommand
                    {
                        Connection = konekcija
                    };

              
                        cmd.Parameters.Add("@NazivProizvoda", SqlDbType.NVarChar).Value = txtNaziv.Text;
                        cmd.Parameters.Add("@Proizvodjac", SqlDbType.NVarChar).Value = txtProizvodjac.Text;
                        cmd.Parameters.Add("@RokTrajanja", SqlDbType.DateTime).Value = datum;
                        cmd.Parameters.Add("@Cena", SqlDbType.Float).Value = txtCena.Text;
                        cmd.Parameters.Add("@Opis", SqlDbType.NVarChar).Value = txtOpis.Text;
                        cmd.Parameters.Add("@DobavljacID", SqlDbType.Int).Value = cbDobavljac.SelectedValue;

                    

                    if (azuriraj)
                    {
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                        cmd.CommandText = @"UPDATE tblProizvod SET NazivProizvoda=@NazivProizvoda, Proizvodjac=@Proizvodjac,
                            RokTrajanja=@RokTrajanja, Cena=@Cena, Opis=@Opis, DobavljacID=@DobavljacID WHERE ProizvodID=@id";
                        red = null;
                    }


                    else
                    {
                        cmd.CommandText = @"insert into tblProizvod (NazivProizvoda, Proizvodjac, RokTrajanja, Cena, Opis, DobavljacID)
                                          VALUES(@NazivProizvoda,@Proizvodjac,@RokTrajanja,@Cena,@Opis,@DobavljacID)";
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
