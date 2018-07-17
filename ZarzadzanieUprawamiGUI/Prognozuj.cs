using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Data = System.Collections.Generic.KeyValuePair<int, string>;

namespace ZarzadzanieUprawamiGUI
{
    public partial class Prognozuj : Form
    {
        UprawyGUI UprawyGUI;
        private zarzadzanieUprawami2Entities1 zarzadzanieUprawami;
        private int maxZysk, maxKoszty, maxPrzychody;
        private int IleLat;
        private List<Data> list = new List<Data>();

        public Prognozuj(UprawyGUI UprawyGUI)
        {
            this.UprawyGUI = UprawyGUI;
            InitializeComponent();
            IleLat = Convert.ToInt32(UprawyGUI.comboBox10.Text);
            zarzadzanieUprawami = new zarzadzanieUprawami2Entities1();
            list = ReceiveDBlist();
            dataGridView1.ColumnCount = 7;
            dataGridView1.Columns[0].Name = "Rok";
            dataGridView1.Columns[1].Name = "Pole";
            dataGridView1.Columns[2].Name = "Gleba";
            dataGridView1.Columns[3].Name = "Roślina";
            dataGridView1.Columns[4].Name = "Przychody";
            dataGridView1.Columns[5].Name = "Koszty";
            dataGridView1.Columns[6].Name = "Zysk";
            dataGridView1.Columns[4].DefaultCellStyle.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            dataGridView1.Columns[5].DefaultCellStyle.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            dataGridView1.Columns[6].DefaultCellStyle.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
            dataGridView1.Columns[4].DefaultCellStyle.BackColor = Color.Cyan;
            dataGridView1.Columns[5].DefaultCellStyle.BackColor = Color.Red;
            dataGridView1.Columns[6].DefaultCellStyle.BackColor = Color.LightGreen;
            dataGridView1.Columns[0].ValueType = Type.GetType("System.Int32");
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            UprawyGUI.progressBar1.Refresh();
            inicjalizujPola();
            wyswietlGleby();
            
        }

        private void Prognozuj_Load(object sender, EventArgs e)
        {
                dataGridView1.ClearSelection();
        }



        private List<Data> ReceiveDBlist()
        {
            List<Data> result = new List<Data>();

            var query = from c in zarzadzanieUprawami.Pola
                        select new
                        {
                            c.idPole,
                            Gleby = c.Gleby.rodzaj,
                        };

            foreach (var c in query)
            {
                int row1 = c.idPole;
                string row2 = c.Gleby;
                result.Add(new Data(row1, row2));
            }

            return result;
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
            int i = 0;
            int liczbawierszy;
            string gleba=null;

            for (i = 0; i < IleLat; i++)
            {
                
                Console.WriteLine("\n             ======= Rok: " + (i + 1) + " ======== \n");
               
                maxZysk = 0;
                maxKoszty = 0;
                maxPrzychody = 0;
                foreach (var p in zarzadzanieUprawami.Pola)
                {
                    String[] tablica = p.liczZyski(zarzadzanieUprawami.Rosliny);

                    for(int a=0; a< list.Count; a++) { 
                        if(Convert.ToInt32(tablica[0]) == list[a].Key)
                        {
                            gleba = list[a].Value;
                        }
                    }

                    Console.WriteLine("Pole: " + tablica[0] + " Gleba: " + gleba + " Roślina: " + tablica[1] +" Przychody: " + tablica[2] + " Koszty: " + tablica[3] + " Zysk: " + tablica[4]);
                    string[] row = new string[] { (i+1).ToString() , tablica[0], gleba , tablica[1] , tablica[2] , tablica[3] , tablica[4] };
                    dataGridView1.Rows.Add(row);
                    maxPrzychody += Convert.ToInt32(tablica[2]);
                    maxKoszty += Convert.ToInt32(tablica[3]);
                    maxZysk += Convert.ToInt32(tablica[4]);
                }

                string[] rowPodsumowanie = new string[] { " Podsumowanie ", " rok: " + (i+1), " ---- ", " --- ",  maxPrzychody.ToString() , maxKoszty.ToString() , maxZysk.ToString() };
                dataGridView1.Rows.Add(rowPodsumowanie);
                liczbawierszy = dataGridView1.RowCount -  1;
                dataGridView1.Rows[liczbawierszy].DefaultCellStyle.Font = new Font("Arial", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
                dataGridView1.Rows[liczbawierszy].DefaultCellStyle.ForeColor = Color.Black;
                dataGridView1.Rows[liczbawierszy].DefaultCellStyle.BackColor = Color.LightBlue;
                UprawyGUI.progressBar1.PerformStep();
            }
        }
    }
}
