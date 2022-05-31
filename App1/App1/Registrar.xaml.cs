using APIEva.Models;
using App1.Logica;
using MySqlConnector;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
        HttpClient client;
        public Registrar()
        {
            InitializeComponent();
            client = new HttpClient ();
        }

        private void btnRegistrar_Clicked(object sender, EventArgs e)
        {
            try
            {
                //Antes de insertar en la BD lo pasamos todo a LOWER CASE
                string user = User.Text.ToLower();
                string pass = Pass.Text;
                string pass2 = Pass2.Text;


                if (!user.Equals("") && !pass.Equals("") && !pass2.Equals(""))
                {
                    if (pass.Equals(pass2))
                    {
                        postAsyncUserConfiguration(user, cifrado.cifrar(pass));
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

        private async Task postAsyncUserConfiguration(string user, string pass)
        {
            JObject joUser = new JObject();
            joUser.Add("name_user", user);
            joUser.Add("pass", pass);

            Uri uriUser = new Uri("https://apieva2022.azurewebsites.net/api/User");

            string jsonUser = JsonConvert.SerializeObject(joUser);
            StringContent contentUser = new StringContent(jsonUser, Encoding.UTF8, "application/json");

            HttpResponseMessage responseUser = null;

            responseUser = await client.PostAsync(uriUser, contentUser);

            if (responseUser.IsSuccessStatusCode)
            {
                JObject joconfiguration = new JObject();
                joconfiguration.Add("name_user", user);
                joconfiguration.Add("color", "purple");
                joconfiguration.Add("showEva", "true");
                joconfiguration.Add("showEmotions", "true");
                joconfiguration.Add("sound", "true");
                joconfiguration.Add("volume", "1.0");

                Uri uriConfiguration = new Uri("https://apieva2022.azurewebsites.net/api/Configuration");

                string jsonConfiguration = JsonConvert.SerializeObject(joconfiguration);
                StringContent contentConfiguration = new StringContent(jsonConfiguration, Encoding.UTF8, "application/json");

                HttpResponseMessage responseConfiguration = null;

                responseConfiguration = await client.PostAsync(uriConfiguration, contentConfiguration);

                if (responseConfiguration.IsSuccessStatusCode)
                {
                    DisplayAlert("Mensaje", "Usuario Creado", "Cerrar");
                }
                    
            }
        }

        private void limpiar()
        {
            User.Text = "";
            Pass.Text = "";
            Pass2.Text = "";
        }

        /*
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
        */

        private void btnCerrar_Clicked(object sender, EventArgs e)
        {
            //Vamos a la ventana de IniciarSession
            InicioSession ic = new InicioSession();
            this.Navigation.PushModalAsync(ic);
        }
    }
}