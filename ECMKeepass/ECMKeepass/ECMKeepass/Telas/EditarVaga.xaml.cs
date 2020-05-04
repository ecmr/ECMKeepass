using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ECMKeepass.Banco;
using ECMKeepass.Modelo;

namespace ECMKeepass.Telas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditarVaga : ContentPage
	{
        private keepass keepass { get; set; }
        public EditarVaga(keepass keepass)
        {
            InitializeComponent();

            this.keepass = keepass;

            GrupoNome.Text = keepass.GrupoNome.ToString();
            Titulo.Text = keepass.Titulo.ToString();
            Usuario.Text = keepass.Usuario.ToString();
            Senha.Text = keepass.Senha.ToString();
            Url.Text = keepass.Url.ToString();
            Comentario.Text = keepass.Comentario.ToString();
        }

        public void SalvarAction(object sender, EventArgs args)
        {
            keepass.GrupoNome = GrupoNome.Text;
            keepass.Titulo = Titulo.Text;
            keepass.Usuario = Usuario.Text;
            keepass.Senha = Senha.Text;
            keepass.Url = Url.Text;
            keepass.Comentario = Comentario.Text;

            Database database = new Database();
            database.Atualizacao(keepass);

            App.Current.MainPage = new NavigationPage(new MinhasVagasCadastradas());
        }

        void switcher_Toggled(object sender, ToggledEventArgs e)
        {
            //lblVisualizar.Text = String.Format("Switch is now {0}", e.Value);
            Senha.IsPassword = !e.Value;
        }
    }
}