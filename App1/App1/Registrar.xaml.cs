using APIEva.Models;
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
                string user = User.Text;
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

        private async Task postAsyncUserConfiguration(string user_name, string pass)
        {
            JObject joUser = new JObject();
            joUser.Add("name_user", user_name);
            joUser.Add("pass", pass); 

            if (await comprovarUsuario(user_name) == false) {
                Uri uriUser = new Uri("https://apieva2022.azurewebsites.net/api/User");
                string jsonUser = JsonConvert.SerializeObject(joUser);
                StringContent contentUser = new StringContent(jsonUser, Encoding.UTF8, "application/json");
                HttpResponseMessage responseUser = await client.PostAsync(uriUser, contentUser);
                Boolean rRegistrar = Convert.ToBoolean(await responseUser.Content.ReadAsStringAsync());

                if (rRegistrar == true)
                {

                    //Mensage de Usuario Creado
                    DisplayAlert("Mensaje", "Usuario Creado con Exito", "Cerrar");

                    JObject joconfiguration = new JObject();
                    joconfiguration.Add("name_user", user_name);
                    joconfiguration.Add("color", "purple");
                    joconfiguration.Add("showEva", "true");
                    joconfiguration.Add("showEmotions", "true");
                    joconfiguration.Add("sound", "true");
                    joconfiguration.Add("volume", "1.0");

                    Uri uriConfiguration = new Uri("https://apieva2022.azurewebsites.net/api/Configuration");

                    string jsonConfiguration = JsonConvert.SerializeObject(joconfiguration);
                    StringContent contentConfiguration = new StringContent(jsonConfiguration, Encoding.UTF8, "application/json");

                    HttpResponseMessage responseConfig = await client.PostAsync(uriConfiguration, contentConfiguration);

                    Boolean rConfig = Convert.ToBoolean(await responseConfig.Content.ReadAsStringAsync());

                    //Leer respueta del post como hacemos en los get, para asi mostrar mensajes de error

                    if (rConfig == false)
                    {
                        DisplayAlert("Mensaje", "Configuracion del usuario no creada", "Cerrar");
                    }

                }
                else if (rRegistrar == false)
                {
                    //Mensage de ERROR
                    DisplayAlert("Mensaje", "Usuario no creado", "Cerrar");
                }
            }else{
                //Mensage de ERROR
                DisplayAlert("Mensaje", "Ya existe un usuario con ese nombre, pruebe con otro", "Cerrar");
            }


        }

        public async Task<bool> comprovarUsuario(String user_name)
        {
            Uri uri = new Uri("https://apieva2022.azurewebsites.net/api/User/" + user_name);

            HttpResponseMessage response = await client.GetAsync(uri);
          
            String rUser = await response.Content.ReadAsStringAsync();

            DisplayAlert("Mensaje", rUser, "Cerrar"); 

            if (!rUser.Equals("null"))
            {
                return true;
            }
            else
            {
                return false;
            }   
        }

        private void limpiar()
        {
            User.Text = "";
            Pass.Text = "";
            Pass2.Text = "";
        }




        private void btnCerrar_Clicked(object sender, EventArgs e)
        {
            //Vamos a la ventana de IniciarSession
            InicioSession ic = new InicioSession();
            this.Navigation.PushModalAsync(ic);
        }
    }
}