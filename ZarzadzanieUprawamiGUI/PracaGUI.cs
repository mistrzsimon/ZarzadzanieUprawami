using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZarzadzanieUprawamiGUI
{
    public partial class PracaGUI : Form
    {
        private SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SVI22LM\ZARZADZANIE;initial catalog=zarzadzanieUprawami2;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework");
        private SqlCommand cmd = new SqlCommand();
        public PracaGUI()
        {
            InitializeComponent();
            wyswietlPrace("");
        }

        private void Filtruj_Click(object sender, EventArgs e)
        {

            wyswietlPrace(string.Format("WHERE  PracePolowe.dataWykonanejPracy >= '{0}' AND PracePolowe.dataWykonanejPracy <= '{1}'", dateTimePicker5.Value.ToString(), dateTimePicker6.Value.ToString()));

        }

        private void Resetuj_Click(object sender, EventArgs e)
        {
            wyswietlPrace("");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void wyswietlPrace(string format)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = string.Format(@"select Pracownicy.imię,Pracownicy.nazwisko,PracePolowe.dataWykonanejPracy,RodzajePrac.nazwa 
                                from Pracownicy 
                                Inner join dbo.PracePolowe 
                                On Pracownicy.idPracownik = PracePolowe.idWykonawca 
                                Inner join dbo.RodzajePrac 
                                ON PracePolowe.idWykonawca = RodzajePrac.idPraca 
                                {0}
                                order by dataWykonanejPracy", format);


            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            dataGridView1.DataSource = dt;
        }

    }
}
