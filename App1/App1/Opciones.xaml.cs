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
    public partial class Opciones : ContentPage
    {
        public Opciones()
        {
            InitializeComponent();

            string u = Detail.user;

            //Buscar por el usuario las configuraciones:
            string select = "SELECT * FROM configurations WHERE user LIKE '" + u + "' LIMIT 1"; //<- Sentencía sql

            MySqlConnection conexionBD = Conexion.conexion(); //<- Crear connexión
            conexionBD.Open(); //<- Abrir la conexión

            MySqlCommand comando = new MySqlCommand(select, conexionBD); //<- Executar sentencía

            MySqlDataReader reader = null; //<- Crear reader
            reader = comando.ExecuteReader(); //<- Ejecutar reader

            //Si reader encuentra algo:
            if (reader.HasRows)
            {
                //Leer lineas:
                while (reader.Read())
                {
                    //Cambiar los colores dependiendo de las configuraciones del usuario:
                    switch (reader.GetString("color"))
                    {
                        case "purple":
                            Bubble2.BackgroundColor = Color.FromHex("#d1cedd");
                            Bubble1.BackgroundColor = Color.FromHex("#6656bc");
                            break;
                        case "green":
                            Bubble2.BackgroundColor = Color.FromHex("#d8ed96");
                            Bubble1.BackgroundColor = Color.FromHex("#bae860");
                            break;
                        case "white":
                            Bubble1.BackgroundColor = Color.FromHex("#e5c6db");
                            Bubble2.BackgroundColor = Color.FromHex("#d6d3d6");
                            break;
                        case "black":
                            Bubble1.BackgroundColor = Color.FromHex("#30383a");
                            Bubble2.BackgroundColor = Color.FromHex("#666d70");
                            break;
                        case "red":
                            Bubble2.BackgroundColor = Color.FromHex("#f9b2b7");
                            Bubble1.BackgroundColor = Color.FromHex("#f43f4f");
                            break;
                        case "blue":
                            Bubble2.BackgroundColor = Color.FromHex("#c4d8e2");
                            Bubble1.BackgroundColor = Color.FromHex("#75b2dd");
                            break;
                    }

                    //Cambiar las configuraciones activadas:
                    if (reader.GetString("showEVA")=="true")
                    {
                        showEva.IsToggled = true;
                    }
                    else
                    {
                        showEva.IsToggled = false;
                    }

                    if (reader.GetString("showEmotions") == "true")
                    {
                        emotionsEva.IsToggled = true;
                    }
                    else
                    {
                        emotionsEva.IsToggled = false;
                    }
                    if (reader.GetString("sound") == "true")
                    {
                        sonidos.IsToggled = true;
                    }
                    else
                    {
                        sonidos.IsToggled = false;
                    }
                }
            }

            conexionBD.Close(); //<- Cerrar la conexión
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Bubble2.BackgroundColor = Color.FromHex("#d8ed96");
            Bubble1.BackgroundColor = Color.FromHex("#bae860");

            Bubble1.BorderColor = Color.FromHex("");
            Bubble2.BorderColor = Color.FromHex("");

            string update = "UPDATE configurations SET color = 'green' WHERE user LIKE '" + Detail.user + "'";
            MySqlConnection conexionBD = Conexion.conexion();
            MySqlCommand comando = new MySqlCommand(update, conexionBD);
            conexionBD.Open();
            comando.ExecuteNonQuery();
            conexionBD.Close();
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            Bubble1.BackgroundColor = Color.FromHex("#e5c6db");
            Bubble2.BackgroundColor = Color.FromHex("#d6d3d6");

            string update = "UPDATE configurations SET color = 'white' WHERE user LIKE '" + Detail.user + "'";
            MySqlConnection conexionBD = Conexion.conexion();
            MySqlCommand comando = new MySqlCommand(update, conexionBD);
            conexionBD.Open();
            comando.ExecuteNonQuery();
            conexionBD.Close();
        }

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            Bubble1.BackgroundColor = Color.FromHex("#30383a");
            Bubble2.BackgroundColor = Color.FromHex("#666d70");

            string update = "UPDATE configurations SET color = 'black' WHERE user LIKE '" + Detail.user + "'";
            MySqlConnection conexionBD = Conexion.conexion();
            MySqlCommand comando = new MySqlCommand(update, conexionBD);
            conexionBD.Open();
            comando.ExecuteNonQuery();
            conexionBD.Close();
        }

        private void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            Bubble2.BackgroundColor = Color.FromHex("#f9b2b7");
            Bubble1.BackgroundColor = Color.FromHex("#f43f4f");

            Bubble1.BorderColor = Color.FromHex("");
            Bubble2.BorderColor = Color.FromHex("");

            string update = "UPDATE configurations SET color = 'red' WHERE user LIKE '" + Detail.user + "'";
            MySqlConnection conexionBD = Conexion.conexion();
            MySqlCommand comando = new MySqlCommand(update, conexionBD);
            conexionBD.Open();
            comando.ExecuteNonQuery();
            conexionBD.Close();
        }

        private void TapGestureRecognizer_Tapped_4(object sender, EventArgs e)
        {
            Bubble2.BackgroundColor = Color.FromHex("#c4d8e2");
            Bubble1.BackgroundColor = Color.FromHex("#75b2dd");

            Bubble1.BorderColor = Color.FromHex("");
            Bubble2.BorderColor = Color.FromHex("");

            string update = "UPDATE configurations SET color = 'blue' WHERE user LIKE '" + Detail.user + "'";
            MySqlConnection conexionBD = Conexion.conexion();
            MySqlCommand comando = new MySqlCommand(update, conexionBD);
            conexionBD.Open();
            comando.ExecuteNonQuery();
            conexionBD.Close();
        }

        private void TapGestureRecognizer_Tapped_5(object sender, EventArgs e)
        {
            Bubble2.BackgroundColor = Color.FromHex("#d1cedd");
            Bubble1.BackgroundColor = Color.FromHex("#6656bc");

            Bubble1.BorderColor = Color.FromHex("");
            Bubble2.BorderColor = Color.FromHex("");

            string update = "UPDATE configurations SET color = 'purple' WHERE user LIKE '" + Detail.user + "'";
            MySqlConnection conexionBD = Conexion.conexion();
            MySqlCommand comando = new MySqlCommand(update, conexionBD);
            conexionBD.Open();
            comando.ExecuteNonQuery();
            conexionBD.Close();
        }

        private void TapGestureRecognizer_Tapped_6(object sender, EventArgs e)
        {
            string show = showEva.IsToggled.ToString();
            show = show.ToLower();

            string emotions = emotionsEva.IsToggled.ToString();
            emotions = emotions.ToLower();

            string sounds = sonidos.IsToggled.ToString();
            sounds = emotions.ToLower();

            string update1 = "UPDATE configurations SET showEVA = '" + show + "' WHERE user LIKE '" + Detail.user + "'";
            string update2 = "UPDATE configurations SET showEmotions = '" + emotions + "' WHERE user LIKE '" + Detail.user + "'";
            string update3 = "UPDATE configurations SET showEmotions = '" + sounds + "' WHERE user LIKE '" + Detail.user + "'";

            MySqlConnection conexionBD = Conexion.conexion();

            MySqlCommand comando1 = new MySqlCommand(update1, conexionBD);
            MySqlCommand comando2 = new MySqlCommand(update2, conexionBD);
            MySqlCommand comando3 = new MySqlCommand(update3, conexionBD);

            conexionBD.Open();

            comando1.ExecuteNonQuery();
            comando2.ExecuteNonQuery();
            comando3.ExecuteNonQuery();

            conexionBD.Close();

            Principal principal = new Principal(Detail.user);
            this.Navigation.PushModalAsync(principal);
        }
    }
}