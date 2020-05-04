
using ECMKeepass.Modelo;
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
            //lblVisualizar.Text = String.Format("Switch is now {0}", e.Value);
            Senha.IsPassword = !e.Value;
        }
    }
}