using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZarzadzanieUprawamiGUI
{
    public partial class PoleGUI : Form
    {
        private zarzadzanieUprawami2Entities1 zarzadzanieUprawami;
        private List<ListSortDirection> sortowania;
        private ObszaryGUI obszaryDialog;
        int columns = 8;
        int columnNr = 0;

        public PoleGUI()
        {
            InitializeComponent();
            zarzadzanieUprawami = new zarzadzanieUprawami2Entities1();
            inicjujSortowania();
            ustawBazeDanych();
            aktualizujComboBoxy();
        }
       
         private void ustawBazeDanych()
        {
            aktualizujTabele();
            dataGridView1.Columns[0].HeaderText = "Nr Pola";
            dataGridView1.Columns[1].HeaderText = "Nr Obszaru";

            for (int i = 7; i < dataGridView1.Columns.Count; i++)
                dataGridView1.Columns[i].Visible = false;
            tableReadOnly(true);
            zarzadzanieUprawami.SaveChanges();
        }

        private void tableReadOnly(bool set)
        {
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
                dataGridView1.Columns[i].ReadOnly = true;
        }

        private void aktualizujTabele()
        {


            var query = from c in zarzadzanieUprawami.Pola
                        select new
                        {
                            c.idPole,
                            Nr_Obszaru = c.idObszar,
                            Powierzchnia = c.Obszary.powierzchniaHa,
                            Szerokość = c.Obszary.szer_geo,
                            Dlugość = c.Obszary.dl_geo,
                            Klasa = c.klasa,
                            Gleby = c.Gleby.rodzaj,
                        };
            switch (columnNr)
            {
                case 0:
                    if (sortowania[columnNr] == ListSortDirection.Ascending)
                        query = query.OrderBy(c => c.idPole);
                    else
                        query = query.OrderByDescending(c => c.idPole);
                    break;
                case 1:
                    if (sortowania[columnNr] == ListSortDirection.Ascending)
                        query = query.OrderBy(c => c.Nr_Obszaru);
                    else
                        query = query.OrderByDescending(c => c.Nr_Obszaru);
                    break;
                case 2:
                    if (sortowania[columnNr] == ListSortDirection.Ascending)
                        query = query.OrderBy(c => c.Powierzchnia);
                    else
                        query = query.OrderByDescending(c => c.Powierzchnia);
                    break;
                case 3:
                    if (sortowania[columnNr] == ListSortDirection.Ascending)
                        query = query.OrderBy(c => c.Szerokość);
                    else
                        query = query.OrderByDescending(c => c.Szerokość);
                    break;
                case 4:
                    if (sortowania[columnNr] == ListSortDirection.Ascending)
                        query = query.OrderBy(c => c.Dlugość);
                    else
                        query = query.OrderByDescending(c => c.Dlugość);
                    break;
                case 5:
                    if (sortowania[columnNr] == ListSortDirection.Ascending)
                        query = query.OrderBy(c => c.Klasa);
                    else
                        query = query.OrderByDescending(c => c.Klasa);
                    break;
                case 6:
                    if (sortowania[columnNr] == ListSortDirection.Ascending)
                        query = query.OrderBy(c => c.Gleby);
                    else
                        query = query.OrderByDescending(c => c.Gleby);
                    break;
                
            }

            var users = query.ToList();
            dataGridView1.DataSource = users;
        }

        private void aktualizujComboBoxy()
        {
            comboBox1.DataSource = zarzadzanieUprawami.Obszary.ToList<Obszary>();
            comboBox1.ValueMember = "idObszar";
            comboBox2.Items.Add("1");
            comboBox2.Items.Add("2");
            comboBox2.Items.Add("3");
            comboBox2.Items.Add("4");
            comboBox2.Items.Add("5");
            comboBox2.Items.Add("6");
            comboBox2.SelectedIndex = 0;

            comboBox3.DataSource = zarzadzanieUprawami.Gleby.ToList<Gleby>();
            comboBox3.ValueMember = "rodzaj";

            comboBox8.DataSource = zarzadzanieUprawami.Pola.ToList<Pola>();
            comboBox8.ValueMember = "idPole";

            comboBox4.DataSource = zarzadzanieUprawami.Obszary.ToList<Obszary>();
            comboBox4.ValueMember = "idObszar";

        }


        private void inicjujSortowania()
        {
            sortowania = new List<ListSortDirection>();
            for (int i = 0; i < columns; i++)
                sortowania.Add(ListSortDirection.Ascending);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }

            if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }

            if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }

            if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }
        }

         private void button1_Click(object sender, EventArgs e)
         {
                  var ostatnia = zarzadzanieUprawami.Pola.OrderByDescending(c => c.idPole).FirstOrDefault();
                  int newId = (null == ostatnia ? 0 : ostatnia.idPole) + 1;
                  int IdGleba =((Gleby)(comboBox3.SelectedItem)).idGleba;

                  var nowePole = new Pola()
                  {
                      idPole = newId,
                      idObszar = ((Obszary)(comboBox1.SelectedItem)).idObszar,
                      klasa = Int32.Parse(comboBox2.Text),
                      idGleba = ((Gleby)(comboBox3.SelectedItem)).idGleba
                  };
                  zarzadzanieUprawami.Pola.Add(nowePole);
                  zarzadzanieUprawami.SaveChanges();
                  aktualizujTabele();
                  aktualizujComboBoxy();
                  
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int zmienna = Int32.Parse(comboBox8.Text);
            Boolean flaga = true; ;

            var Lista = from c in zarzadzanieUprawami.Uprawy
                           where c.idPole == zmienna
                           select new
                           {
                               Nr_Uprawy = c.idUprawa,
                               Nr_pola = c.idUprawa
                           };
            foreach (var danePole in Lista)
            {
                Console.WriteLine(" Lista pusta ? Dane Uprawy: " +  danePole.Nr_Uprawy + " nr pola: " + danePole.Nr_pola);
                flaga = false;
            }

            if (flaga)
            {
                zarzadzanieUprawami.Pola.Remove(((Pola)(comboBox8.SelectedItem)));
                zarzadzanieUprawami.SaveChanges();
                MessageBox.Show("Usunięto Pole o numerze ID " + comboBox8.Text, " Info");
                aktualizujTabele();
                aktualizujComboBoxy();
            }
            else
            {
                MessageBox.Show("Nie można usunąć Pola, ponieważ jest używane przez Uprawy ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            var ostatnia = zarzadzanieUprawami.Obszary.OrderByDescending(c => c.idObszar).FirstOrDefault();
            int newId = (null == ostatnia ? 0 : ostatnia.idObszar) + 1;

            var nowyObszar = new Obszary()
            {
                idObszar = newId,
                powierzchniaHa = double.Parse(textBox1.Text, CultureInfo.InvariantCulture),
                szer_geo = double.Parse(textBox2.Text, CultureInfo.InvariantCulture),
                dl_geo = double.Parse(textBox3.Text, CultureInfo.InvariantCulture)
            };

            zarzadzanieUprawami.Obszary.Add(nowyObszar);
            MessageBox.Show("Dodano Obszar o numerze ID:  " + nowyObszar.idObszar + ".\n Jego powierzchnia to: " + nowyObszar.powierzchniaHa + ", szerokość : " + nowyObszar.szer_geo + ", długość : " + nowyObszar.dl_geo , " Info");
            zarzadzanieUprawami.SaveChanges();
            aktualizujTabele();
            aktualizujComboBoxy();
        
        }

        private void button4_Click(object sender, EventArgs e)
        {

            int zmienna = Int32.Parse(comboBox4.Text);
            Boolean flaga = true; ;

            var Lista = from c in zarzadzanieUprawami.Pola
                        where c.idObszar == zmienna
                        select new
                        {
                            Nr_Pola = c.idPole,
                            Nr_Obszaru = c.idObszar
                        };
            foreach (var danePole in Lista)
            {
                Console.WriteLine(" Lista pusta ? Dane Uprawy: " + danePole.Nr_Pola + " nr pola: " + danePole.Nr_Obszaru);
                flaga = false;
            }

            if (flaga)
            {
                zarzadzanieUprawami.Obszary.Remove(((Obszary)(comboBox4.SelectedItem)));
                zarzadzanieUprawami.SaveChanges();
                MessageBox.Show("Usunięto Obszar o numerze ID " + comboBox4.Text, " Info");
                aktualizujTabele();
                aktualizujComboBoxy();
            }
            else 
            {
                MessageBox.Show("Nie można usunąć Obsaru, ponieważ jest używane przez Pola ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            obszaryDialog = new ObszaryGUI();
            obszaryDialog.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            aktualizujTabele();
            aktualizujComboBoxy();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            aktualizujTabele();
            dataGridView1.ClearSelection();
        }

        private void PoleGUI_Load(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
        }
    }
}
