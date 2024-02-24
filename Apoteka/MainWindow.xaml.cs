using Apoteka.Forme;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace Apoteka
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private string ucitanaTabela;
        private bool azuriraj;
        private DataRowView red;

        // razliciti regioni za selecte
        #region Select sa uslovom 
        private static string selectUslovKupac = @"select * from tblKupac where KupacID=";
        private static string selectUslovDobavljac = @"select * from tblDobavljac where DobavljacID=";
        private static string selectUslovProizvod = @"select * from tblProizvod where ProizvodID=";
        private static string selectUslovRacun = @"select * from tblRacun where RacunID=";
        private static string selectUslovTekuciRacun = @"select * from tblTekuciRacun where TekuciRacunID=";
        private static string selectUslovSastojak = @"select * from tblSastojak where SastojakID=";
        private static string selectUslovSastojakProizvod = @"select * from tblSastojak_Proizvod where Sastojak_ProizvodID=";
        private static string selectUslovZaposleni = @"select * from tblZaposleni where ZaposleniID=";
        #endregion

        #region Delete sa uslovom
        private static string KupacDelete = @"delete from tblKupac where KupacID=";
        private static string DobavljacDelete = @"delete from tblDobavljac where DobavljacID=";
        private static string ProizvodDelete = @"delete from tblProizvod where ProizvodID=";
        private static string RacunDelete = @"delete from tblRacun where RacunID=";
        private static string TekuciRacunDelete = @"delete from tblTekuciRacun where TekuciRacunID=";
        private static string SastojakDelete = @"delete from tblSastojak where SastojakID=";
        private static string SastojakProizvodDelete = @"delete from tblSastojak_Proizvod where Sastojak_ProizvodID=";
        private static string ZaposleniDelete = @"delete from tblZaposleni where ZaposleniID=";
        #endregion

        #region Select Upiti

        private static string KupacSelect = @"select KupacID as ID, ImeKupca as Ime,PrezimeKupca as Prezime,DatumRodjenjaKupca as DatumRodjenja,Pol as Pol from tblKupac";

        private static string DobavljacSelect = @"select DobavljacID as ID, NazivDobavljaca as Dobavljac,KontaktDobavljaca as Kontakt from tblDobavljac";

        private static string ProizvodSelect = @"select ProizvodID as ID, NazivProizvoda as NazivProizvoda,Proizvodjac, RokTrajanja, Cena, Opis from tblProizvod
                                                    join tblDobavljac on tblProizvod.DobavljacID = tblDobavljac.DobavljacID";


        private static string RacunSelect = @"select RacunID as ID, DatumIzdavanja as DatumIzdavanja, UkupnaCena as UkupnaCena from tblRacun
                                      join tblKupac on tblRacun.KupacID = tblKupac.KupacID
                                      join tblProizvod on tblRacun.ProizvodID = tblProizvod.ProizvodID
                                      join tblZaposleni on tblRacun.ZaposleniID = tblZaposleni.ZaposleniID";


        private static string TekuciRacunSelect = @"select TekuciRacunID as ID, ImePostojecegKupca as Ime, PrezimePostojecegKupca as Prezime, Stanje from tblTekuciRacun
                                                    join tblKupac on tblTekuciRacun.KupacID = tblKupac.KupacID";

        private static string SastojakSelect = @"select SastojakID as ID, Ime from tblSastojak";

        private static string SastojakProizvodSelect = @"select Sastojak_ProizvodID as ID, Kolicina from tblSastojak_Proizvod
                                                       join tblSastojak on tblSastojak_Proizvod.SastojakID = tblSastojak.SastojakID
                                                       join tblProizvod on tblSastojak_Proizvod.ProizvodID = tblProizvod.ProizvodID";

        private static string ZaposleniSelect = @"select ZaposleniID as ID, Ime , Prezime, JMBG, Grad, Adresa, KontaktZaposlenog as Kontakt, KorisnickoIme as Username, Lozinka as Password from tblZaposleni";


        #endregion

        public MainWindow()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            UcitajPodatke(KupacSelect);
        }


        private void UcitajPodatke (string selectUpiti)
        {

            try
            {

                konekcija.Open();
                SqlDataAdapter dataadapter = new SqlDataAdapter(selectUpiti, konekcija);
                DataTable dataTable = new DataTable();
                dataadapter.Fill(dataTable);

                if (dataGridCentralni != null)
                {
                    dataGridCentralni.ItemsSource = dataTable.DefaultView;

                }
                ucitanaTabela = selectUpiti;
                dataadapter.Dispose();
                dataTable.Dispose();


            }
            catch (SqlException ex)
            {
                MessageBox.Show("Neuspešno učitani podaci. Greška: " + ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
            }

        }




        private void btnProizvod_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(ProizvodSelect);
        }

        private void btnKupac_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(KupacSelect);
        }

        private void btnTekuciRacun_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(TekuciRacunSelect);
        }

        private void btnDobavljac_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(DobavljacSelect);
        }

        private void btnSastojak_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(SastojakSelect);
        }

        private void btnRacun_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(RacunSelect);
        }

        private void btnSastojakProizvod_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(SastojakProizvodSelect);
        }

        private void btnZaposleni_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(ZaposleniSelect);
        }



        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            Window prozor;

            if (ucitanaTabela.Equals(KupacSelect))
            {
                prozor = new Kupac();
                prozor.ShowDialog();
                UcitajPodatke(KupacSelect);
            }
            else if (ucitanaTabela.Equals(DobavljacSelect))
            {
                prozor = new Dobavljac();
                prozor.ShowDialog();
                UcitajPodatke(DobavljacSelect);
            }
            else if (ucitanaTabela.Equals(ProizvodSelect))
            {
                prozor = new Proizvod();
                prozor.ShowDialog();
                UcitajPodatke(ProizvodSelect);
            }
            else if (ucitanaTabela.Equals(RacunSelect))
            {
                prozor = new Racun();
                prozor.ShowDialog();
                UcitajPodatke(RacunSelect);
            }
            else if (ucitanaTabela.Equals(SastojakSelect))
            {
                prozor = new Sastojak();
                prozor.ShowDialog();
                UcitajPodatke(SastojakSelect);
            }
            else if (ucitanaTabela.Equals(TekuciRacunSelect))
            {
                prozor = new TekuciRacun();
                prozor.ShowDialog();
                UcitajPodatke(TekuciRacunSelect);
            }
            else if (ucitanaTabela.Equals(ZaposleniSelect))
            {
                prozor = new Zaposleni();
                prozor.ShowDialog();
                UcitajPodatke(ZaposleniSelect);
            }
            else if (ucitanaTabela.Equals(SastojakProizvodSelect))
            {
                prozor = new SastojakProizvod();
                prozor.ShowDialog();
                UcitajPodatke(SastojakProizvodSelect);
            }


        }

        private void btnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            if (ucitanaTabela.Equals(KupacSelect))
            {
                PopuniFormu(selectUslovKupac);
                UcitajPodatke(KupacSelect);

            }
            else if (ucitanaTabela.Equals(DobavljacSelect))
            {
                PopuniFormu(selectUslovDobavljac);
                UcitajPodatke(DobavljacSelect);
            }
            else if (ucitanaTabela.Equals(ProizvodSelect))
            {
                PopuniFormu(selectUslovProizvod);
                UcitajPodatke(ProizvodSelect);
            }
            else if (ucitanaTabela.Equals(RacunSelect))
            {
                PopuniFormu(selectUslovRacun);
                UcitajPodatke(RacunSelect);
            }
            else if (ucitanaTabela.Equals(SastojakSelect))
            {
                PopuniFormu(selectUslovSastojak);
                UcitajPodatke(SastojakSelect);
            }
            else if (ucitanaTabela.Equals(TekuciRacunSelect))
            {
                PopuniFormu(selectUslovTekuciRacun);
                UcitajPodatke(TekuciRacunSelect);
            }
            else if (ucitanaTabela.Equals(ZaposleniSelect))
            {
                PopuniFormu(selectUslovZaposleni);
                UcitajPodatke(ZaposleniSelect);
            }
            else if (ucitanaTabela.Equals(SastojakProizvodSelect))
            {
                PopuniFormu(selectUslovSastojakProizvod);
                UcitajPodatke(SastojakProizvodSelect);
            }
            
        }


        private void PopuniFormu(object selectUslov)
        {
            try
            {
                konekcija.Open();
                azuriraj = true;
                red = (DataRowView)dataGridCentralni.SelectedItems[0];
                SqlCommand cmd = new SqlCommand
                {
                    Connection = konekcija
                };
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                cmd.CommandText = selectUslov + "@id";
                SqlDataReader citac = cmd.ExecuteReader();
                cmd.Dispose();
                if (citac.Read())
                {
                    if (ucitanaTabela.Equals(KupacSelect))
                    {
                        Kupac prozorKupac = new Kupac(azuriraj, red);
                        prozorKupac.txtIme.Text = citac["ImeKupca"].ToString();
                        prozorKupac.txtPrezime.Text = citac["PrezimeKupca"].ToString();
                        prozorKupac.txtPol.Text = citac["Pol"].ToString();
                        prozorKupac.dpDatumRodjenja.SelectedDate = (DateTime)citac["DatumRodjenjaKupca"];
                        prozorKupac.ShowDialog();

                    }
                    else if (ucitanaTabela.Equals(DobavljacSelect))
                    {
                        Dobavljac prozorDobavljac = new Dobavljac(azuriraj, red);
                        prozorDobavljac.txtNaziv.Text = citac["NazivDobavljaca"].ToString();
                        prozorDobavljac.txtKontakt.Text = citac["KontaktDobavljaca"].ToString();
                        prozorDobavljac.ShowDialog();

                    }
                    else if (ucitanaTabela.Equals(ProizvodSelect))
                    {
                        Proizvod prozorProizvod = new Proizvod(azuriraj, red);
                        prozorProizvod.txtNaziv.Text = citac["NazivProizvoda"].ToString();
                        prozorProizvod.txtProizvodjac.Text = citac["Proizvodjac"].ToString();
                        prozorProizvod.txtCena.Text = citac["Cena"].ToString();
                        prozorProizvod.txtOpis.Text = citac["Opis"].ToString();
                        prozorProizvod.dpRokTrajanja.Text = citac["RokTrajanja"].ToString();
                        prozorProizvod.cbDobavljac.SelectedValue = citac["DobavljacID"].ToString();
                        prozorProizvod.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(SastojakSelect))
                    {
                        Sastojak prozorSastojak = new Sastojak(azuriraj, red);
                        prozorSastojak.txtSastojak.Text = citac["Ime"].ToString();
                        prozorSastojak.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(SastojakProizvodSelect))
                    {
                        SastojakProizvod prozorSastojakProizvod = new SastojakProizvod(azuriraj, red);
                        prozorSastojakProizvod.txtKolicina.Text = citac["Kolicina"].ToString();
                        prozorSastojakProizvod.cbSastojak.SelectedValue = citac["SastojakID"].ToString();
                        prozorSastojakProizvod.cbProizvod.SelectedValue = citac["ProizvodID"].ToString();
                        prozorSastojakProizvod.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(TekuciRacunSelect))
                    {
                        TekuciRacun prozorTekuciRacun = new TekuciRacun(azuriraj, red);
                        prozorTekuciRacun.cbKupac.IsEnabled = false;
                        prozorTekuciRacun.txtStanje.Text = citac["Stanje"].ToString();
                        prozorTekuciRacun.cbKupac.SelectedValue = citac["KupacID"].ToString();
                        prozorTekuciRacun.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(ZaposleniSelect))
                    {
                        Zaposleni prozorZaposleni = new Zaposleni(azuriraj, red);
                        prozorZaposleni.txtIme.Text = citac["Ime"].ToString();
                        prozorZaposleni.txtPrezime.Text = citac["Prezime"].ToString();
                        prozorZaposleni.txtJMBG.Text = citac["JMBG"].ToString();
                        prozorZaposleni.txtGrad.Text = citac["Grad"].ToString();
                        prozorZaposleni.txtAdresa.Text = citac["Adresa"].ToString();
                        prozorZaposleni.txtKontaktZaposlenog.Text = citac["KontaktZaposlenog"].ToString();
                        prozorZaposleni.txtKorisnickoIme.Text = citac["KorisnickoIme"].ToString();
                        prozorZaposleni.txtLozinka.Text = citac["Lozinka"].ToString();
                        prozorZaposleni.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(RacunSelect))
                    {
                        Racun prozorRacun = new Racun(azuriraj, red);
                        prozorRacun.dpRacun.Text = citac["DatumIzdavanja"].ToString();
                        prozorRacun.txtUkupnaCena.Text = citac["UkupnaCena"].ToString();                
                        prozorRacun.cbZaposleni.SelectedValue = citac["ZaposleniID"].ToString();
                        prozorRacun.cbKupac.SelectedValue = citac["KupacID"].ToString();
                        prozorRacun.cbProizvod.Text = citac["ProizvodID"].ToString();
                        prozorRacun.cbZaposleni.IsEnabled = false;
                        prozorRacun.cbKupac.IsEnabled = false;
                        prozorRacun.ShowDialog();
                    }
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Red nije selektovan!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
            }
        }      
        private void btnObrisi_Click(object sender, RoutedEventArgs e)
        {
            if (ucitanaTabela.Equals(KupacSelect))
            {
                ObrisiZapis(KupacDelete);
                UcitajPodatke(KupacSelect);

            }
            else if (ucitanaTabela.Equals(DobavljacSelect))
            {
                ObrisiZapis(DobavljacDelete);
                UcitajPodatke(DobavljacSelect);
            }
            else if (ucitanaTabela.Equals(ProizvodSelect))
            {
                ObrisiZapis(ProizvodDelete);
                UcitajPodatke(ProizvodSelect);
            }
            else if (ucitanaTabela.Equals(RacunSelect))
            {
                ObrisiZapis(RacunDelete);
                UcitajPodatke(RacunSelect);
            }
            else if (ucitanaTabela.Equals(SastojakSelect))
            {
                ObrisiZapis(SastojakDelete);
                UcitajPodatke(SastojakSelect);
            }
            else if (ucitanaTabela.Equals(TekuciRacunSelect))
            {
                ObrisiZapis(TekuciRacunDelete);
                UcitajPodatke(TekuciRacunSelect);
            }
            else if (ucitanaTabela.Equals(ZaposleniSelect))
            {
                ObrisiZapis(ZaposleniDelete);
                UcitajPodatke(ZaposleniSelect);
            }
            else if (ucitanaTabela.Equals(SastojakProizvodSelect))
            {
                ObrisiZapis(SastojakProizvodDelete);
                UcitajPodatke(SastojakProizvodSelect);
            }

        }

        private void ObrisiZapis(string deleteUslov)
        {
            try
            {
                konekcija.Open();
                DataRowView red = (DataRowView)dataGridCentralni.SelectedItems[0];
                MessageBoxResult rezultat = MessageBox.Show("Jeste li sigurni?", "Upozorenje!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (rezultat == MessageBoxResult.Yes)
                {
                    SqlCommand cmd = new SqlCommand
                    {
                        Connection = konekcija
                    };
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = deleteUslov + "@id";
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Red nije selektovan!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (SqlException)
            {
                MessageBox.Show("Postoje podaci koji su povezani u drugim tabelama!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
            }
        }
    }
}
