using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;


namespace DataMigration.App
{
    class DBContextFactory : IDesignTimeDbContextFactory<MyDBContext>
    {

        public MyDBContext CreateDbContext(string connectionString)
        {
            var builder = new DbContextOptionsBuilder<MyDBContext>().UseSqlServer(connectionString);

            builder.UseMyModel();

            return new MyDBContext(builder.Options);
        }
        /// <summary>
        ///Called by dotnet ef database update.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public MyDBContext CreateDbContext(string[] args)
        {
            return CreateDbContext(Environment.GetEnvironmentVariable("WebApiSqlConnection"));
        }
    }
}
