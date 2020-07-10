
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
	public partial class Detalhar : ContentPage
	{
        public Detalhar(keepass keepass)
        {
            InitializeComponent();

            BindingContext = keepass;

            Database database = new Database();

            GrupoNome.Text = database.ObterKeeGroupPorId(keepass.GrupoId).GrupoNome.ToString();
        }

        void switcher_Toggled(object sender, ToggledEventArgs e)
        {
            Senha.IsPassword = !e.Value;
        }

        public void Visualizar(object sender, EventArgs args)
        {
            Senha.IsPassword = !Senha.IsPassword;
        }

        public void EditarCadastro(object sender, EventArgs args)
        {
            Database database = new Database();
            keepass kp = database.Pesquisar(lblTitulo.Text).FirstOrDefault();

            Navigation.PushAsync(new Editar(kp));
        }

        private void SalvarDados()
        {


           // App.Current.MainPage = new NavigationPage(new Grupo(keepass));
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