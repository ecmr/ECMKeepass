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
	public partial class Consultar : ContentPage
	{
        List<keepass> Lista { get; set; }
        List<keepass> Lista2 = new List<keepass>();

        public Consultar()
        {
            InitializeComponent();

            Database database = new Database();
            Lista = database.Consultar();

            ListaSenhas.ItemsSource = Lista;

            lblCount.Text = "Grupos: " + Lista.Count.ToString();

        }

        public void GoCadastro(object sender, EventArgs args)
        {
            //Navigation.PushAsync(new Cadastrar());
        }

        public void GoMinhasSenhas(object sender, EventArgs args)
        {
            //Navigation.PushAsync(new MinhasVagasCadastradas());
        }

        public void MaisDetalheAction(object sender, EventArgs args)
        {
            Label lblDetalhe = (Label)sender;
            TapGestureRecognizer tapGest = (TapGestureRecognizer)lblDetalhe.GestureRecognizers[0];
            keepass keep = tapGest.CommandParameter as keepass;

            Navigation.PushAsync(new Detalhar(keep));
        }

        public void PesquisarAction(object sender, TextChangedEventArgs args)
        {
            ListaSenhas.ItemsSource = Lista.Where(a => a.Titulo.Contains(args.NewTextValue)).ToList();
        }
    }
}