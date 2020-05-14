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
	public partial class Grupo : ContentPage
	{

        List<keepass> Lista { get; set; }
        List<keepass> Lista2 = new List<keepass>();

        public Grupo (keepass keepass)
		{
			InitializeComponent ();

            BindingContext = keepass;

            Database database = new Database();
            Lista = database.Consultar().Where(g => g.GrupoNome.Contains(keepass.GrupoNome)).ToList();

            foreach (keepass GroupPass in Lista)
            {
                    Lista2.Add(GroupPass);
            }

            ListaSenhas.ItemsSource = null;
            ListaSenhas.ItemsSource = Lista2;

            lblNomeGrupo.Text = keepass.GrupoNome.ToString();
        }

        public void GoCadastro(object sender, EventArgs args)
        {
            Navigation.PushAsync(new Cadastrar());
        }

        public void GoMinhasSenhas(object sender, EventArgs args)
        {
            Navigation.PushAsync(new MinhasVagasCadastradas());
        }

        public void MaisDetalheAction(object sender, EventArgs args)
        {
            Label lblDetalhe = (Label)sender;
            TapGestureRecognizer tapGest = (TapGestureRecognizer)lblDetalhe.GestureRecognizers[0];
            keepass keep = tapGest.CommandParameter as keepass;

            Navigation.PushAsync(new DetalharVaga(keep));
        }

        public void PesquisarAction(object sender, TextChangedEventArgs args)
        {
            ListaSenhas.ItemsSource = Lista.Where(a => a.Titulo.Contains(args.NewTextValue)).ToList();
        }

        void OnDeleteClicked(object sender, EventArgs e)
        {
            MenuItem item = sender as MenuItem;
         }

        void OnEditClicked(object sender, EventArgs e)
        {
            MenuItem item = sender as MenuItem;
        }

    }
}