using IBM.Cloud.SDK.Core.Authentication.Iam;
using IBM.Cloud.SDK.Core.Http.Exceptions;
using IBM.Watson.Assistant.v2;
using IBM.Watson.Assistant.v2.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var m = mensaje.Text.ToString();


            string usuario1Antiguo = usuario1.Text.ToString();
            string usuario2Antiguo = usuario2.Text.ToString();
            string usuario3Antiguo = usuario3.Text.ToString();

            usuario1.Text = m;
            usuario2.Text = usuario1Antiguo;
            usuario3.Text = usuario2Antiguo;
            usuario4.Text = usuario3Antiguo;

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
                string sessionID = result.Response.Substring(20, 36);
                //Escribimos el sessionID Limpio
                Console.WriteLine("Session ID: " + sessionID);

                //Mandamos un mensaje de prueba
                var result2 = assistant.Message(
                    assistantId: "7ca45e38-0155-4856-8c0b-86e574c514b6",
                    sessionId: sessionID,
                    input: new MessageInput()
                    {
                        Text = m //Variable que recoge el texo
                    }
                 );
              
                //Convertir json a objeto:
                Root deserialized = JsonConvert.DeserializeObject<Root>(result2.Response.ToString());
                
                //Desplazar y mostrar mensaje:
                string eva1Antiguo = eva1.Text.ToString();
                string eva2Antiguo = eva2.Text.ToString();
                string eva3Antiguo = eva2.Text.ToString();

                eva1.Text = deserialized.output.generic[0].text.ToString();
                eva2.Text = eva1Antiguo;
                eva3.Text = eva2Antiguo;
                eva4.Text = eva3Antiguo;

                string usuario1Antiguoo = usuario1.Text.ToString();
                string usuario2Antiguoo = usuario2.Text.ToString();
                string usuario3Antiguoo = usuario3.Text.ToString();

                usuario1.Text = "";
                usuario2.Text = usuario1Antiguoo;
                usuario3.Text = usuario2Antiguoo;
                usuario4.Text = usuario3Antiguoo;

                mensaje.Text = "";

                //Console.WriteLine(result2.Response);
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
