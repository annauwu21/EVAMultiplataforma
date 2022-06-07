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
    public partial class Opciones : ContentPage
    {
        HttpClient client;
        public static string user_name { get; private set; } //<- Guardar el usuario conectado para aplicar las configuraciones
        public static string color { get; private set; } //<- Guardar el usuario conectado para aplicar las configuraciones

        public Opciones()
        {
            InitializeComponent();
            client = new HttpClient();

            user_name = Detail.user_name;

            loadConfigurationsAsync();

        }

        private async Task putConfigurationAsync(string color, string showEva, string showEmotions, string sound, string volume)
        {
            Configuration c = new Configuration(user_name, color, showEva, showEmotions, sound, volume);

            Uri uri = new Uri("https://apieva2022.azurewebsites.net/api/Configuration");

            string json = JsonConvert.SerializeObject(c);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;

            response = await client.PutAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                response = await client.PutAsync(uri, content);
            }
            else
            {
                DisplayAlert("Error", "No se ha podido guardar la configuración", "Cerrar");
            }

        }

        private async Task deleteHistoryAsync()
        {
            Uri uri = new Uri("https://apieva2022.azurewebsites.net/api/History/" + Detail.user_name);
            HttpResponseMessage response = await client.DeleteAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                DisplayAlert("Error", "Hisotrial borrado correctamente.", "Cerrar");
            }
        }

        private async Task deleteUserAsync()
        {
            Uri uri = new Uri("https://apieva2022.azurewebsites.net/api/User/" + Detail.user_name);
            HttpResponseMessage response = await client.DeleteAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                DisplayAlert("Error", "Usuario borrado correctamente.", "Cerrar");
            }
        }

        private async Task<Configuration> getConfigurationsAsync()
        {

            Uri uri = new Uri("https://apieva2022.azurewebsites.net/api/Configuration/" + user_name);

            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {

                string content = await response.Content.ReadAsStringAsync();
                Configuration c = JsonConvert.DeserializeObject<Configuration>(content);

                return c;

            }
            return null;
        }

        private async Task loadConfigurationsAsync()
        {
            Configuration c = await getConfigurationsAsync();

            switch (c.color)
            {
                case "purple":
                    Bubble2.BackgroundColor = Color.FromHex("#d1cedd");
                    Bubble1.BackgroundColor = Color.FromHex("#6656bc");

                    volume.MinimumTrackColor = Color.FromHex("#6656bc"); //dark
                    volume.MaximumTrackColor = Color.FromHex("#d1cedd"); //light
                    volume.ThumbColor = Color.FromHex("#a5a0d6");

                    showEva.OnColor = Color.FromHex("#a5a0d6");
                    emotionsEva.OnColor = Color.FromHex("#a5a0d6");
                    sonidos.OnColor = Color.FromHex("#a5a0d6");

                    break;
                case "green":
                    Bubble2.BackgroundColor = Color.FromHex("#d8ed96");
                    Bubble1.BackgroundColor = Color.FromHex("#bae860");


                    volume.MinimumTrackColor = Color.FromHex("#bae860"); //dark
                    volume.MaximumTrackColor = Color.FromHex("#d8ed96"); //light
                    volume.ThumbColor = Color.FromHex("#ceea82");

                    showEva.OnColor = Color.FromHex("#ceea82");
                    emotionsEva.OnColor = Color.FromHex("#ceea82");
                    sonidos.OnColor = Color.FromHex("#ceea82");

                    break;
                case "white":
                    Bubble1.BackgroundColor = Color.FromHex("#e5c6db");
                    Bubble2.BackgroundColor = Color.FromHex("#d6d3d6");

                    volume.MinimumTrackColor = Color.FromHex("#e5c6db"); //dark
                    volume.MaximumTrackColor = Color.FromHex("#d6d3d6"); //light
                    volume.ThumbColor = Color.FromHex("#dbd3d3");

                    showEva.OnColor = Color.FromHex("#dbd3d3");
                    emotionsEva.OnColor = Color.FromHex("#dbd3d3");
                    sonidos.OnColor = Color.FromHex("#dbd3d3");

                    break;
                case "black":
                    Bubble1.BackgroundColor = Color.FromHex("#30383a");
                    Bubble2.BackgroundColor = Color.FromHex("#666d70");

                    volume.MinimumTrackColor = Color.FromHex("#30383a"); //dark
                    volume.MaximumTrackColor = Color.FromHex("#666d70"); //light
                    volume.ThumbColor = Color.FromHex("#444f51");

                    showEva.OnColor = Color.FromHex("#444f51");
                    emotionsEva.OnColor = Color.FromHex("#444f51");
                    sonidos.OnColor = Color.FromHex("#444f51");

                    break;
                case "red":
                    Bubble2.BackgroundColor = Color.FromHex("#f9b2b7");
                    Bubble1.BackgroundColor = Color.FromHex("#f43f4f");

                    volume.MinimumTrackColor = Color.FromHex("#f43f4f"); //dark
                    volume.MaximumTrackColor = Color.FromHex("#f9b2b7"); //light
                    volume.ThumbColor = Color.FromHex("#fc6675");

                    showEva.OnColor = Color.FromHex("#fc6675");
                    emotionsEva.OnColor = Color.FromHex("#fc6675");
                    sonidos.OnColor = Color.FromHex("#fc6675");

                    break;
                case "blue":
                    Bubble2.BackgroundColor = Color.FromHex("#c4d8e2");
                    Bubble1.BackgroundColor = Color.FromHex("#75b2dd");

                    volume.MinimumTrackColor = Color.FromHex("#75b2dd"); //dark
                    volume.MaximumTrackColor = Color.FromHex("#c4d8e2"); //light
                    volume.ThumbColor = Color.FromHex("#a8cee2");

                    showEva.OnColor = Color.FromHex("#a8cee2");
                    emotionsEva.OnColor = Color.FromHex("#a8cee2");
                    sonidos.OnColor = Color.FromHex("#a8cee2");
                    break;
            }

            if (c.showEva == "true")
            {
                showEva.IsToggled = true;
            }
            else
            {
                showEva.IsToggled = false;
            }

            if (c.showEmotions == "true")
            {
                emotionsEva.IsToggled = true;
            }
            else
            {
                emotionsEva.IsToggled = false;
            }
            if (c.sound == "true")
            {
                sonidos.IsToggled = true;
            }
            else
            {
                sonidos.IsToggled = false;
            }

            volume.Value = float.Parse(c.volume);
        }


        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Bubble2.BackgroundColor = Color.FromHex("#d8ed96");
            Bubble1.BackgroundColor = Color.FromHex("#bae860");

            Bubble1.BorderColor = Color.FromHex("");
            Bubble2.BorderColor = Color.FromHex("");

            volume.MinimumTrackColor = Color.FromHex("#bae860"); //dark
            volume.MaximumTrackColor = Color.FromHex("#d8ed96"); //light
            volume.ThumbColor = Color.FromHex("#ceea82");

            showEva.OnColor = Color.FromHex("#ceea82");
            emotionsEva.OnColor = Color.FromHex("#ceea82");
            sonidos.OnColor = Color.FromHex("#ceea82");

            color = "green";
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            Bubble1.BackgroundColor = Color.FromHex("#e5c6db");
            Bubble2.BackgroundColor = Color.FromHex("#d6d3d6");

            Bubble1.BorderColor = Color.FromHex("");
            Bubble2.BorderColor = Color.FromHex("");

            volume.MinimumTrackColor = Color.FromHex("#e5c6db"); //dark
            volume.MaximumTrackColor = Color.FromHex("#d6d3d6"); //light
            volume.ThumbColor = Color.FromHex("#dbd3d3");

            showEva.OnColor = Color.FromHex("#dbd3d3");
            emotionsEva.OnColor = Color.FromHex("#dbd3d3");
            sonidos.OnColor = Color.FromHex("#dbd3d3");

            color = "white";
        }

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            Bubble1.BackgroundColor = Color.FromHex("#30383a");
            Bubble2.BackgroundColor = Color.FromHex("#666d70");

            Bubble1.BorderColor = Color.FromHex("");
            Bubble2.BorderColor = Color.FromHex("");

            volume.MinimumTrackColor = Color.FromHex("#30383a"); //dark
            volume.MaximumTrackColor = Color.FromHex("#666d70"); //light
            volume.ThumbColor = Color.FromHex("#444f51");

            showEva.OnColor = Color.FromHex("#444f51");
            emotionsEva.OnColor = Color.FromHex("#444f51");
            sonidos.OnColor = Color.FromHex("#444f51");

            color = "black";
        }

        private void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            Bubble2.BackgroundColor = Color.FromHex("#f9b2b7");
            Bubble1.BackgroundColor = Color.FromHex("#f43f4f");

            Bubble1.BorderColor = Color.FromHex("");
            Bubble2.BorderColor = Color.FromHex("");

            volume.MinimumTrackColor = Color.FromHex("#f43f4f"); //dark
            volume.MaximumTrackColor = Color.FromHex("#f9b2b7"); //light
            volume.ThumbColor = Color.FromHex("#fc6675");

            showEva.OnColor = Color.FromHex("#fc6675");
            emotionsEva.OnColor = Color.FromHex("#fc6675");
            sonidos.OnColor = Color.FromHex("#fc6675");

            color = "red";
        }

        private void TapGestureRecognizer_Tapped_4(object sender, EventArgs e)
        {
            Bubble2.BackgroundColor = Color.FromHex("#c4d8e2");
            Bubble1.BackgroundColor = Color.FromHex("#75b2dd");

            Bubble1.BorderColor = Color.FromHex("");
            Bubble2.BorderColor = Color.FromHex("");

            volume.MinimumTrackColor = Color.FromHex("#75b2dd"); //dark
            volume.MaximumTrackColor = Color.FromHex("#c4d8e2"); //light
            volume.ThumbColor = Color.FromHex("#a8cee2");

            showEva.OnColor = Color.FromHex("#a8cee2");
            emotionsEva.OnColor = Color.FromHex("#a8cee2");
            sonidos.OnColor = Color.FromHex("#a8cee2");

            color = "blue";
        }

        private void TapGestureRecognizer_Tapped_5(object sender, EventArgs e)
        {
            Bubble2.BackgroundColor = Color.FromHex("#d1cedd");
            Bubble1.BackgroundColor = Color.FromHex("#6656bc");

            Bubble1.BorderColor = Color.FromHex("");
            Bubble2.BorderColor = Color.FromHex("");

            volume.MinimumTrackColor = Color.FromHex("#6656bc"); //dark
            volume.MaximumTrackColor = Color.FromHex("#d1cedd"); //light
            volume.ThumbColor = Color.FromHex("#a5a0d6");

            showEva.OnColor = Color.FromHex("#a5a0d6");
            emotionsEva.OnColor = Color.FromHex("#a5a0d6");
            sonidos.OnColor = Color.FromHex("#a5a0d6");

            color = "purple";
        }

        private void TapGestureRecognizer_Tapped_6(object sender, EventArgs e)
        {
            Detail.historial.Clear();
            Principal principal = new Principal(Detail.user_name, "Opciones");
            this.Navigation.PushModalAsync(principal);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            Color c = new Color();
            Configuration config = await getConfigurationsAsync();

            switch (config.color)
            {
                case "purple":
                    c = Color.FromHex("#6656bc");
                    break;
                case "green":
                    c = Color.FromHex("#bae860");
                    break;
                case "white":
                    c = Color.FromHex("#e5c6db");
                    break;
                case "black":
                    c = Color.FromHex("#30383a");
                    break;
                case "red":
                    c = Color.FromHex("#f43f4f");
                    break;
                case "blue":
                    c = Color.FromHex("#75b2dd");
                    break;
            }

            NavigationPage extra = new NavigationPage(new Extra())
            {
                BarBackgroundColor = c,
            };
            this.Navigation.PushModalAsync(extra);
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            deleteHistoryAsync();

        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            InicioSession s = new InicioSession();
            this.Navigation.PushModalAsync(s);
        }

        private void Button_Clicked_3(object sender, EventArgs e)
        {
            Detail.historial.Clear();
            deleteUserAsync();
            InicioSession s = new InicioSession();
            this.Navigation.PushModalAsync(s);
        }

        private void Button_Clicked_4(object sender, EventArgs e)
        {
            string show = showEva.IsToggled.ToString().ToLower();
            show = show.ToLower();

            string emotions = emotionsEva.IsToggled.ToString();
            emotions = emotions.ToLower();

            string sounds = sonidos.IsToggled.ToString();
            sounds = sounds.ToLower();

            string volum = volume.Value.ToString();

            putConfigurationAsync(color, show, emotions, sounds, volum);
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            Principal p = new Principal(Detail.user_name, "Opciones");
            this.Navigation.PushModalAsync(p);
        }

        private async void Button_Clicked_5(object sender, EventArgs e)
        {
            Color c = new Color();
            Configuration config = await getConfigurationsAsync();

            switch (config.color)
            {
                case "purple":
                    c = Color.FromHex("#6656bc");
                    break;
                case "green":
                    c = Color.FromHex("#bae860");
                    break;
                case "white":
                    c = Color.FromHex("#e5c6db");
                    break;
                case "black":
                    c = Color.FromHex("#30383a");
                    break;
                case "red":
                    c = Color.FromHex("#f43f4f");
                    break;
                case "blue":
                    c = Color.FromHex("#75b2dd");
                    break;
            }

            NavigationPage tz = new NavigationPage(new TwilightZone(c))
            {
                BarBackgroundColor = c,
            };
            this.Navigation.PushModalAsync(tz);

        }
    }
}