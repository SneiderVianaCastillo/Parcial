﻿using System;
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
                string proveedor;
                DateTime fecha;
                string ruta, mensaje;
                this.openFileDialog1.ShowDialog();
                if (this.openFileDialog1.Equals("") == false)
                {
                   ruta=this.openFileDialog1.FileName;
                   proveedor = EntidadCombo.Text;
                   fecha =Convert.ToDateTime( Fechadtp.Text);
                   mensaje= service.ConsultarTxt(ruta,proveedor);
                   int NRegistros = service.ContarRegistros();
                   MessageBox.Show("Numero de registro =>"+NRegistros.ToString()+" "+mensaje);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("error de la ruta del archivo" + ex.ToString());
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
