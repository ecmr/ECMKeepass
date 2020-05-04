using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ECMKeepass.Modelo;
using ECMKeepass.Banco;

namespace ECMKeepass.Telas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Cadastrar : ContentPage
	{
		public Cadastrar ()
		{
			InitializeComponent ();
		}

        void switcher_Toggled(object sender, ToggledEventArgs e)
        {
            //lblVisualizar.Text = String.Format("Switch is now {0}", e.Value);
            Senha.IsPassword = !e.Value;
        }

        public void SalvarAction(object sender, EventArgs args)
        {
            //TODO - Validar Dados do Cadastro
            keepass keepass = new keepass();
            keepass.GrupoNome = GrupoNome.Text;
            //keepass.GrupoId
            keepass.Titulo = Titulo.Text;
            keepass.Usuario = Usuario.Text;
            keepass.Senha = Senha.Text;
            keepass.Url = Url.Text;
            keepass.Comentario = Comentario.Text;

            Database database = new Database();
            database.Cadastro(keepass);

            App.Current.MainPage = new NavigationPage(new Consultar());
        }
    }
}