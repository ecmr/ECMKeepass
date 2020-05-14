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

        List<keepass> Lista { get; set; }
        List<keepass> ListaAgrupada { get; set; }
        List<keepass> Lista2 = new List<keepass>();

        public Grupos ()
		{
			InitializeComponent ();

            Database database = new Database();
            Lista = database.Consultar();

            foreach (keepass GroupPass in Lista)
            {
                List<keepass> keepassItem = Lista2.Where(g => g.GrupoNome.Contains(GroupPass.GrupoNome)).ToList();
                if (keepassItem.Count() < 1)
                    Lista2.Add(new keepass() { Id = GroupPass.Id, GrupoNome = GroupPass.GrupoNome });
            }

            ListaSenhas.ItemsSource = Lista2;
        }


        public void GoCadastro(object sender, EventArgs args)
        {
            Navigation.PushAsync(new Cadastrar());
        }

        public void MaisDetalheAction(object sender, EventArgs args)
        {
            Label lblDetalhe = (Label)sender;
            TapGestureRecognizer tapGest = (TapGestureRecognizer)lblDetalhe.GestureRecognizers[0];
            keepass keep = tapGest.CommandParameter as keepass;

            Navigation.PushAsync(new Grupo(keep));
        }

        public void PesquisarAction(object sender, TextChangedEventArgs args)
        {
            ListaSenhas.ItemsSource = Lista.Where(a => a.Titulo.Contains(args.NewTextValue)).ToList();
        }
    }
}