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
    public partial class TwilightZone : ContentPage
    {
        public Color color { get; private set; } //<- Guardar el color de fondo de Eva.

        public TwilightZone(Color c)
        {
            InitializeComponent();
            color = c;
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            NavigationPage o = new NavigationPage(new Opciones())
            {
                BarBackgroundColor = color,
            };
            this.Navigation.PushModalAsync(o);
        }
    }
}