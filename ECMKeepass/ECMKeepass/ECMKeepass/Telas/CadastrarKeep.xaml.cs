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
	public partial class CadastrarKeep : ContentPage
	{
        private KeepGroup keepG;

        public CadastrarKeep(KeepGroup kpg)
		{
			InitializeComponent ();
            keepG = kpg;
            GrupoNome.Text = kpg.GrupoNome;
        }

        public void Visualizar(object sender, EventArgs args)
        {
            Senha.IsPassword = !Senha.IsPassword;
        }

        public void GerarSenha(object sender, EventArgs args)
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

                Senha.Text = strbld.ToString();
            }
            catch (Exception ex)
            {
                DisplayAlert("Erro", ex.Message, "Cancelar");
            }
        }

        public void SalvarAction(object sender, EventArgs args)
        {
            keepass keepass = new keepass();
            keepass.GrupoId = keepG.Id;
            keepass.Titulo = Titulo.Text;
            keepass.Usuario = Usuario.Text;
            keepass.Senha = Senha.Text;
            keepass.Url = Url.Text;
            keepass.Comentario = Comentario.Text;

            Database database = new Database();
            database.Cadastro(keepass);

            Navigation.PushAsync(new Grupo(keepG));
        }
    }
}