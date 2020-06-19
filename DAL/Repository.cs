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
                command.CommandText = "select Identificacion,Nombre from Persona where Identificacion=@Identificacion";
                command.Parameters.AddWithValue("@Identificacion", identificacion);
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

        

        public void GuardarPersona(Persona persona)
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
        public void GuardarGlosas(Persona persona)
        {


            using (var comando = connection.CreateCommand())
            {

                comando.CommandText = "Insert into Glosas  values (@Identificacion,@Proveedor,@Nombre,@Fecha,@ValorAyuda)";
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
