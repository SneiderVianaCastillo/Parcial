using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entiry;
namespace DAL
{
   public  class RepositoyTXT
    {
        List<Persona> personas = new List<Persona>();
        Persona persona;
        public Persona Consultar(string ruta)
        {
            personas.Clear();
            FileStream SourceStream = new FileStream(ruta, FileMode.OpenOrCreate);
            StreamReader reader = new StreamReader(SourceStream);
            string linea = string.Empty;
            while ((linea = reader.ReadLine()) != null)
            {
                persona = MapearReporte(linea);
                
                personas.Add(persona);
               
            }
            reader.Close();
            SourceStream.Close();

            return persona;
        }

        public int ContarRegistros()
        {
            return personas.Count();
        }
        
        private Persona MapearReporte(string linea)
        {

            string[] Datos = linea.Split(';');
            Persona persona = new Persona();
            persona.CodigoProveedor = Datos[0];
            persona.Identificacion = Datos[1];
            persona.Nombre = Datos[2];
            persona.Fecha = DateTime.Parse(Datos[3]);
            persona.ValorAyuda = Convert.ToDouble(Datos[4]);

            return persona;
        }


    }
}
