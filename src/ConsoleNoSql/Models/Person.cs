using ConsoleNoSql.Helpers;

namespace ConsoleNoSql.Models
{
    public class Person : EntityMongoDb
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
