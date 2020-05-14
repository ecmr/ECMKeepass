
using ECMKeepass.Banco;
using ECMKeepass.Modelo;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ECMKeepass.Telas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetalharVaga : ContentPage
	{
        public DetalharVaga(keepass keepass)
        {
            InitializeComponent();

            BindingContext = keepass;
        }

        void switcher_Toggled(object sender, ToggledEventArgs e)
        {
            Senha.IsPassword = !e.Value;
        }

        public void EditarCadastro(object sender, EventArgs args)
        {
            if (btnEditar.Text == "Editar")
            {
                btnEditar.Text = "Salvar";
                lblTitulo.IsVisible = false;
                Titulo.IsEnabled = true;
                lblTituloEditar.IsVisible = true;
                Titulo.IsVisible = true;
                GrupoNome.IsEnabled = true;
                Usuario.IsEnabled = true;
                Senha.IsEnabled = true;
                Url.IsEnabled = true;
                Comentario.IsEnabled = true;
            }
            else
            {
                SalvarDados();
                lblTitulo.IsVisible = true;
                Titulo.IsEnabled = false;
                lblTituloEditar.IsVisible = false;
                Titulo.IsVisible = false;
                GrupoNome.IsEnabled = false;
                Usuario.IsEnabled = false;
                Senha.IsEnabled = false;
                Url.IsEnabled = false;
                Comentario.IsEnabled = false;
                btnEditar.Text = "Editar";
            }

        }

        public void ExcluirAction(object sender, EventArgs args)
        {

            Database database = new Database();
            keepass kp = database.Pesquisar(lblTitulo.Text).FirstOrDefault();
            database.Exclusao(kp);

            App.Current.MainPage = new NavigationPage(new Grupos());
        }

        private void SalvarDados()
        {
            keepass keepass = new keepass();
            keepass.GrupoNome = GrupoNome.Text;
            keepass.Titulo = Titulo.Text;
            keepass.Usuario = Usuario.Text;
            keepass.Senha = Senha.Text;
            keepass.Url = Url.Text;
            keepass.Comentario = Comentario.Text;

            Database database = new Database();
            keepass kp = database.Pesquisar(lblTitulo.Text).FirstOrDefault();
            database.Exclusao(kp);
            database = null;
            database = new Database();
            database.Cadastro(keepass);

            App.Current.MainPage = new NavigationPage(new Grupo(keepass));
        }
    }
}