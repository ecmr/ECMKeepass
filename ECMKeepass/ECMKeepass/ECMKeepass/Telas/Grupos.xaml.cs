using ECMKeepass.Banco;
using ECMKeepass.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace ECMKeepass.Telas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Grupos : ContentPage
	{

        //List<keepass> Lista { get; set; }
        //List<keepass> ListaAgrupada { get; set; }
        //List<keepass> Lista2 = new List<keepass>();
        List<KeepGroup> grupos { get; set; }
        List<KeepGroup> Lista2 = new List<KeepGroup>();

        public Grupos ()
		{
			InitializeComponent ();

            Database database = new Database();
            grupos = database.Listar();
            BindingContext = grupos;
            ListaSenhas.ItemsSource = grupos;
        }

        //TODO: REVER
        public void GoCadastro(object sender, EventArgs args)
        {
            Navigation.PushAsync(new CadastrarGrupo());
        }

        public void GoExcluir(object sender, EventArgs args)
        {
            Database database = new Database();
            Label GrupoId = (Label)sender;
            TapGestureRecognizer tapGest = (TapGestureRecognizer)GrupoId.GestureRecognizers[0];
            KeepGroup kg = tapGest.CommandParameter as KeepGroup;

            database.ExclusaoGrupo(kg);

            Navigation.PushAsync(new Grupos());

        }


        //TODO: REVER
        public void GarregarLista(object sender, EventArgs args)
        {
            Label lblDetalhe = (Label)sender;
            TapGestureRecognizer tapGest = (TapGestureRecognizer)lblDetalhe.GestureRecognizers[0];
            keepass keep = tapGest.CommandParameter as keepass;

            Navigation.PushAsync(new Grupo(keep));
        }

        public void PesquisarAction(object sender, TextChangedEventArgs args)
        {
            ListaSenhas.ItemsSource = grupos.Where(a => a.GrupoNome.Contains(args.NewTextValue)).ToList();
        }
    }
}