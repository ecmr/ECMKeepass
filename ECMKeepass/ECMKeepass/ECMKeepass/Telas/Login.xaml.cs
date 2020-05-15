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
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();

            PrimeiroLogin();
        }

        private void PrimeiroLogin()
        {
            Database database = new Database();

            List<UserAcess> lista = database.ConsultarPrimeiroAcesso().ToList();
            if (lista.Count < 1)
            {
                lblMessage.Text = "Crie um usuário e senha para seu primeiro acesso!";
                btnLogar.Text = "Criar";
            }
        }

        public void CadastroPrimeiroAcesso()
        {
            Database database = new Database();

            if (CamposIsValid())
            {
                UserAcess UA = new UserAcess() { Usuario = MailPhone.Text, Senha = PassWord.Text };
                database.CadastroPrimeiroACesso(UA);
                lblMessage.Text = "Bem vindo!";
                Navigation.PushAsync(new Grupos());
            }
            else
            {
                lblMessage.Text = "Preencha os campos!";
            }
        }

        private bool CamposIsValid()
        {
            if (string.IsNullOrEmpty(MailPhone.Text) && string.IsNullOrEmpty(PassWord.Text))
                return false;
            else
                return true;
        }

        public void Logar(object sender, EventArgs args)
        {
            if (btnLogar.Text == "Criar")
            {
                CadastroPrimeiroAcesso();
                return;
            }

            Database database = new Database();

            if (!CamposIsValid())
            {
                lblMessage.Text = "Preencha os campos!";
            }
            else
            {
                UserAcess user = new UserAcess() { Usuario = MailPhone.Text, Senha = PassWord.Text };

                if (database.PequisaUsuario(user))
                {
                    lblMessage.Text = "Bem vindo!";
                    Navigation.PushAsync(new Grupos());
                }
                else
                {
                    lblMessage.Text = "Usuário não localizado!";
                }
            }

        }

        void switcher_Toggled(object sender, ToggledEventArgs e)
        {
            PassWord.IsPassword = !e.Value;
        }
    }
}