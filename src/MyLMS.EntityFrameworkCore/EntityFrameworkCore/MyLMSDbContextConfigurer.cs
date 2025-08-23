using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace MyLMS.EntityFrameworkCore;

public static class MyLMSDbContextConfigurer
{
    public static void Configure(DbContextOptionsBuilder<MyLMSDbContext> builder, string connectionString)
    {
        builder.UseSqlServer(connectionString);
    }

    public static void Configure(DbContextOptionsBuilder<MyLMSDbContext> builder, DbConnection connection)
    {
        builder.UseSqlServer(connection);
    }
}
