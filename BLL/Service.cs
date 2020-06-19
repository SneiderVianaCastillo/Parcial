using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entiry;
namespace BLL
{
    public class Service
    {
        private readonly ConnectionManager conexion;
        private readonly Repository repository;
        RepositoyTXT repositoyTXT;
        List<Persona> personas;
        public Service(string connectionString, string providerName)
        {
            conexion = new ConnectionManager(connectionString);
            repository = new Repository(conexion);
            personas = new List<Persona>();
            repositoyTXT = new RepositoyTXT();
        }
        public string Guardar(Persona persona)
        {        
            try
            {
                conexion.Open();

                if (repository.BuscarPorIdentificacion(persona.Identificacion) == null)
                {
                    repository.Guardar(persona);
                    
                    return $"Se guardaron los datos de {persona.Nombre} datos satisfactoriamente" ;
                }
                return $"La persona ya existe";
            }
            catch (Exception e)
            {
                return $"Error de la Aplicacion: {e.Message}";
            }
            finally { conexion.Close(); }
        }
        public string ConsultarTxt(string ruta)
        {
            
            try
            {
                personas= repositoyTXT.Consultar(ruta);
                return "Archivo cargado correctamente";
            }
            catch(Exception e)
            {
                return "Archivo cargado incorrectamente"+ e.Message;
            }
           
        }
    }
}
