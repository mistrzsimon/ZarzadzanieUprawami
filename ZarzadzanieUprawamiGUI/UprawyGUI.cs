using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using System.Data.SqlClient;

namespace ZarzadzanieUprawamiGUI
{
    enum Sortowanie { brak, rosnace, malejace};
    public partial class UprawyGUI : Form
    {
        private zarzadzanieUprawami2Entities1 zarzadzanieUprawami;
        private List<ListSortDirection> sortowania;
        private PoleGUI poleDialog;
        List<int> daty = new List<int>();
        SqlCommand cmd;

        //filtrowanie
        private bool filtrCzasowy;
        DateTime dataPocz;
        DateTime dataKonc;

        //sortowanie
        int columns = 8;
        int columnNr = 0;

        //SqlConnection con = new SqlConnection("Data source=DESKTOP-OBLSHB6;initial catalog=zarzadzanieUprawami2;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework");

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SVI22LM\ZARZADZANIE;initial catalog=zarzadzanieUprawami2;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework");

        public UprawyGUI()
        {
            filtrCzasowy = false;
            InitializeComponent();
            zarzadzanieUprawami = new zarzadzanieUprawami2Entities1();
            inicjujSortowania();
            ustawBazeDanych();
            comboBox10.SelectedIndex = 0;
            // wyswietlRosliny();

        }


        private void inicjalizujPola()
        {
            foreach (var p in zarzadzanieUprawami.Pola)
            {
                p.inicjalizujPredyspozycje();
            }
        }

        private void wyswietlGleby()
        {
            for(int i = 0; i < 10; i++)
            {

                Console.WriteLine(" ===Rok: {0}===", i+1);
                foreach (var p in zarzadzanieUprawami.Pola)
                {                
                    p.liczZyski(zarzadzanieUprawami.Rosliny);
                }
            }
 
        }

        private void wyswietlRosliny()
        {
            foreach (var p in zarzadzanieUprawami.Rosliny)
            {
              
                Console.WriteLine("Waga pszenica: {0}", p.kosztZbioruHa);
            }

        }


        private void dodaj_Click(object sender, EventArgs e)
        {
            dodajUprawe();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            aktualizujTabele();
            dataGridView1.ClearSelection();
        }

       
        private void tableReadOnly(bool set)
        {
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
                dataGridView1.Columns[i].ReadOnly = true;
        }


        private void inicjujSortowania()
        {
            sortowania = new List<ListSortDirection>();
            for (int i = 0; i < columns; i++)
                sortowania.Add(ListSortDirection.Ascending);
        }

        private void aktualizujTabele()
        {
            var query = from c in zarzadzanieUprawami.Uprawy
            select new
            {
                c.idUprawa,
                Nr_pola = c.idPole,
                Gleby = c.Pola.Gleby.rodzaj,
                Roslina = c.Rosliny.Gatunki.nazwa,
                Odmiana = c.Rosliny.Odmiany.nazwa,
                c.dataPoczatkowa,
                c.dataKoncowa,
            };
            if(filtrCzasowy)
            {
                query = query.Where(c => c.dataPoczatkowa >= dataPocz && c.dataKoncowa <= dataKonc);
            }
            switch(columnNr)
            { 
                case 0:
                    if (sortowania[columnNr] == ListSortDirection.Ascending)
                        query = query.OrderBy(c => c.idUprawa);
                    else
                        query = query.OrderByDescending(c => c.idUprawa);
                    break;
                case 1:
                    if (sortowania[columnNr] == ListSortDirection.Ascending)
                        query = query.OrderBy(c => c.Nr_pola);
                    else
                        query = query.OrderByDescending(c => c.Nr_pola);
                    break;
                case 2:
                    if (sortowania[columnNr] == ListSortDirection.Ascending)
                        query = query.OrderBy(c => c.Roslina);
                    else
                        query = query.OrderByDescending(c => c.Roslina);
                    break;
                case 3:
                    if (sortowania[columnNr] == ListSortDirection.Ascending)
                        query = query.OrderBy(c => c.Odmiana);
                    else
                        query = query.OrderByDescending(c => c.Odmiana);
                    break;
                case 4:
                    if (sortowania[columnNr] == ListSortDirection.Ascending)
                        query = query.OrderBy(c => c.dataPoczatkowa);
                    else
                        query = query.OrderByDescending(c => c.dataPoczatkowa);
                    break;
                case 5:
                    if (sortowania[columnNr] == ListSortDirection.Ascending)
                        query = query.OrderBy(c => c.dataPoczatkowa);
                    else
                        query = query.OrderByDescending(c => c.dataPoczatkowa);
                    break;
            }

            var users = query.ToList();
            dataGridView1.DataSource = users;
        }

        private void ustawBazeDanych()
        {

            aktualizujTabele();            
            dataGridView1.Columns[0].HeaderText = "Nr uprawy";
            dataGridView1.Columns[1].HeaderText = "Nr pola";           
            
            for (int i = 7; i < dataGridView1.Columns.Count; i++)
                dataGridView1.Columns[i].Visible = false;
            tableReadOnly(true);
            zarzadzanieUprawami.SaveChanges();


            aktualizujComboBoxy();     

        }


        private int zwrocRosline(int IdGatunek, int IdOdmiana)
        {
            var query = from roslina in zarzadzanieUprawami.Rosliny
                        where roslina.idGatunek == IdGatunek && roslina.idOdmiana == IdOdmiana
                        select roslina.idRoslina;

            var znalezione = query.ToList<int>();
            if (znalezione.Count > 0)
                return znalezione[0];
            else
            {
                var ostatnia = zarzadzanieUprawami.Rosliny.OrderByDescending(c => c.idRoslina).FirstOrDefault();
                int newId = (null == ostatnia ? 0 : ostatnia.idRoslina) + 1;
                var nowaRoslina = new Rosliny()
                {
                    idRoslina = newId,
                    idGatunek = IdGatunek,
                    idOdmiana = IdOdmiana,
                    idOkresowosc = 1,
                };
                zarzadzanieUprawami.Rosliny.Add(nowaRoslina);
                zarzadzanieUprawami.SaveChanges();

                return newId;
            }

        }


        private void usun_Click(object sender, EventArgs e)
        {
            try
            {
            zarzadzanieUprawami.Uprawy.Remove(((Uprawy)(comboBox8.SelectedItem)));
            zarzadzanieUprawami.SaveChanges();
            aktualizujTabele();
            aktualizujComboBoxy();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd : " + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
   
        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            columnNr = e.ColumnIndex;
            sortowania[columnNr] = sortowania[columnNr] == ListSortDirection.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending;
            aktualizujTabele();
        }


        private void button4_Click(object sender, EventArgs e)
        {
            generujRaport();

        }

        private void Filtruj_Click(object sender, EventArgs e)
        {
            filtrCzasowy = true;
            dataPocz = dateTimePicker5.Value;
            dataKonc = dateTimePicker6.Value;
            aktualizujTabele();
        }

        private void Resetuj_Click(object sender, EventArgs e)
        {
            filtrCzasowy = false;
            aktualizujTabele();
            aktualizujComboBoxy();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            aktualizujUprawe();
        }

        
       
        private void dodajUprawe()
        {
            Boolean flaga1 = false;
            Boolean flaga2 = true;
            try {
                var ostatnia = zarzadzanieUprawami.Uprawy.OrderByDescending(c => c.idUprawa).FirstOrDefault();
                int newId = (null == ostatnia ? 0 : ostatnia.idUprawa) + 1;

                int IdRoslina = zwrocRosline(((Gatunki)(comboBox2.SelectedItem)).idGatunek, ((Odmiany)(comboBox3.SelectedItem)).idOdmiana);

                var nowaUprawa = new Uprawy()
                {
                    idUprawa = newId,
                    idPole = ((Pola)(comboBox1.SelectedItem)).idPole,
                    idRoslina = IdRoslina,
                    dataPoczatkowa = dateTimePicker1.Value.Date ,
                    dataKoncowa = dateTimePicker2.Value.Date
                };

                var ListaDat = from c in zarzadzanieUprawami.Uprawy
                               where c.idPole == nowaUprawa.idPole
                               select new
                               {
                                   Nr_pola = c.idPole,
                                   DataPocza = c.dataPoczatkowa,
                                   DataKonc = c.dataKoncowa
                               };

                DateTime date1wpisana = nowaUprawa.dataPoczatkowa.Value;
                DateTime date2wpisana = nowaUprawa.dataKoncowa.Value;

                int result0 = DateTime.Compare(date1wpisana, date2wpisana);

                if (result0 > 0)
                {
                    MessageBox.Show("Pierwsza data jest późniejsza niż druga ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    foreach (var danePole in ListaDat)
                    {
                        DateTime date1zbazy = danePole.DataPocza.Value;
                        DateTime date2zbazy = danePole.DataKonc.Value;

                        int result1 = DateTime.Compare(date1wpisana, date1zbazy);
                        int result2 = DateTime.Compare(date2wpisana, date1zbazy);
                        int result3 = DateTime.Compare(date1wpisana, date2zbazy);
                        int result4 = DateTime.Compare(date2wpisana, date2zbazy);

                        if ((result1 < 0 && result2 < 0) || (result3 > 0 && result4 > 0))
                        {
                            flaga1 = true;
                            Console.WriteLine(" Wolna data ");
                        }
                        else
                        {
                            flaga2 = false;
                            Console.WriteLine(" Zajeta data ");
                        }

                    }

                    if (ListaDat.Count() == 0)
                    {
                        flaga1 = true;
                        flaga2 = true;
                    }


                    if (flaga1 && flaga2)
                    {
                        zarzadzanieUprawami.Uprawy.Add(nowaUprawa);
                        zarzadzanieUprawami.SaveChanges();
                        aktualizujTabele();
                        aktualizujComboBoxy();
                    }
                    else
                    {
                        MessageBox.Show("Nie można wpisać uprawy, ponieważ dane pole jest zajęte w tych datach ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }
                    Console.WriteLine("Flaga1: " + flaga1 + " flaga 2: " + flaga2);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd : " + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void aktualizujUprawe()
        {
            var uprawaDoAktualizacji = (Uprawy)(comboBox7.SelectedItem);
            int gatunek = (int)uprawaDoAktualizacji.Rosliny.idGatunek;
            int odmiana = (int)uprawaDoAktualizacji.Rosliny.idOdmiana;

            if (NrPola.Checked)
                uprawaDoAktualizacji.Pola = (Pola)comboBox6.SelectedItem;
            if (Roslina.Checked)
                gatunek = ((Gatunki)(comboBox5.SelectedItem)).idGatunek;
            if (Odmiana.Checked)
                odmiana = ((Odmiany)(comboBox4.SelectedItem)).idOdmiana;
            if (DataP.Checked)
                uprawaDoAktualizacji.dataPoczatkowa = dateTimePicker3.Value;
            if (DataK.Checked)
                uprawaDoAktualizacji.dataKoncowa = dateTimePicker4.Value;

            if (Odmiana.Enabled || Roslina.Enabled)
                uprawaDoAktualizacji.Rosliny = zarzadzanieUprawami.Rosliny.Find(zwrocRosline(gatunek, odmiana));


            zarzadzanieUprawami.SaveChanges();
            aktualizujTabele();
            aktualizujComboBoxy();
        }

        private void generujRaport()
        {
            try
            {
                if (comboBox9.SelectedItem.ToString() == "Suma dotacji")
                {

                    var baza = new zarzadzanieUprawami2Entities1();

                    cmd = new SqlCommand {
                        Connection = con,
                        CommandType = CommandType.Text,
                        CommandText = (@"SELECT SUM(Obszary.powierzchniaHa * Rosliny.doplatyHa) as 'Suma doplat [zł]'
                                        FROM Uprawy
                                        INNER JOIN Pola
                                        ON Uprawy.idPole = Pola.idPole
                                        INNER JOIN Obszary
                                        ON Pola.idObszar = Obszary.idObszar
                                        INNER JOIN Rosliny
                                        ON Uprawy.idRoslina = Rosliny.idRoslina")
                    };
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adap.Fill(dt);
                    dataGridView2.DataSource = dt;

                }

                else if (comboBox9.SelectedItem.ToString() == "Średnia powierzchnia pól")
                {
                    var baza = new zarzadzanieUprawami2Entities1();
                    cmd = new SqlCommand {
                        Connection = con,
                        CommandType = CommandType.Text,
                        CommandText = ("select  AVG(powierzchniaHa)as 'Średnia powierzchnia pół [Ha]' from Obszary")
                     };
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adap.Fill(dt);

                    dataGridView2.DataSource = dt;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void aktualizujComboBoxy()
        {
            comboBox1.DataSource = zarzadzanieUprawami.Pola.ToList<Pola>();
            comboBox1.ValueMember = "idPole";
            comboBox2.DataSource = zarzadzanieUprawami.Gatunki.ToList<Gatunki>();
            comboBox2.ValueMember = "nazwa";
            comboBox3.DataSource = zarzadzanieUprawami.Odmiany.ToList<Odmiany>();
            comboBox3.ValueMember = "nazwa";

            comboBox6.DataSource = zarzadzanieUprawami.Pola.ToList<Pola>();
            comboBox6.ValueMember = "idPole";
            comboBox5.DataSource = zarzadzanieUprawami.Gatunki.ToList<Gatunki>();
            comboBox5.ValueMember = "nazwa";
            comboBox4.DataSource = zarzadzanieUprawami.Odmiany.ToList<Odmiany>();
            comboBox4.ValueMember = "nazwa";

            comboBox7.DataSource = zarzadzanieUprawami.Uprawy.ToList<Uprawy>();
            comboBox7.ValueMember = "idUprawa";

            comboBox8.DataSource = zarzadzanieUprawami.Uprawy.ToList<Uprawy>();
            comboBox8.ValueMember = "idUprawa";
        }

        private void ZarzadzajPole_Click(object sender, EventArgs e)
        {
            poleDialog = new PoleGUI();
            poleDialog.ShowDialog();
            aktualizujComboBoxy();
        }

        private void prognozuj_Click(object sender, EventArgs e)
        {
            //inicjalizujPola();
            // wyswietlGleby();
            progressBar1.Minimum = 0;
            progressBar1.Maximum = Convert.ToInt32(comboBox10.Text);
            progressBar1.Step = 1;
            progressBar1.Value = 0;

            
            if (string.IsNullOrEmpty(comboBox10.Text)) {
                MessageBox.Show("Proszę wybrać na ile lat ma być prognoza");
            }
            else
            {
                Prognozuj Prognozuj = new Prognozuj(this)
                {
                    Owner = this
                };
                Prognozuj.ShowDialog();
            }
            
        }

        private void UprawyGUI_Load(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
        }
    }
}

