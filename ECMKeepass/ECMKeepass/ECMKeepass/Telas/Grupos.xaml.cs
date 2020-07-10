using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Constants = ECMKeepass.AppConstant.Constants;
using ECMKeepass.AuthHelpers;
using ECMKeepass.Banco;
using ECMKeepass.Modelo;
using Xamarin.Essentials;

namespace ECMKeepass.Telas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Grupos : ContentPage
	{
        static Account account;
        static AccountStore store;

        static string[] Scopes = { DriveService.Scope.Drive };

        //List<keepass> Lista { get; set; }
        //List<keepass> ListaAgrupada { get; set; }
        //List<keepass> Lista2 = new List<keepass>();
        List<KeepGroup> grupos { get; set; }
        List<KeepGroup> Lista2 = new List<KeepGroup>();

        public Grupos ()
		{
			InitializeComponent ();

            store = AccountStore.Create();

            Database database = new Database();
            grupos = database.Listar();
            BindingContext = grupos;
            ListaSenhas.ItemsSource = grupos;

            Passo1LoginGoogle();

            //UserCredential credential;

            //credential = Autenticar();

            //var service = new DriveService(new Google.Apis.Services.BaseClientService.Initializer()
            //{
            //    HttpClientInitializer = credential,
            //    ApplicationName = "ECM Keepass",
            //});

            //string pagToken = null;

            //do
            //{
            //    ListFiles(service, ref pagToken);
            //} while (pagToken!= null);

        }

        private static void Passo1LoginGoogle()
        {
            string clientId = null;
            string redirectUrl = null;
            string AuthorizeUrl = null;
            string AccessTokenUrl = null;
            string Scope = null;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    clientId = Constants.IOSClientId;
                    redirectUrl = Constants.IOSRedirectUrl;
                    break;

                case Device.Android:
                    clientId = Constants.AndroidClientId;
                    redirectUrl = Constants.AndroidRedirectUrl;
                    AuthorizeUrl = Constants.AuthorizeUrl;
                    AccessTokenUrl = Constants.AccessTokenUrl;
                    Scope = Constants.Scope;
                    break;

                default:
                    break;
            }

            account = store.FindAccountsForService(Constants.AppName).FirstOrDefault();

            var authenticator = new OAuth2Authenticator(
                clientId,
                null,
                Scope,
                new Uri(AuthorizeUrl),
                new Uri(redirectUrl),
                new Uri(AccessTokenUrl),
                null,
                true);

            authenticator.Completed += OnAuthCompleted;
            authenticator.Error += OnAuthError;

            AuthenticationState.Authenticator = authenticator;

            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(authenticator);

        }

        async static void OnAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            var authenticator = sender as OAuth1Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
            }

            User user = null;
            if (e.IsAuthenticated)
            {
                var request = new OAuth2Request("GET", new Uri(Constants.UserInfoUrl), null, e.Account);
                var response = await request.GetResponseAsync();

                if (response != null)
                {
                    string userJson = await response.GetResponseTextAsync();
                    user = JsonConvert.DeserializeObject<User>(userJson);
                }

                if (account != null)
                {
                    store.Delete(account, Constants.AppName);
                }

                await store.SaveAsync(account = e.Account, Constants.AppName);
                //await DisplayAlert("Email address", user.Email, "OK");
            }
        }

        static void OnAuthError(object sender, AuthenticatorErrorEventArgs e)
        {
            var authenticator = sender as OAuth1Authenticator;
            if(authenticator != null)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
            }

            Debug.WriteLine("Authentication error: " + e.Message);
        }



        private static void ListFiles(DriveService service, ref string pageToken)
        {
            FilesResource.ListRequest listRequest = service.Files.List();
            listRequest.PageSize = 1;
            listRequest.Fields = "nextPageToken, files(name)";
            listRequest.PageToken = pageToken;
            listRequest.Q = "mimeType='image/jpeg'";

            var request = listRequest.Execute();

            if (request.Files != null && request.Files.Count > 0)
            {
                foreach (var file in request.Files)
                {
                    Console.WriteLine("{0}", file.Name);
                }
                pageToken = request.NextPageToken;

                if (request.NextPageToken != null)
                {
                    Console.WriteLine("Press to conti...");
                    Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine("No files found...");
            }
        }

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

            List<keepass> lista = database.Consultar().Where(i => i.GrupoId == kg.Id).ToList();

            if (lista.Count > 0)
            {
                DisplayAlert("Atenão", "Esta lista não está vazia", "Ok");
                return;
            }

            database.ExclusaoGrupo(kg);

            Navigation.PushAsync(new Grupos());

        }

        async void OnAlertYesNoClicked(object sender, EventArgs e)
        {
            Database database = new Database();
            Label GrupoId = (Label)sender;
            TapGestureRecognizer tapGest = (TapGestureRecognizer)GrupoId.GestureRecognizers[0];
            KeepGroup kg = tapGest.CommandParameter as KeepGroup;

            List<keepass> lista = database.Consultar().Where(i => i.GrupoId == kg.Id).ToList();

            if (lista.Count > 0)
            {
                await DisplayAlert("Atenão", "Esta lista não está vazia", "Ok");
                return;
            }

            bool answer = await DisplayAlert("Atenção?", "O grupo -" + kg.GrupoNome + "- Será excluído!", "Confirmar", "Cancelar");
            if (answer)
            {
                database.ExclusaoGrupo(kg);

                await Navigation.PushAsync(new Grupos());

            }
        }

        public void GarregarLista(object sender, EventArgs args)
        {
            Label lblDetalhe = (Label)sender;
            TapGestureRecognizer tapGest = (TapGestureRecognizer)lblDetalhe.GestureRecognizers[0];
            KeepGroup grupo = tapGest.CommandParameter as KeepGroup;


            Navigation.PushAsync(new Grupo(grupo));
        }

        public void PesquisarAction(object sender, TextChangedEventArgs args)
        {
            ListaSenhas.ItemsSource = grupos.Where(a => a.GrupoNome.Contains(args.NewTextValue)).ToList();
        }

        private static Google.Apis.Auth.OAuth2.UserCredential Autenticar()
        {
            Google.Apis.Auth.OAuth2.UserCredential credenciais;

            using (var stream = new System.IO.FileStream("credentials.json", System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {
                string creadPath = "C:\\Users\\edine\\Google Drive\\"; //System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                creadPath = System.IO.Path.Combine(creadPath, "._TEMP/drive-dotnet-quickstart.json");
     
                credenciais = Google.Apis.Auth.OAuth2.GoogleWebAuthorizationBroker.AuthorizeAsync(
                    Google.Apis.Auth.OAuth2.GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    System.Threading.CancellationToken.None,
                    new Google.Apis.Util.Store.FileDataStore(creadPath, true)).Result;
            }

            return credenciais;
        }

        private static Google.Apis.Drive.v3.DriveService AbrirServico(Google.Apis.Auth.OAuth2.UserCredential credenciais)
        {
            return new Google.Apis.Drive.v3.DriveService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                HttpClientInitializer = credenciais
            });
        }
    }
}