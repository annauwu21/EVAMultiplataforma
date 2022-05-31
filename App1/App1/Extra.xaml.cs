using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Extra : ContentPage
    {
        public Extra()
        {
            InitializeComponent();

            /*var assembly = typeof(App).GetTypeInfo().Assembly;
            Stream audioStream = assembly.GetManifestResourceStream("App1.Sonidos.backgroundMusic.mp3");
            var audio = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            audio.Loop = true;
            audio.Load(audioStream);

            if (!audio.IsPlaying)
            {
                audio.Play();
            }*/
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Opciones p = new Opciones();
            this.Navigation.PushModalAsync(p);
        }
    }
}