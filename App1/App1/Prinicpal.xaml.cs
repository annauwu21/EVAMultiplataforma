using APIEva.Models;
using IBM.Cloud.SDK.Core.Authentication.Iam;
using IBM.Cloud.SDK.Core.Http.Exceptions;
using IBM.Watson.Assistant.v2;
using IBM.Watson.Assistant.v2.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1
{
    public partial class Principal : MasterDetailPage
    {
        HttpClient client;
        public Color barcolor { get; private set; }

        public Principal(string user, string tipo)
        {
            InitializeComponent();

            client = new HttpClient();
            string user_name = user;

            loadDetailPageAsync(user_name, tipo);

            loadPage(user_name, tipo);



        }
        private void loadPage(string user_name, string tipo){
            //Metemos el menu desplegable lateral
            this.Master = new Master();

            //Metemos la pagina principal
            this.Detail = new NavigationPage(new Detail(user_name, tipo))
            {
                BarBackgroundColor = barcolor,
                BarTextColor = Color.White,
            };
        }

        private async Task<Configuration> getConfigurationsAsync(string user_name)
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

        private async Task loadDetailPageAsync(string user_name, string tipo)
        {
            Configuration c = await getConfigurationsAsync(user_name);

            switch (c.color)
            {
                case "purple":
                    barcolor = Color.FromHex("#6656bc");
                    break;
                case "green":
                    barcolor = Color.FromHex("#bae860");
                    break;
                case "white":
                    barcolor = Color.FromHex("#e5c6db");
                    break;
                case "black":
                    barcolor = Color.FromHex("#30383a");
                    break;
                case "red":
                    barcolor = Color.FromHex("#f43f4f");
                    break;
                case "blue":
                    barcolor = Color.FromHex("#75b2dd");
                    break;
            }
            loadPage(user_name, tipo);

        }
    }
}
