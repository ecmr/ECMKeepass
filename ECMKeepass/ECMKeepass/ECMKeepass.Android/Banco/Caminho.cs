using ECMKeepass.Banco;
using ECMKeepass.Droid.Banco;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(Caminho))]
namespace ECMKeepass.Droid.Banco
{
    public class Caminho : ICaminho
    {
        public string ObterCaminho(string NomeArquivoBanco)
        {
            string caminhoDaPasta = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

            string caminhoBanco = Path.Combine(caminhoDaPasta, NomeArquivoBanco);

            return caminhoBanco;
        }
    }
}