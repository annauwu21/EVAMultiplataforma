using App1.Models;
using App1.ViewModels;
using IBM.Cloud.SDK.Core.Authentication.Iam;
using IBM.Cloud.SDK.Core.Http.Exceptions;
using IBM.Watson.Assistant.v2;
using IBM.Watson.Assistant.v2.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Detail : ContentPage
    {
        public static List<Chat> historial = new List<Chat>(); //<- Lista para guardar los chats (preguntas y respuestas)

        public Detail()
        {
            InitializeComponent();
            
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
                    var intent = r.output.intents[i].intent.ToString();
                    //var entity = r.output.entities[0].entity.ToString();
                    var responseType = r.output.generic[i].response_type.ToString();

                    DisplayAlert("Error", i.ToString(), "Cerrar");
                    DisplayAlert("Error", r.output.generic.Count().ToString(), "Cerrar");
                    DisplayAlert("Error", jsonResponse.Response, "Cerrar");

                    //Cambiar la expresión de Eva según su Intent:
                    switch (intent)
                    {
                        case "Insultos": cp.BackgroundImageSource = angry; break;
                        case "Piropos": cp.BackgroundImageSource = happy; break;
                        default: cp.BackgroundImageSource = normal; break;
                    }

                    //Comprovar si la respuesta de Eva es un texto o una imagen.
                    switch (responseType)
                    {
                        case "text":
                            var response = r.output.generic[i].text.ToString(); //<- Guardar respuesta de Eva

                            Chat Textchat = new Chat(question, response, "", false); //<- Crear un chat sin imagen.
                            historial.Add(Textchat); //<- Añadir el chat al historial

                            Console.WriteLine("Respuesta (texto): " + response); //<- Imprimir por consola la respuesta de Eva (mensaje)

                            DisplayAlert("Error",response, "Cerrar");
                            break;
                        case "image":
                            var source = r.output.generic[i].source.ToString(); //<- Guardar respuesta de Eva

                            Chat Imagechat = new Chat(question, "", source, true); //<- Crear un chat con imagen.
                            historial.Add(Imagechat); //<- Añadir el chat al historial

                            Console.WriteLine("Respuesta (imagen): " + source); //<- Imprimir por consola la respuesta de Eva (url de la imagen)

                            DisplayAlert("Error", source, "Cerrar");
                            break;
                    }

                    //Limpiar el recurso:
                    cv.ItemsSource = "";

                    //Poner en el recurso el historial:
                    cv.ItemsSource = historial;

                    question = "";
                }

                //Limpiar el recurso:
                cv.ItemsSource = "";

                //Vaciar el campo de texto para escribir un mensaje:
                Question.Text = "";

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
    }
}