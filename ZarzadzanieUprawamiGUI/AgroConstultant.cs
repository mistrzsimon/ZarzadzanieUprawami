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
    public partial class AgroConstultant : Form
    {
        private UprawyGUI uprawyDialog;
        private PracaGUI pracaDialog;
        public AgroConstultant()
        {
            InitializeComponent();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Uprawy_Click(object sender, EventArgs e)
        {
            uprawyDialog = new UprawyGUI();
            uprawyDialog.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pracaDialog = new PracaGUI();
            pracaDialog.ShowDialog();
        }

        private void Pomoc_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Jesli potrzebujesz pomocy skontatkuj się z administratorem. \nAdres email: pomoc@agroconsultant.com " , "Pomoc");
        }
    }
}
