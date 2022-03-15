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
            usuario.Text = m;


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
              
                //Intento de convertir json a objeto:
                
                //El objeto root que digo es este de aqui abajo vv
                Root deserialized = JsonConvert.DeserializeObject<Root>(result2.Response.ToString());
                eva.Text = deserialized.output.generic[0].text.ToString();


                //Console.WriteLine(result2.Response);
                //eva.Text = result2.Response.ToString();
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
