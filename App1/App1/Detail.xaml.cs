using App1.Models;
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
using Xamarin.Essentials;
using NavigationPage = Xamarin.Forms.NavigationPage;
using Plugin.AudioRecorder;
using System.Reflection;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using APIEva.Models;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Detail : ContentPage
    {
        //Variables creacion session con APi-Watson
        //Crear la autentificación con nuestra ApiKey:
        static IamAuthenticator authenticator = new IamAuthenticator(apikey: "_DPGYFyIP4aUykVS53B4JFawtz261nvV4l9GGLUuN4eb");

        //Crear el asistente:
        static AssistantService assistant = assistant = new AssistantService("2021-06-14", authenticator); //<- Versión y autentificador

        //Convertir json (de sessionID) a objeto:
        static Session session = null;

        HttpClient client;

        public static string user_name { get; private set; } //<- Guardar el usuario conectado para aplicar las configuraciones
        public static List<Chat> historial = new List<Chat>(); //<- Lista para guardar los chats (preguntas y respuestas)
        public Color bubbleEva { get; private set; } //<- Guardar el color de fondo de Eva.
        public Color bubbleUser{ get; private set; } //<- Guardar el color de fondo del usuario.
        //Guardar la configuracines de Eva:
        public bool showEva { get; private set; }
        public bool showEmotions { get; private set; }
        public bool sound { get; private set; }
        public float volume { get; private set; }


        private readonly AudioPlayer audioPlayer = new AudioPlayer();

        public Detail(string u)
        {
            InitializeComponent();

            client = new HttpClient();

            //Guardar el usuario conectado:
            user_name = u;

            loadConfigurationsAsync();
            loadHistoryAsync();

            //Cargar los chats de esta sessión:
            cv.ItemsSource = "";

            //Creamos asistente y session
            assistant.SetServiceUrl("https://api.eu-de.assistant.watson.cloud.ibm.com"); //<- URL del servicio 
            assistant.DisableSslVerification(true); //<- Desabilitar verificación SSL

            //Crear la sesión con el assistantID:
            var jsonSession = assistant.CreateSession(assistantId: "7ca45e38-0155-4856-8c0b-86e574c514b6");

            //Convertir json (de sessionID) a objeto:
            session = JsonConvert.DeserializeObject<Session>(jsonSession.Response.ToString());

        }

        private async Task loadConfigurationsAsync()
        {
            Configuration c = await getConfigurationsAsync();

            if (c.showEva == "true")
            {
                //cp.BackgroundImageSource = "https://i.pinimg.com/originals/e0/dc/a3/e0dca3c111c0702c4004778395be124c.jpg";
                bg.Source = "https://i.pinimg.com/originals/e0/dc/a3/e0dca3c111c0702c4004778395be124c.jpg";
                showEva = true;
            }
            else
            {
                cp.BackgroundImageSource = "";
                showEva = false;
            }
            if (c.showEmotions == "true")
            {
                showEmotions = true;
            }
            else
            {
                showEmotions = false;
            }
            if (c.sound == "true")
            {
                sound = true;
            }
            else
            {
                sound = false;
            }

            volume = float.Parse(c.volume);

            switch (c.color)
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

        private async Task loadHistoryAsync()
        {
            var list = await getHistoryAsyc(user_name);
           
            foreach (var h in list) {
                switch (h.type)
                {
                    case "text": 
                        Chat Textchat = new Chat(h.question, h.response, "", false, true, false, bubbleEva, bubbleUser); //<- Crear un chat con la pregunta del usuario
                        historial.Add(Textchat); //<- Añadir el chat al historial

                        Chat Spacechat = new Chat(h.question, h.response, "", false, false, true, bubbleEva, bubbleUser); //<- Crear un chat de texto de respuesta
                        historial.Add(Spacechat); //<- Añadir el chat al historial
                        break;
                    case "image":
                        Chat Imagechat = new Chat(h.question, "", h.response, false, true, false, bubbleEva, bubbleUser); //<- Crear un chat con la pregunta del usuario.
                        historial.Add(Imagechat); //<- Añadir el chat al historial

                        Chat Spacechat2 = new Chat(h.question, "", h.response, true, false, false, bubbleEva, bubbleUser); //<- Crear un chat con imagen de respuesta.
                        historial.Add(Spacechat2); //<- Añadir el chat al historial
                        break;
                }
            }
            //Cambiar los colores de los chats ya hablados.
            foreach (var chat in historial)
            {
                chat.bubbleEva = this.bubbleEva;
                chat.bubbleUser = this.bubbleUser;
            }
            cv.ItemsSource = historial;
        }

        private async Task<List<History>> getHistoryAsyc(string user_name)
        {
            Uri uri = new Uri("https://apieva2022.azurewebsites.net/api/History/" + user_name);

            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<History>>(content);  

                return list;

            }
            return null;
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

        private async Task putHistoryAsync(string user_name, string question, string response, string type)
        {
            JObject joHistory = new JObject();
            joHistory.Add("name_user", user_name);
            joHistory.Add("question", question);
            joHistory.Add("response", response);
            joHistory.Add("timedate", "");
            joHistory.Add("type", type);

            Uri uriHistory = new Uri("https://apieva2022.azurewebsites.net/api/History");

            string jsonHistory = JsonConvert.SerializeObject(joHistory);
            StringContent contentHistory = new StringContent(jsonHistory, Encoding.UTF8, "application/json");

            HttpResponseMessage responseConfiguration = null;

            responseConfiguration = await client.PostAsync(uriHistory, contentHistory);
        }

        private async void Button_Clicked(object sender, EventArgs e) 
        {
          
            var question = Question.Text.ToString(); //<- Guardar el mensaje que envia el usuario

            try
            {
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
                var happy = "https://i.pinimg.com/originals/e0/dc/a3/e0dca3c111c0702c4004778395be124c.jpg";
                var love = "https://i.pinimg.com/originals/ed/49/fa/ed49fab9694d4aea01aeaa9aef81228c.jpg";
                var sad = "https://i.pinimg.com/originals/8d/b2/86/8db286b160fc18b6d11225e180048700.jpg";
                var glitch = "https://i.pinimg.com/originals/b7/0a/39/b70a39455c369f378ebec7fa7a153fbb.jpg";

                /*Recorrer toda la respuesta de Eva:*/
                for (int i = 0 ; i <= r.output.generic.Count() ; i++)
                {
                    //Guardar información útil de la respuesta de Eva:
                    var intent = r.output.intents[0].intent.ToString();
                    //var entity = r.output.entities[0].entity.ToString(); //<- A veces el campo está vacío y da error. Por eso está comentado
                    var responseType = r.output.generic[i].response_type.ToString();

                    var response = ""; //<- aqui se guardará la respuesta de Eva.

                    //Comprobar la configuración del usuario:
                    if (showEva && showEmotions)
                    {
                        //Cambiar la expresión de Eva según su Intent:
                        switch (intent)
                        {
                            case "Insultos": bg.Source = sad; break;
                            case "Piropos": bg.Source = love; break;
                            default: bg.Source = happy; break;
                        }
                    }             

                    //Comprovar si la respuesta de Eva es un texto o una imagen.
                    switch (responseType)
                    {
                        case "text":
                            response = r.output.generic[i].text.ToString(); //<- Guardar respuesta de Eva

                            //Si la respuesta de Eva es más de una, solo mostrar la pregunta una vez.
                            if (r.output.generic.Count() > 0 && i > 0)
                            {
                                Chat Spacechat = new Chat(question, response, "", false, false, true, bubbleEva, bubbleUser); //<- Crear un chat de texto de respuesta
                                historial.Add(Spacechat); //<- Añadir el chat al historial

                                putHistoryAsync(user_name, question, response, "text");
                            }
                            else
                            {
                                Chat Textchat = new Chat(question, response, "", false, true, false, bubbleEva, bubbleUser); //<- Crear un chat con la pregunta del usuario
                                historial.Add(Textchat); //<- Añadir el chat al historial

                                Chat Spacechat = new Chat(question, response, "", false, false, true, bubbleEva, bubbleUser); //<- Crear un chat de texto de respuesta
                                historial.Add(Spacechat); //<- Añadir el chat al historial

                                putHistoryAsync(user_name, question, response, "text");
                            }

                            

                            Console.WriteLine("Respuesta (texto): " + response); //<- Imprimir por consola la respuesta de Eva (mensaje)
                            break;
                        case "image":
                            var source = r.output.generic[i].source.ToString(); //<- Guardar respuesta de Eva

                            //Si la respuesta de Eva es más de una, solo mostrar la pregunta una vez.
                            if (r.output.generic.Count() > 0 && i > 0)
                            {
                                Chat Imagechat = new Chat(question, "", source, true, false, false, bubbleEva, bubbleUser); //<- Crear un chat con imagen de respuesta.
                                historial.Add(Imagechat); //<- Añadir el chat al historial

                                putHistoryAsync(user_name, question, source, "image");
                            }
                            else
                            {
                                Chat Imagechat = new Chat(question, "", source, false, true, false, bubbleEva, bubbleUser); //<- Crear un chat con la pregunta del usuario.
                                historial.Add(Imagechat); //<- Añadir el chat al historial

                                Chat Spacechat2 = new Chat(question, "", source, true, false, false, bubbleEva, bubbleUser); //<- Crear un chat con imagen de respuesta.
                                historial.Add(Spacechat2); //<- Añadir el chat al historial

                                putHistoryAsync(user_name, question, source, "image");

                                //cv.ScrollTo(Spacechat2, position: ScrollToPosition.MakeVisible);
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

                    if (responseType == "text" && sound)
                    {
                        await TextToSpeech.SpeakAsync(response, new SpeechOptions
                        {
                            Pitch = 2.0f,
                            Volume = volume
                        });
                    }
                    
                }

                //Limpiar el recurso:
                cv.ItemsSource = "";

                //Imprimir por consola:
                //Console.WriteLine(jsonSession.Response); //<- sessionID (json)
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


        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            NavigationPage opciones = new NavigationPage(new Opciones())
            {
                BarBackgroundColor = bubbleEva,
            };
            this.Navigation.PushModalAsync(opciones);
      
        }

        private void cv_Scrolled(object sender, ItemsViewScrolledEventArgs e)
        {
           
            if (e.VerticalDelta < 0)
            {
                bg.TranslateTo( bg.TranslationX, (bg.TranslationY - 0.2), 5);
            }
            else
            {
                bg.TranslateTo( bg.TranslationX , (bg.TranslationY + 0.2), 5);
            }
        }
    }
}