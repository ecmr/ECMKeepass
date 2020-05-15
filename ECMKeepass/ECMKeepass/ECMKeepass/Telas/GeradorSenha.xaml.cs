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
	public partial class GeradorSenha : ContentPage
	{
		public GeradorSenha (keepass keepass)
        {
            InitializeComponent();

            BindingContext = keepass;
            lblTituloKeep.Text = keepass.Titulo;
            txtSenha.Text = keepass.Senha;
        }

        public void Gerar(object sender, EventArgs args)
        {
            string validar = "abcdefghijklmnozABCDEFGHIJKLMNOZ1234567890@#$%&*!";

            try
            {
                StringBuilder strbld = new StringBuilder(100);
                Random random = new Random();

                for (int i = 0; i < 8; i++)
                {
                    strbld.Append(validar[random.Next(validar.Length)]);
                }

                txtSenha.Text = strbld.ToString();
            }
            catch (Exception ex)
            {
                DisplayAlert("Erro", ex.Message, "Cancelar");
            }
        }

        public void Utilizar(object sender, EventArgs args)
        {
            Database database = new Database();
            keepass kp = database.Pesquisar(lblTituloKeep.Text).FirstOrDefault();
            kp.Senha = txtSenha.Text;
            Navigation.PushAsync(new Editar(kp));
        }
    }
}