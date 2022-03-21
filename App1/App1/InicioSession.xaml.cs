using MySqlConnector;
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
    public partial class InicioSession : ContentPage
    {
        Cifrado cifrado = new Cifrado();

        public InicioSession()
        {
            InitializeComponent();
        }

        private void btnRegistrar_Clicked(object sender, EventArgs e)
        {
            Registrar registrar = new Registrar();
            this.Navigation.PushModalAsync(registrar);
        }

        private void btnIniciar_Clicked(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(User.Text) || !string.IsNullOrEmpty(Pass.Text)){
                //Si las casillas estan llenas lo guardamos en variables
                var user = User.Text.ToLower(); 
                var pass = Pass.Text.ToLower();
                MySqlDataReader reader = null;

                string select = "SELECT * FROM users WHERE user LIKE '" + user + "' LIMIT 1";
                MySqlConnection conexionBD = Conexion.conexion();
                conexionBD.Open();

                try
                {
                    MySqlCommand comando = new MySqlCommand(select, conexionBD);
                    reader = comando.ExecuteReader();
                    if (reader.HasRows) //Si reader encuentra algo
                    {
                        while (reader.Read())//Leer lineas
                        {
                            //Si el usuario existe y los datos son correctos, abre la ventana AdminEleccion
                            //user.Equals(reader.GetString(0)) && pass.Equals(cifrado.descifrar(reader.GetString(1)))
                            if (user.Equals(reader.GetString(0)) && pass.Equals(cifrado.descifrar(reader.GetString(1))))
                            {
                                //Abrimos la ventana principal
                                Principal principal = new Principal();
                                this.Navigation.PushModalAsync(principal);
                            }
                            else
                            {
                                DisplayAlert("Error", "Usuario o contraseña incorrecta", "Cerrar");
                            }
                        }
                    }
                    else
                    {
                        DisplayAlert("Error", "Usuario o contraseña incorrecta", "Cerrar");
                    }
                }
                catch (MySqlException ex)
                {
                    DisplayAlert("Error", "No se ha encontrado al usuario", "Cerrar");
                }
                finally
                {
                    conexionBD.Close();
                }
            }
            else
            {
                DisplayAlert("Error", "Tienes que rellenar las casillas", "Cerrar");
            }

        }


        private void comprobarbtn()
        {

            if (!string.IsNullOrEmpty(User.Text) || !string.IsNullOrEmpty(Pass.Text))
            {
                btnInicarSessio.IsEnabled = true; //Mostrem el boto iniciar sessio
            }
            else
            {
                btnInicarSessio.IsEnabled = false;
            }
        }

        private void limpiar()
        {
            User.Text = "";
            Pass.Text = "";
        }
    }
}