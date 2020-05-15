using ECMKeepass.Banco;
using ECMKeepass.Modelo;
using System;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ECMKeepass.Telas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditarVaga : ContentPage
	{
        public EditarVaga(keepass keepass)
        {
            InitializeComponent();

            BindingContext = keepass;

            GrupoNome.Text = keepass.GrupoNome.ToString();
            Titulo.Text = keepass.Titulo.ToString();
            Usuario.Text = keepass.Usuario.ToString();
            Senha.Text = keepass.Senha.ToString();
            Url.Text = keepass.Url.ToString();
            Comentario.Text = keepass.Comentario.ToString();
        }

        void switcher_Toggled(object sender, ToggledEventArgs e)
        {
            Senha.IsPassword = !e.Value;
        }

        public void GerarSenha(object sender, EventArgs args)
        {
            Database database = new Database();
            keepass kp = database.Pesquisar(Titulo.Text).FirstOrDefault();
            Navigation.PushAsync(new GeradorSenha(kp));
        }

        public void EditarCadastro(object sender, EventArgs args)
        {
                SalvarDados();
        }

        public void ExcluirAction(object sender, EventArgs args)
        {
            Database database = new Database();
            keepass kp = database.Pesquisar(Titulo.Text).FirstOrDefault();
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
            keepass kp = database.Pesquisar(Titulo.Text).FirstOrDefault();
            database.Exclusao(kp);
            database = null;
            database = new Database();
            database.Cadastro(keepass);

            App.Current.MainPage = new NavigationPage(new Grupo(keepass));
        }

        public string NewPassWord(int tamanho, bool UpCase, bool LoCase, bool Digitos, bool SpChars)
        {
            string validar = "abcdefghijklmnozABCDEFGHIJKLMNOZ1234567890@#$%&*!";
            try
            {
                StringBuilder strbld = new StringBuilder(100);
                Random random = new Random();
                while (0 < tamanho--)
                {
                    strbld.Append(validar[random.Next(validar.Length)]);
                }
                return strbld.ToString();
            }
            catch (Exception ex)
            {
                DisplayAlert("Erro", ex.Message, "Cancelar");
            }
            return "1q2w3e4r";
        }
    }
}