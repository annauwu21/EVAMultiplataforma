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

                            volume.MinimumTrackColor = Color.FromHex("#6656bc"); //dark
                            volume.MaximumTrackColor = Color.FromHex("#d1cedd"); //light
                            volume.ThumbColor = Color.FromHex("#a5a0d6");

                            showEva.OnColor = Color.FromHex("#a5a0d6");
                            emotionsEva.OnColor = Color.FromHex("#a5a0d6");
                            daltonico.OnColor = Color.FromHex("#a5a0d6");
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
                            daltonico.OnColor = Color.FromHex("#ceea82");
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
                            daltonico.OnColor = Color.FromHex("#dbd3d3");
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
                            daltonico.OnColor = Color.FromHex("#444f51");
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
                            daltonico.OnColor = Color.FromHex("#fc6675");
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
                            daltonico.OnColor = Color.FromHex("#a8cee2");
                            sonidos.OnColor = Color.FromHex("#a8cee2");
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

                    volume.Value = float.Parse(reader.GetString("volume"));
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

            volume.MinimumTrackColor = Color.FromHex("#bae860"); //dark
            volume.MaximumTrackColor = Color.FromHex("#d8ed96"); //light
            volume.ThumbColor = Color.FromHex("#ceea82");

            showEva.OnColor = Color.FromHex("#ceea82");
            emotionsEva.OnColor = Color.FromHex("#ceea82");
            daltonico.OnColor = Color.FromHex("#ceea82");
            sonidos.OnColor = Color.FromHex("#ceea82");

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

            Bubble1.BorderColor = Color.FromHex("");
            Bubble2.BorderColor = Color.FromHex("");

            volume.MinimumTrackColor = Color.FromHex("#e5c6db"); //dark
            volume.MaximumTrackColor = Color.FromHex("#d6d3d6"); //light
            volume.ThumbColor = Color.FromHex("#dbd3d3");

            showEva.OnColor = Color.FromHex("#dbd3d3");
            emotionsEva.OnColor = Color.FromHex("#dbd3d3");
            daltonico.OnColor = Color.FromHex("#dbd3d3");
            sonidos.OnColor = Color.FromHex("#dbd3d3");

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

            Bubble1.BorderColor = Color.FromHex("");
            Bubble2.BorderColor = Color.FromHex("");

            volume.MinimumTrackColor = Color.FromHex("#30383a"); //dark
            volume.MaximumTrackColor = Color.FromHex("#666d70"); //light
            volume.ThumbColor = Color.FromHex("#444f51");

            showEva.OnColor = Color.FromHex("#444f51");
            emotionsEva.OnColor = Color.FromHex("#444f51");
            daltonico.OnColor = Color.FromHex("#444f51");
            sonidos.OnColor = Color.FromHex("#444f51");

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

            volume.MinimumTrackColor = Color.FromHex("#f43f4f"); //dark
            volume.MaximumTrackColor = Color.FromHex("#f9b2b7"); //light
            volume.ThumbColor = Color.FromHex("#fc6675");

            showEva.OnColor = Color.FromHex("#fc6675");
            emotionsEva.OnColor = Color.FromHex("#fc6675");
            daltonico.OnColor = Color.FromHex("#fc6675");
            sonidos.OnColor = Color.FromHex("#fc6675");

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

            volume.MinimumTrackColor = Color.FromHex("#75b2dd"); //dark
            volume.MaximumTrackColor = Color.FromHex("#c4d8e2"); //light
            volume.ThumbColor = Color.FromHex("#a8cee2");

            showEva.OnColor = Color.FromHex("#a8cee2");
            emotionsEva.OnColor = Color.FromHex("#a8cee2");
            daltonico.OnColor = Color.FromHex("#a8cee2");
            sonidos.OnColor = Color.FromHex("#a8cee2");

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

            volume.MinimumTrackColor = Color.FromHex("#6656bc"); //dark
            volume.MaximumTrackColor = Color.FromHex("#d1cedd"); //light
            volume.ThumbColor = Color.FromHex("#a5a0d6");

            showEva.OnColor = Color.FromHex("#a5a0d6");
            emotionsEva.OnColor = Color.FromHex("#a5a0d6");
            daltonico.OnColor = Color.FromHex("#a5a0d6");
            sonidos.OnColor = Color.FromHex("#a5a0d6");

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
            sounds = sounds.ToLower();

            string volum = volume.Value.ToString();

            string update1 = "UPDATE configurations SET showEVA = '" + show + "' WHERE user LIKE '" + Detail.user + "'";
            string update2 = "UPDATE configurations SET showEmotions = '" + emotions + "' WHERE user LIKE '" + Detail.user + "'";
            string update3 = "UPDATE configurations SET sound = '" + sounds + "' WHERE user LIKE '" + Detail.user + "'";
            string update4 = "UPDATE configurations SET volume = '" + volum + "' WHERE user LIKE '" + Detail.user + "'";

            MySqlConnection conexionBD = Conexion.conexion();

            MySqlCommand comando1 = new MySqlCommand(update1, conexionBD);
            MySqlCommand comando2 = new MySqlCommand(update2, conexionBD);
            MySqlCommand comando3 = new MySqlCommand(update3, conexionBD);
            MySqlCommand comando4 = new MySqlCommand(update4, conexionBD);

            conexionBD.Open();

            comando1.ExecuteNonQuery();
            comando2.ExecuteNonQuery();
            comando3.ExecuteNonQuery();
            comando4.ExecuteNonQuery();

            conexionBD.Close();

            Principal principal = new Principal(Detail.user);
            this.Navigation.PushModalAsync(principal);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Extra ex = new Extra();
            this.Navigation.PushModalAsync(ex);
        }
    }
}