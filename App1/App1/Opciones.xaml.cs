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
            string select = "SELECT * FROM users WHERE user LIKE '" + u + "' LIMIT 1"; //<- Sentencía sql

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
                            Bubble1.BackgroundColor = Color.Purple;
                            Bubble1.BorderColor = Color.MediumPurple;
                            break;
                        case "green":
                            Bubble1.BackgroundColor = Color.LightGreen;
                            Bubble1.BorderColor = Color.Green;
                            break;
                        case "white":
                            Bubble1.BackgroundColor = Color.White;
                            Bubble1.BorderColor = Color.Black;
                            break;
                        case "black":
                            Bubble1.BackgroundColor = Color.Black;
                            Bubble1.BorderColor = Color.Black;
                            break;
                        case "red":
                            Bubble1.BackgroundColor = Color.Red;
                            Bubble1.BorderColor = Color.PaleVioletRed;
                            break;
                        case "blue":
                            Bubble1.BackgroundColor = Color.Blue;
                            Bubble1.BorderColor = Color.LightBlue;
                            break;
                    }

                    if (reader.GetString("showEVA")=="true")
                    {
                        showEva.IsToggled = true;
                    }
                    else
                    {
                        showEva.IsToggled = false;
                    }
                }
            }

            conexionBD.Close(); //<- Cerrar la conexión
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Bubble1.BorderColor = Color.Green;
            Bubble1.BackgroundColor = Color.LightGreen;

            string update = "UPDATE users SET color = 'green' WHERE user LIKE '" + Detail.user + "'";
            MySqlConnection conexionBD = Conexion.conexion();
            MySqlCommand comando = new MySqlCommand(update, conexionBD);
            conexionBD.Open();
            comando.ExecuteNonQuery();
            conexionBD.Close();
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            Bubble1.BorderColor = Color.Black;
            Bubble1.BackgroundColor = Color.White;

            string update = "UPDATE users SET color = 'white' WHERE user LIKE '" + Detail.user + "'";
            MySqlConnection conexionBD = Conexion.conexion();
            MySqlCommand comando = new MySqlCommand(update, conexionBD);
            conexionBD.Open();
            comando.ExecuteNonQuery();
            conexionBD.Close();
        }

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            Bubble1.BorderColor = Color.Black;
            Bubble1.BackgroundColor = Color.Black;

            string update = "UPDATE users SET color = 'black' WHERE user LIKE '" + Detail.user + "'";
            MySqlConnection conexionBD = Conexion.conexion();
            MySqlCommand comando = new MySqlCommand(update, conexionBD);
            conexionBD.Open();
            comando.ExecuteNonQuery();
            conexionBD.Close();
        }

        private void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            Bubble1.BorderColor = Color.PaleVioletRed;
            Bubble1.BackgroundColor = Color.Red;

            string update = "UPDATE users SET color = 'red' WHERE user LIKE '" + Detail.user + "'";
            MySqlConnection conexionBD = Conexion.conexion();
            MySqlCommand comando = new MySqlCommand(update, conexionBD);
            conexionBD.Open();
            comando.ExecuteNonQuery();
            conexionBD.Close();
        }

        private void TapGestureRecognizer_Tapped_4(object sender, EventArgs e)
        {
            Bubble1.BorderColor = Color.Blue;
            Bubble1.BackgroundColor = Color.LightBlue;

            string update = "UPDATE users SET color = 'blue' WHERE user LIKE '" + Detail.user + "'";
            MySqlConnection conexionBD = Conexion.conexion();
            MySqlCommand comando = new MySqlCommand(update, conexionBD);
            conexionBD.Open();
            comando.ExecuteNonQuery();
            conexionBD.Close();
        }

        private void TapGestureRecognizer_Tapped_5(object sender, EventArgs e)
        {
            Bubble1.BorderColor = Color.Purple;
            Bubble1.BackgroundColor = Color.MediumPurple;

            string update = "UPDATE users SET color = 'purple' WHERE user LIKE '" + Detail.user + "'";
            MySqlConnection conexionBD = Conexion.conexion();
            MySqlCommand comando = new MySqlCommand(update, conexionBD);
            conexionBD.Open();
            comando.ExecuteNonQuery();
            conexionBD.Close();
        }

        private void TapGestureRecognizer_Tapped_6(object sender, EventArgs e)
        {
            string Eva = showEva.IsToggled.ToString();
            Eva = Eva.ToLower();

            string update = "UPDATE users SET showEVA = '"+Eva+"' WHERE user LIKE '" + Detail.user + "'";
            MySqlConnection conexionBD = Conexion.conexion();
            MySqlCommand comando = new MySqlCommand(update, conexionBD);
            conexionBD.Open();
            comando.ExecuteNonQuery();
            conexionBD.Close();

            Principal principal = new Principal(Detail.user);
            this.Navigation.PushModalAsync(principal);
        }
    }
}