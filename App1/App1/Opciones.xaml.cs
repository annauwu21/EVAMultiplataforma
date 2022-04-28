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
    public partial class Opciones : ContentPage
    {
        public Opciones()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Bubble1.BorderColor = Color.Green;
            Bubble1.BackgroundColor = Color.LightGreen;
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            Bubble1.BorderColor = Color.Black;
            Bubble1.BackgroundColor = Color.White;
        }

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            Bubble1.BorderColor = Color.Black;
            Bubble1.BackgroundColor = Color.Black;
        }

        private void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            Bubble1.BorderColor = Color.Red;
            Bubble1.BackgroundColor = Color.PaleVioletRed;
        }

        private void TapGestureRecognizer_Tapped_4(object sender, EventArgs e)
        {
            Bubble1.BorderColor = Color.Blue;
            Bubble1.BackgroundColor = Color.LightBlue;
        }

        private void TapGestureRecognizer_Tapped_5(object sender, EventArgs e)
        {
            Bubble1.BorderColor = Color.Purple;
            Bubble1.BackgroundColor = Color.MediumPurple;
        }

        private void TapGestureRecognizer_Tapped_6(object sender, EventArgs e)
        {
            this.Navigation.PopModalAsync();
        }
    }
}