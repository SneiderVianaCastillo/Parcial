using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Covid
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void CargarBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string ruta;
                this.openFileDialog1.ShowDialog();
                if (this.openFileDialog1.Equals("") == false)
                {
                   ruta=  this.openFileDialog1.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("error de la ruta del archivo" + ex.ToString());
            }
        }
    }
}
