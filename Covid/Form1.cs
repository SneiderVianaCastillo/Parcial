using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using Entiry;

namespace Covid
{
    public partial class Form1 : Form
    {
        Service service;
        public Form1()
        {
            service = new Service(ConfigConnection.connectionString, ConfigConnection.ProviderName);
            InitializeComponent();
        }

        private void CargarBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string ruta, mensaje;
                this.openFileDialog1.ShowDialog();
                if (this.openFileDialog1.Equals("") == false)
                {
                   ruta=this.openFileDialog1.FileName;
                   mensaje= service.ConsultarTxt(ruta);
                   MessageBox.Show(mensaje);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("error de la ruta del archivo" + ex.ToString());
            }
        }
    }
}
