using ECMKeepass.Banco;
using ECMKeepass.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ECMKeepass.Telas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CadastrarGrupo : ContentPage
	{
		public CadastrarGrupo ()
		{
			InitializeComponent ();
		}

        public void SalvarAction(object sender, EventArgs args)
        {
            Database database = new Database();
            KeepGroup kg = new KeepGroup() { GrupoNome = txtGrupoNome.Text };
             database.CadastroGrupo(kg);

            Navigation.PushAsync(new Grupos());
        }

    }
}