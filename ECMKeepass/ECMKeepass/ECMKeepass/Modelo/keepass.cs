using SQLite;

namespace ECMKeepass.Modelo
{
    [Table("KeePass")]
    public class keepass
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int GrupoId { get; set; }
        public string GrupoNome { get; set; }
        public string Titulo { get; set; }
        public string Icone { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public string Url { get; set; }
        public string Comentario { get; set; }
    }
}
