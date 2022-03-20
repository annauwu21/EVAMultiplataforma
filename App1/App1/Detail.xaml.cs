using IBM.Cloud.SDK.Core.Authentication.Iam;
using IBM.Cloud.SDK.Core.Http.Exceptions;
using IBM.Watson.Assistant.v2;
using IBM.Watson.Assistant.v2.Model;
using Newtonsoft.Json;
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
    public partial class Detail : ContentPage
    {
        public Detail()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            //imagenEva.Source = ImageSource.FromResource("App1.Images.eva.jpeg");

            var m = mensaje.Text.ToString();

            usuario1.Text = usuario1.Text + "\n\r " + m;
            eva1.Text = eva1.Text + "\n\r\n\r\n\r";

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
                //Extraemos solo el SessionID de la respuesta
                string sessionID = result.Response.Substring(20, 36); //WINDOWS: 20,36 - ANDROID: 19,36 - Hay que tratar el JSON para que coja siempre la SessionID correcta
                //Escribimos el sessionID Limpio
                Console.WriteLine("Session ID: " + sessionID);

                //Convertir json de la sessionID a objeto:
                Session s = JsonConvert.DeserializeObject<Session>(result.Response.ToString());

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

                eva1.Text = eva1.Text + "\n\r " + deserialized.output.generic[0].text.ToString() ;

                //Mostramos la respuesta de EVA por consola
                Console.WriteLine("Respuesta: " + deserialized.output.generic[0].text.ToString());

                usuario1.Text = usuario1.Text + "\n\r\n\r\n\r";

                mensaje.Text = "";

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