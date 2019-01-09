using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace DBHandler
{
    public class Command
    {
        public static void Insert(string s)
        {
            using (Context dbContext = new Context())
            {
                string query = "INSERT INTO Clip (content) VALUES (@content)";
                SqliteParameter content = new SqliteParameter("@content", s);
                dbContext.Database.ExecuteSqlCommand(query, content);
            }
        }
    }
}
