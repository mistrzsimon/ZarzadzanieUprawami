using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZarzadzanieUprawamiGUI
{
    public partial class ObszaryGUI : Form
    {
        private zarzadzanieUprawami2Entities1 zarzadzanieUprawami;
        private List<ListSortDirection> sortowania;
        int columns = 6;
        int columnNr = 0;

        public ObszaryGUI()
        {
            InitializeComponent();
            zarzadzanieUprawami = new zarzadzanieUprawami2Entities1();
            inicjujSortowania();
            ustawBazeDanych();
            aktualizujTabele();
        }


        private void ustawBazeDanych()
        {

            aktualizujTabele();
            dataGridView1.Columns[0].HeaderText = "Nr Obszaru";

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


            var query = from c in zarzadzanieUprawami.Obszary
                        select new
                        {
                            Nr_Obszaru = c.idObszar,
                            Powierzchnia = c.powierzchniaHa,
                            Szerokość = c.szer_geo,
                            Dlugość = c.dl_geo
                        };
            switch (columnNr)
            {
               
                case 0:
                    if (sortowania[columnNr] == ListSortDirection.Ascending)
                        query = query.OrderBy(c => c.Nr_Obszaru);
                    else
                        query = query.OrderByDescending(c => c.Nr_Obszaru);
                    break;
                case 1:
                    if (sortowania[columnNr] == ListSortDirection.Ascending)
                        query = query.OrderBy(c => c.Powierzchnia);
                    else
                        query = query.OrderByDescending(c => c.Powierzchnia);
                    break;
                case 2:
                    if (sortowania[columnNr] == ListSortDirection.Ascending)
                        query = query.OrderBy(c => c.Szerokość);
                    else
                        query = query.OrderByDescending(c => c.Szerokość);
                    break;
                case 3:
                    if (sortowania[columnNr] == ListSortDirection.Ascending)
                        query = query.OrderBy(c => c.Dlugość);
                    else
                        query = query.OrderByDescending(c => c.Dlugość);
                    break;
            }

            var users = query.ToList();
            dataGridView1.DataSource = users;
        }

        private void inicjujSortowania()
        {
            sortowania = new List<ListSortDirection>();
            for (int i = 0; i < columns; i++)
                sortowania.Add(ListSortDirection.Ascending);
        }

        private void ObszaryGUI_Load(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
        }
    }
}
