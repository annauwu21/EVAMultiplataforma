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
    public partial class Principal : MasterDetailPage
    {
        public Principal()
        {
            InitializeComponent();
            //Metemos el menu desplegable lateral
            this.Master = new Master();
            //Metemos la pagina principal
            this.Detail = new NavigationPage(new Detail());
        }
    }
}
