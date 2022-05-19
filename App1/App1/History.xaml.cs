using App1.Models;
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
    public partial class History : ContentPage
    {
        public static List<Chat> historial = new List<Chat>(); //<- Lista para guardar los chats (preguntas y respuestas)

        public Color bubbleEva { get; private set; } //<- Guardar el color de fondo de Eva.
        public Color bubbleUser { get; private set; } //<- Guardar el color de fondo del usuario.
        public History(string u)
        {
            InitializeComponent();

            //Buscar por el usuario las configuraciones:
            string config = "SELECT * FROM configurations WHERE user LIKE '" + u + "' LIMIT 1"; //<- Sentencía sql

            MySqlConnection conexionBDConfig = Conexion.conexion(); //<- Crear connexión
            conexionBDConfig.Open(); //<- Abrir la conexión

            MySqlCommand comandoConfig = new MySqlCommand(config, conexionBDConfig); //<- Executar sentencía

            MySqlDataReader readerConfig = null; //<- Crear reader
            readerConfig = comandoConfig.ExecuteReader(); //<- Ejecutar reader

            //Si reader encuentra algo:
            if (readerConfig.HasRows)
            {
                //Leer lineas:
                while (readerConfig.Read())
                {
                    //Cambiar los colores dependiendo de las configuraciones del usuario:

                    switch (readerConfig.GetString("color"))
                    {
                        case "purple":
                            bubbleEva = Color.FromHex("#6656bc");
                            bubbleUser = Color.FromHex("#d1cedd");
                            break;
                        case "green":
                            bubbleEva = Color.FromHex("#bae860");
                            bubbleUser = Color.FromHex("#d8ed96");
                            break;
                        case "white":
                            bubbleEva = Color.FromHex("#e5c6db");
                            bubbleUser = Color.FromHex("#d6d3d6");
                            break;
                        case "black":
                            bubbleEva = Color.FromHex("#30383a");
                            bubbleUser = Color.FromHex("#666d70");
                            break;
                        case "red":
                            bubbleEva = Color.FromHex("#f43f4f");
                            bubbleUser = Color.FromHex("#f9b2b7");
                            break;
                        case "blue":
                            bubbleEva = Color.FromHex("#75b2dd");
                            bubbleUser = Color.FromHex("#c4d8e2");
                            break;
                    }
                }
            }
            readerConfig.Close();
            conexionBDConfig.Close(); //<- Cerrar la conexión

            //Buscar por el usuario el historial:
            string selectHistory = "SELECT * FROM history WHERE user LIKE '" + u + "' ORDER BY timedate DESC"; //<- Sentencía sql
            MySqlConnection conexionBDHistory = Conexion.conexion(); //<- Crear connexión
            conexionBDHistory.Open(); //<- Abrir la conexión

            MySqlCommand comandoHistory = new MySqlCommand(selectHistory, conexionBDHistory); //<- Executar sentencía

            MySqlDataReader readerHistory = null; //<- Crear reader
            readerHistory = comandoHistory.ExecuteReader(); //<- Ejecutar reader

            //Si reader encuentra algo:
            if (readerHistory.HasRows)
            {
                //Leer lineas:
                while (readerHistory.Read())
                {
                    switch (readerHistory.GetString("tipe"))
                    {
                        case "text":

                            //Si la respuesta de Eva es más de una, solo mostrar la pregunta una vez.
                            //if (r.output.generic.Count() > 0 && i > 0)
                            //{
                                //Chat Spacechat = new Chat(question, response, "", false, false, true, bubbleEva, bubbleUser); //<- Crear un chat de texto de respuesta
                                //historial.Add(Spacechat); //<- Añadir el chat al historial
                            //}
                            //else
                            //{
                                Chat Textchat = new Chat(readerHistory.GetString("question"), readerHistory.GetString("response"), "", false, true, false, bubbleEva, bubbleUser); //<- Crear un chat con la pregunta del usuario
                                historial.Add(Textchat); //<- Añadir el chat al historial

                                Chat Spacechat = new Chat(readerHistory.GetString("question"), readerHistory.GetString("response"), "", false, false, true, bubbleEva, bubbleUser); //<- Crear un chat de texto de respuesta
                                historial.Add(Spacechat); //<- Añadir el chat al historial
                            //}

                            break;
                        case "image":

                            //Si la respuesta de Eva es más de una, solo mostrar la pregunta una vez.
                            //if (r.output.generic.Count() > 0 && i > 0)
                            //{
                               //Chat Imagechat = new Chat(question, "", source, true, false, false, bubbleEva, bubbleUser); //<- Crear un chat con imagen de respuesta.
                               // historial.Add(Imagechat); //<- Añadir el chat al historial
                            //}
                            //else
                            //{
                                Chat Imagechat = new Chat(readerHistory.GetString("question"), "", readerHistory.GetString("response"), false, true, false, bubbleEva, bubbleUser); //<- Crear un chat con la pregunta del usuario.
                                historial.Add(Imagechat); //<- Añadir el chat al historial

                                Chat Spacechat2 = new Chat(readerHistory.GetString("question"), "", readerHistory.GetString("response"), true, false, false, bubbleEva, bubbleUser); //<- Crear un chat con imagen de respuesta.
                                historial.Add(Spacechat2); //<- Añadir el chat al historial

                                //cv.ScrollTo(Spacechat2, position: ScrollToPosition.MakeVisible);
                            //}
                            break;
                    }
                }
            }
            readerHistory.Close();
            conexionBDHistory.Close();
            foreach (var chat in historial)
            {
                chat.bubbleEva = this.bubbleEva;
                chat.bubbleUser = this.bubbleUser;
            }
            cv.ItemsSource = historial;


        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Opciones o = new Opciones();
            this.Navigation.PushModalAsync(o);
        }
    }
}