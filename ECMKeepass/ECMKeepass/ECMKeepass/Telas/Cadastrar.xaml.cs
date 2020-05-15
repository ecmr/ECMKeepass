using ECMKeepass.Banco;
using ECMKeepass.Modelo;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
             Senha.IsPassword = !e.Value;
        }

        public void SalvarAction(object sender, EventArgs args)
        {
            keepass keepass = new keepass();
            keepass.Titulo = Titulo.Text;
            keepass.Usuario = Usuario.Text;
            keepass.Senha = Senha.Text;
            keepass.Url = Url.Text;
            keepass.Comentario = Comentario.Text;

            Database database = new Database();
            database.Cadastro(keepass);

            Navigation.PushAsync(new Grupo(keepass));
        }
    }
}