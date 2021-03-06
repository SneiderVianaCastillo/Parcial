﻿using System;
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
        Persona persona;
        public Service(string connectionString, string providerName)
        {
            conexion = new ConnectionManager(connectionString);
            repository = new Repository(conexion);
            personas = new List<Persona>();
            repositoyTXT = new RepositoyTXT();
        }
        public string Guardar(Persona persona,string proveedor)
        {
            int contadorBuenos=0;
            int contadorGlosas = 0;
            try
            {
                conexion.Open();

                if (repository.BuscarPorIdentificacion(persona.Identificacion) == null && persona.CodigoProveedor == proveedor && (persona.ValorAyuda>=200000) && (persona.ValorAyuda <= 200000))
                {
                        repository.GuardarPersona(persona);
                        contadorBuenos=contadorBuenos+1;

                    return $"Datos GUardados bien"+contadorBuenos.ToString();
                }
                else
                {
                    repository.GuardarGlosas(persona);
                    contadorGlosas = contadorGlosas + 1;
                }
                return $"DATOS CON INCONSITENCIA: "+contadorGlosas.ToString();
            }
            catch (Exception e)
            {
                return $"Error de la Aplicacion: {e.Message}";
            }
            finally { conexion.Close(); }
        }
        string mensaje;
        public string ConsultarTxt(string ruta, string proveedor)
        {
           
            try
            {
                personas= repositoyTXT.Consultar(ruta);
                foreach (var item in personas)
                {
                  
                    mensaje = Guardar(item, proveedor);
                   
                }

                return "Archivo cargado correctamente" + mensaje;
            }
            catch(Exception e)
            {
                return "Archivo cargado incorrectamente"+ e.Message;
            }
           
        }
        public int ContarRegistros()
        {
            return repositoyTXT.ContarRegistros();
        }

    }
}
