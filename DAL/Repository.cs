using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entiry;

namespace DAL
{
    public class Repository
    {
        List<Persona> personas = new List<Persona>();
        
        SqlConnection connection;
        string ruta = @"";

        private readonly SqlConnection _connection;
        public Repository(ConnectionManager connection)
        {
            _connection = connection._conexion;
        }
       

        public List<Persona> Consultar()
        {
            personas.Clear();
            FileStream SourceStream = new FileStream(ruta, FileMode.OpenOrCreate);
            StreamReader reader = new StreamReader(SourceStream);
            string linea = string.Empty;
            while ((linea = reader.ReadLine()) != null)
            {

                Persona persona= MapearReporte(linea);
                personas.Add(persona);
            }
            reader.Close();
            SourceStream.Close();

            return personas;
        }
        private Persona MapearReporte(string linea)
        {

            string[] Datos = linea.Split(';');
            Persona persona = new Persona();
            persona.CodigoProveedor = Datos[0];
            persona.Identificacion = Datos[1];
            persona.Nombre = Datos[2];
            persona.Fecha = DateTime.Parse(Datos[3]);
            persona.ValorAyuda =Convert.ToDouble( Datos[4]);

            return persona;
        }

        public void Guardar(List<Persona> personas)
        {

            foreach (var item in personas)
            {

                using (var comando = connection.CreateCommand())
                {

                    comando.CommandText = "Insert into Persona  values (@Identificacion,@Proveedor_id,@Nombre,@Fecha,@ValorAyuda)";
                    comando.Parameters.AddWithValue("@Identificacion", item.Identificacion);
                    comando.Parameters.AddWithValue("@Proveedor_id", item.CodigoProveedor);
                    comando.Parameters.AddWithValue("@Nombre", item.Nombre);
                    comando.Parameters.AddWithValue("@Fecha", item.Fecha);
                    comando.Parameters.AddWithValue("@ValorAyuda", item.ValorAyuda);

                    comando.ExecuteNonQuery();
                }
            }
        }
}
