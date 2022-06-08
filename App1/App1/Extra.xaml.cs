using APIEva.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Plugin.SimpleAudioPlayer;
using Xamarin.Forms;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Extra : ContentPage
    {
        HttpClient client;

        static ISimpleAudioPlayer audio = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
        public Extra()
        {
            InitializeComponent();

            client = new HttpClient();

            var assembly = typeof(App).GetTypeInfo().Assembly;
            Stream audioStream = assembly.GetManifestResourceStream("App1.Sonidos.backgroundMusic.mp3");
            //var audio = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            //audio.Loop = true;
            audio.Load(audioStream);

            if (!audio.IsPlaying)
            {
                audio.Play();
            }
        }

        private async Task<Configuration> getConfigurationsAsync()
        {
            audio.Stop();

            Uri uri = new Uri("https://apieva2022.azurewebsites.net/api/Configuration/" + Detail.user_name);

            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {

                string content = await response.Content.ReadAsStringAsync();
                Configuration c = JsonConvert.DeserializeObject<Configuration>(content);

                return c;

            }
            return null;
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
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

            NavigationPage o = new NavigationPage(new Opciones())
            {
                BarBackgroundColor = c,
            };
            this.Navigation.PushModalAsync(o);
        }
    }
}