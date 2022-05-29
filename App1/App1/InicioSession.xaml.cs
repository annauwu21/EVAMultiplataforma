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
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific.AppCompat;
using Xamarin.Forms.Xaml;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InicioSession : ContentPage
    {
        Cifrado cifrado = new Cifrado();
        HttpClient client;
        public ImageSource ImageSource { get; private set; }
        public InicioSession()
        {
            InitializeComponent();
            client = new HttpClient();
        }

        private void btnRegistrar_Clicked(object sender, EventArgs e)
        {
            //Abrimos la ventana REGISTRAR
            Registrar registrar = new Registrar();
            this.Navigation.PushModalAsync(registrar);
        }

        private async void btnIniciar_Clicked(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(User.Text) || !string.IsNullOrEmpty(Pass.Text)) {
                //Si las casillas estan llenas lo guardamos en variables

                //Lo pasamos todo a LOWER CASE
                var user = User.Text.ToLower();
                var pass = Pass.Text;

                User u = await getAyncUser(user);

                if (u != null)
                {
                    if (u.pass == cifrado.cifrar(pass))
                    {
                        Principal principal = new Principal(u.name_user);
                        this.Navigation.PushModalAsync(principal);
                    }
                    else
                    {
                        DisplayAlert("Error", "Datos erroneos", "Cerrar");
                    }
                }
                else
                {
                    DisplayAlert("Error", "Datos erroneos", "Cerrar");
                }
              

            }
        }

        private async Task<User> getAyncUser(string user_name)
        {
    
            Uri uri = new Uri("https://apieva2022.azurewebsites.net/api/User/"+ user_name);

            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                
                string content = await response.Content.ReadAsStringAsync();
                User u = JsonConvert.DeserializeObject<User>(content);

                return u;
                
            }
            return null;

        }

        private void comprobarbtn()
        {

            if (!string.IsNullOrEmpty(User.Text) || !string.IsNullOrEmpty(Pass.Text))
            {
                btnInicarSessio.IsEnabled = true; //Mostrem el boto iniciar sessio
            }
            else
            {
                btnInicarSessio.IsEnabled = false;
            }
        }

        private void limpiar()
        {
            User.Text = "";
            Pass.Text = "";
        }
    }
}