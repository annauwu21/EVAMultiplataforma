using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registrar : ContentPage
    {
        Cifrado cifrado = new Cifrado();
        public Registrar()
        {
            InitializeComponent();
        }


        private void btnRegistrar_Clicked(object sender, EventArgs e)
        {
            try
            {
                //Antes de insertar en la BD lo pasamos todo a LOWER CASE
                string user = User.Text.ToLower();
                string pass = Pass.Text.ToLower();
                string pass2 = Pass2.Text.ToLower();


                if (!user.Equals("") && !pass.Equals("") && !pass2.Equals(""))
                {
                    if (pass.Equals(pass2))
                    {
                        string sql = "INSERT INTO users (user, pass) VALUES ('" + user + "', '" + cifrado.cifrar(pass) + "')";
                        string sql2 = "INSERT INTO configurations (user) VALUES ('" + user + "')";

                        MySqlConnection conexionBD = Conexion.conexion();
                        conexionBD.Open();

                        try
                        {
                            //Si el usuario se añade, mostramos mensaje
                            MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                            comando.ExecuteNonQuery();

                            MySqlCommand comando2 = new MySqlCommand(sql2, conexionBD);
                            comando2.ExecuteNonQuery();

                            DisplayAlert("Alerta", "Usuario añadido!!!!", "Cerrar");
                            limpiar();
                        }
                        catch (MySqlException ex)
                        {
                            DisplayAlert("Error", "Error al guardar el usuario: " + ex.Message, "Cerrar");
                        }
                        finally
                        {
                            conexionBD.Clone();
                        }
                    }
                    else
                    {
                        DisplayAlert("Error", "Las dos contraseñas tiene que ser iguales", "Cerrar");
                      
                    }
                }
                else
                {
                    DisplayAlert("Error", "Tienes que rellenar todos los datos", "Cerrar");
                }
            }
            catch (MySqlException fex)
            {
                DisplayAlert("Error", "Problemas con la conexión: " +fex.Message, "Cerrar");
            }
        }

        private void limpiar()
        {
            User.Text = "";
            Pass.Text = "";
            Pass2.Text = "";
        }

        public Boolean comprobarUsuari(String nom)
        {
            Boolean ok = false;
            MySqlDataReader reader = null;

            if (!nom.Equals(""))
            {
                string select = "SELECT * FROM usuaris WHERE nom LIKE '" + nom + "';";
                MySqlConnection conexionBD = Conexion.conexion();
                conexionBD.Open();

                try
                {
                    MySqlCommand comando = new MySqlCommand(select, conexionBD);
                    reader = comando.ExecuteReader();
                    if (reader.HasRows) //Si reader encuentra algo
                    {
                        ok = true;
                    }
                    else
                    {
                        DisplayAlert("Error", "No s'ha trobat cap usuari amb el nom: " + nom, "Cerrar");
                        ok = false;
                    }
                }
                catch (MySqlException ex)
                {

                    DisplayAlert("Error", "No s'ha pogut cercar l'usuari " + ex.Message, "Cerrar");

                }
                finally
                {
                    conexionBD.Close();
                }
            }
            return ok;
        }

        private void btnCerrar_Clicked(object sender, EventArgs e)
        {
            //Vamos a la ventana de IniciarSession
            InicioSession ic = new InicioSession();
            this.Navigation.PushModalAsync(ic);
        }
    }
}