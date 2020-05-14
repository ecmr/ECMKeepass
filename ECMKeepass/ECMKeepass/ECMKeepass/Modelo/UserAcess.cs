using SQLite;

namespace ECMKeepass.Modelo
{
    [Table("UserAcess")]
    public class UserAcess
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
    }
}