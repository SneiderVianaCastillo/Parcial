using System;
using System.Collections.Generic;
using System.Data;
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


        private readonly SqlConnection _connection;
        public Repository(ConnectionManager connection)
        {
            _connection = connection._conexion;
        }
        public Persona BuscarPorIdentificacion(string identificacion)
        {
            SqlDataReader dataReader;
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "select Identificacion,Nombre from Persona where Identificacion=:Identificacion";
                command.Parameters.Add("Identificacion", SqlDbType.NVarChar).Value =identificacion;
                dataReader = command.ExecuteReader();
                dataReader.Read();
                Persona persona =    DataReaderMapearPersona(dataReader);
                return persona;
            }
        }
        private Persona DataReaderMapearPersona(SqlDataReader dataReader)
        {
            if (!dataReader.HasRows) return null;
            Persona persona = new Persona();
            persona.Identificacion = dataReader.GetString(0);
            persona.CodigoProveedor = dataReader.GetString(1);
            persona.Nombre = dataReader.GetString(2);
            persona.Fecha = Convert.ToDateTime( dataReader.GetString(3));
            persona.ValorAyuda = Convert.ToDouble( dataReader.GetString(4));
            

            return persona;

        }

        public List<Persona> Consultar(string ruta)
        {
            personas.Clear();
            FileStream SourceStream = new FileStream(ruta, FileMode.OpenOrCreate);
            StreamReader reader = new StreamReader(SourceStream);
            string linea = string.Empty;
            while ((linea = reader.ReadLine()) != null)
            {

                Persona persona = MapearReporte(linea);
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
            persona.ValorAyuda = Convert.ToDouble(Datos[4]);

            return persona;
        }

        public void Guardar(Persona persona)
        {

            
                using (var comando = connection.CreateCommand())
                {

                    comando.CommandText = "Insert into Persona  values (@Identificacion,@Proveedor_id,@Nombre,@Fecha,@ValorAyuda)";
                    comando.Parameters.AddWithValue("@Identificacion", persona.Identificacion);
                    comando.Parameters.AddWithValue("@Proveedor_id", persona.CodigoProveedor);
                    comando.Parameters.AddWithValue("@Nombre", persona.Nombre);
                    comando.Parameters.AddWithValue("@Fecha", persona.Fecha);
                    comando.Parameters.AddWithValue("@ValorAyuda", persona.ValorAyuda);

                    comando.ExecuteNonQuery();
                }
            
        }
    }
}
