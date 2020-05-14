using ECMKeepass.Banco;
using ECMKeepass.Modelo;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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

        public void ExcluirAction(object sender, EventArgs args)
        {
            Database database = new Database();
            database.Exclusao(keepass);

            App.Current.MainPage = new NavigationPage(new MinhasVagasCadastradas());
        }

        void switcher_Toggled(object sender, ToggledEventArgs e)
        {
            Senha.IsPassword = !e.Value;
        }
    }
}