using MySqlConnector;
using System;
namespace App1
{
    internal class Conexion
    {
        public static MySqlConnection conexion()
        {
            string servidor = "sql11.freesqldatabase.com";
            string bd = "sql11480498";
            string usuario = "sql11480498";
            string password = "caesxtG6i7";

            string cadenaConexion = "Database=" + bd + "; Data Source =" + servidor + "; User Id =" + usuario + ";Password=" + password + "";

            try
            {
                MySqlConnection conexionBD = new MySqlConnection(cadenaConexion);
                return conexionBD;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }
    }
}
