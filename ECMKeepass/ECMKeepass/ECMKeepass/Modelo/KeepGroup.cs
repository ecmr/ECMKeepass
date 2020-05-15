using SQLite;

namespace ECMKeepass.Modelo
{
    [Table("KeepGroup")]
    public class KeepGroup
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string GrupoNome { get; set; }
    }
}
