using MCB.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace MCB.Tests.Data
{
    public static class DbSettingHellper
    {
        public static DbContextOptions<MCBContext> GetDbOptions()
        {
            var connectionStringBuilder =
                             new SqliteConnectionStringBuilder { DataSource = ":memory:" };
            var connection = new SqliteConnection(connectionStringBuilder.ToString());

            var options = new DbContextOptionsBuilder<MCBContext>()
                .UseSqlite(connection)
                .Options;
            return options;
        }
    }
}
