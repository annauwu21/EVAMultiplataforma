﻿using App1.Models;
using IBM.Cloud.SDK.Core.Authentication.Iam;
using IBM.Cloud.SDK.Core.Http.Exceptions;
using IBM.Watson.Assistant.v2;
using IBM.Watson.Assistant.v2.Model;
using MySqlConnector;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific.AppCompat;
using Xamarin.Forms.Xaml;
using NavigationPage = Xamarin.Forms.NavigationPage;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Detail : ContentPage
    {
        public static string user { get; private set; } //<- Guardar el usuario conectado para aplicar las configuraciones


        public static List<Chat> historial = new List<Chat>(); //<- Lista para guardar los chats (preguntas y respuestas)


        public Color Bubble1Color { get; private set; } //<- Guardar el color de fondo de Eva.
        public Color Bubble1Border { get; private set; } //<- Guardar el color del borde de Eva.

        public bool showEva { get; private set; } //<- Guardar la configuración de mostrar/no mostrar Eva.

        public Detail(string u)
        {
            InitializeComponent();

            NavigationPage.SetHasBackButton(this, false);

            //Guardar el usuario conectado:
            user = u;

            //Buscar por el usuario las configuraciones:
            string select = "SELECT * FROM users WHERE user LIKE '" + user + "' LIMIT 1"; //<- Sentencía sql

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
                            Bubble1Color = Color.Purple;
                            Bubble1Border = Color.MediumPurple;
                        break;
                        case "green":
                            Bubble1Color = Color.LightGreen;
                            Bubble1Border = Color.Green;
                        break;
                        case "white":
                            Bubble1Color = Color.White;
                            Bubble1Border = Color.Black;
                        break;
                        case "black":
                            Bubble1Color = Color.Black;
                            Bubble1Border = Color.Black;
                        break;
                        case "red":
                            Bubble1Color = Color.Red;
                            Bubble1Border = Color.PaleVioletRed;
                        break;
                        case "blue":
                            Bubble1Color = Color.Blue;
                            Bubble1Border = Color.LightBlue;
                        break;
                    }

                    //Cambiar la configuración mostrar/no mostrar Eva según el usuario:
                    if (reader.GetString("showEVA") == "true")
                    {
                        cp.BackgroundImageSource = "https://i.pinimg.com/originals/d8/6f/92/d86f92c6e76d5a4a84dcb779fb6b6447.jpg";
                        showEva = true;
                    }
                    else
                    {
                        cp.BackgroundImageSource = "";
                        showEva = false;
                    }
                }
            }

            conexionBD.Close(); //<- Cerrar la conexión

            //Cambiar los colores de los chats ya hablados.
            foreach (var chat in historial)
            {
                chat.Bubble1Border = this.Bubble1Border;
                chat.Bubble1Color = this.Bubble1Color;
            }

            //Cargar los chats de esta sessión:
            cv.ItemsSource = historial;
        }
        
        private void Button_Clicked(object sender, EventArgs e) 
        {
          
            var question = Question.Text.ToString(); //<- Guardar el mensaje que envia el usuario

            try
            {
                //Crear la autentificación con nuestra ApiKey:
                IamAuthenticator authenticator = new IamAuthenticator(apikey: "_DPGYFyIP4aUykVS53B4JFawtz261nvV4l9GGLUuN4eb");

                //Crear el asistente:
                AssistantService assistant = new AssistantService("2021-06-14", authenticator); //<- Versión y autentificador
                assistant.SetServiceUrl("https://api.eu-de.assistant.watson.cloud.ibm.com"); //<- URL del servicio 
                assistant.DisableSslVerification(true); //<- Desabilitar verificación SSL

                //Crear la sesión con el assistantID:
                var jsonSession = assistant.CreateSession(assistantId: "7ca45e38-0155-4856-8c0b-86e574c514b6");

                //Convertir json (de sessionID) a objeto:
                Session session = JsonConvert.DeserializeObject<Session>(jsonSession.Response.ToString());
              
                //Mandar el mensaje a Eva:
                var jsonResponse = assistant.Message(
                    assistantId: "7ca45e38-0155-4856-8c0b-86e574c514b6", //<- assistantID
                    sessionId: session.session_id,  //<- sessionID
                    input: new MessageInput()
                    {
                        Text = question //<- Mensaje que envia el usuario
                    }
                 );

                //Convertir json (de la respuesta de Eva) a objeto:
                Root r = JsonConvert.DeserializeObject<Root>(jsonResponse.Response.ToString());

                //Guardar distintas expresiones de Eva:
                var normal = "https://i.pinimg.com/originals/d8/6f/92/d86f92c6e76d5a4a84dcb779fb6b6447.jpg";
                var happy = "https://www.mundogatos.com/Uploads/mundogatos.com/ImagenesGrandes/como-hacer-a-tu-gato-feliz.jpg";
                var angry = "https://cdn.wamiz.fr/cdn-cgi/image/quality=80,width=1200,height=675,fit=cover/article/main-picture/61090e4759fdb447112947.jpg";

                /*Recorrer toda la respuesta de Eva:*/
                for (int i = 0 ; i <= r.output.generic.Count() ; i++)
                {
                    //Guardar información útil de la respuesta de Eva:
                    var intent = r.output.intents[0].intent.ToString();
                    //var entity = r.output.entities[0].entity.ToString(); //<- A veces el campo está vacío y da error. Por eso está comentado
                    var responseType = r.output.generic[i].response_type.ToString();

                    //Comprobar la configuración del usuario:
                    if (showEva)
                    {
                        //Cambiar la expresión de Eva según su Intent:
                        switch (intent)
                        {
                            case "Insultos": cp.BackgroundImageSource = angry; break;
                            case "Piropos": cp.BackgroundImageSource = happy; break;
                            default: cp.BackgroundImageSource = normal; break;
                        }
                    }             

                    //Comprovar si la respuesta de Eva es un texto o una imagen.
                    switch (responseType)
                    {
                        case "text":
                            var response = r.output.generic[i].text.ToString(); //<- Guardar respuesta de Eva

                            //Si la respuesta de Eva es más de una, solo mostrar la pregunta una vez.
                            if (r.output.generic.Count() > 0 && i > 0)
                            {
                                Chat Spacechat = new Chat(question, response, "", false, false, true, Bubble1Color, Bubble1Border); //<- Crear un chat de texto de respuesta
                                historial.Add(Spacechat); //<- Añadir el chat al historial
                            }
                            else
                            {
                                Chat Textchat = new Chat(question, response, "", false, true, false, Bubble1Color, Bubble1Border); //<- Crear un chat con la pregunta del usuario
                                historial.Add(Textchat); //<- Añadir el chat al historial

                                Chat Spacechat = new Chat(question, response, "", false, false, true, Bubble1Color, Bubble1Border); //<- Crear un chat de texto de respuesta
                                historial.Add(Spacechat); //<- Añadir el chat al historial
                            }

                            Console.WriteLine("Respuesta (texto): " + response); //<- Imprimir por consola la respuesta de Eva (mensaje)
                            break;
                        case "image":
                            var source = r.output.generic[i].source.ToString(); //<- Guardar respuesta de Eva

                            //Si la respuesta de Eva es más de una, solo mostrar la pregunta una vez.
                            if (r.output.generic.Count() > 0 && i > 0)
                            {
                                Chat Imagechat = new Chat(question, "", source, true, false, false, Bubble1Color, Bubble1Border); //<- Crear un chat con imagen de respuesta.
                                historial.Add(Imagechat); //<- Añadir el chat al historial
                            }
                            else
                            {
                                Chat Imagechat = new Chat(question, "", source, false, true, false, Bubble1Color, Bubble1Border); //<- Crear un chat con la pregunta del usuario.
                                historial.Add(Imagechat); //<- Añadir el chat al historial

                                Chat Spacechat2 = new Chat(question, "", source, true, false, false, Bubble1Color, Bubble1Border); //<- Crear un chat con imagen de respuesta.
                                historial.Add(Spacechat2); //<- Añadir el chat al historial

                                cv.ScrollTo(Spacechat2, position: ScrollToPosition.MakeVisible);
                            }
                      
                            Console.WriteLine("Respuesta (imagen): " + source); //<- Imprimir por consola la respuesta de Eva (url de la imagen)
                            break;
                    }

                    //impiar el recurso:
                    cv.ItemsSource = "";

                    //Poner en el recurso el historial:
                    cv.ItemsSource = historial;

                    //Vaciar el campo de texto para escribir un mensaje:
                    Question.Text = "";
                }

                //Limpiar el recurso:
                cv.ItemsSource = "";

                //Imprimir por consola:
                Console.WriteLine(jsonSession.Response); //<- sessionID (json)
                Console.WriteLine("Session ID: " + session); //<- sessionID (limpio)
                Console.WriteLine("Mensaje introducido: " + question); //<- mensaje que envia el usuario
                Console.WriteLine(jsonResponse.Response); //<- respuesta de Eva (json)
                //Console.WriteLine("RESPONSE_TYPE: " + responseType); //<- tipo de respuesta (text/image)
                //Console.WriteLine("INTENT: " + intent); //<- intent
                //Console.WriteLine("ENTITY: " + entity); //<- entity

            }
            catch (ServiceResponseException es)
            {
                Console.WriteLine("Error: " + es.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Opciones principal = new Opciones();
            this.Navigation.PushModalAsync(principal);
        }
    }
}