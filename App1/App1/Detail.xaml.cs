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
        public static List<Chat> list = new List<Chat>();
        public MyViewModel aaa { get; set; }

        public Detail()
        {
            InitializeComponent();
            
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            //imagenEva.Source = ImageSource.FromResource("App1.Images.eva.jpeg");


            var m = question.Text.ToString();

            //usuario1.Text = usuario1.Text + "\n\r " + m;
            //eva1.Text = eva1.Text + "\n\r\n\r\n\r";

            try
            {
                IamAuthenticator authenticator = new IamAuthenticator(apikey: "_DPGYFyIP4aUykVS53B4JFawtz261nvV4l9GGLUuN4eb");

                AssistantService assistant = new AssistantService("2021-06-14", authenticator);
                assistant.SetServiceUrl("https://api.eu-de.assistant.watson.cloud.ibm.com");

                assistant.DisableSslVerification(true);

                //Creem la sessio
                var result = assistant.CreateSession(assistantId: "7ca45e38-0155-4856-8c0b-86e574c514b6");
                //Escribimos la SessionID
                Console.WriteLine(result.Response);
                //Convertir json de la sessionID a objeto:
                Session s = JsonConvert.DeserializeObject<Session>(result.Response.ToString());

                //Escribimos el sessionID Limpio
                Console.WriteLine("Session ID: " + s);

                //Mandamos un mensaje de prueba
                var result2 = assistant.Message(
                    assistantId: "7ca45e38-0155-4856-8c0b-86e574c514b6",
                    sessionId: s.session_id,
                    input: new MessageInput()
                    {
                        Text = m //Variable que recoge el texo
                    }
                 );
                Console.WriteLine(result2.Response);
                Console.WriteLine("Mensaje introducido: " + m);

                //Convertir json a objeto:
                Root deserialized = JsonConvert.DeserializeObject<Root>(result2.Response.ToString());

                //Desplazar y mostrar mensaje:

                //eva1.Text = eva1.Text + "\n\r " + deserialized.output.generic[0].text.ToString() ;

                switch (deserialized.output.intents[0].intent.ToString())
                {
                    case "Insultos":
                        cp.BackgroundImageSource = "https://cdn.wamiz.fr/cdn-cgi/image/quality=80,width=1200,height=675,fit=cover/article/main-picture/61090e4759fdb447112947.jpg";
                        break;
                    case "Piropos":
                        cp.BackgroundImageSource = "https://cdn.wamiz.fr/cdn-cgi/image/quality=80,width=1200,height=675,fit=cover/article/main-picture/61090e4759fdb447112947.jpg";
                        break;
                    default:
                        cp.BackgroundImageSource = "https://i.pinimg.com/originals/d8/6f/92/d86f92c6e76d5a4a84dcb779fb6b6447.jpg";
                        break;
                }

                Chat chat = new Chat(m, deserialized.output.generic[0].text.ToString(), "https://www.xtrafondos.com/wallpapers/alan-walker-4721.jpg"); //Objeto Chat
                list.Add(chat);
                cv.ItemsSource = "";
                cv.ItemsSource = list;

                chat.MyProperty = "https://www.xtrafondos.com/wallpapers/alan-walker-4721.jpg";


                //Mostramos la respuesta de EVA por consola
                Console.WriteLine("Respuesta: " + deserialized.output.generic[0].text.ToString());

                //usuario1.Text = usuario1.Text + "\n\r\n\r\n\r";
                question.Text = "";
                //Sacamos el tipo de respusta IMAGEN/TEXTO
                Console.WriteLine("RESPONSE_TYPE: " +deserialized.output.generic[0].response_type);

                //Sacamos el intent
                Console.WriteLine("INTENT: " + deserialized.output.intents[0].intent);
                //Sacamos el entinty
                Console.WriteLine("ENTITY: " + deserialized.output.entities[0].entity.ToString());

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