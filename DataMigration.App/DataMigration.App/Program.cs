using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataMigration.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string connStr = Environment.GetEnvironmentVariable("WebApiSqlConnection");

            if (String.IsNullOrWhiteSpace(connStr))
            {
                Console.WriteLine("No WebApiSqlConnection environment variable found.");
                Console.WriteLine("Please set this variable to a connection string for the main database.");
                return;
            }
            List<string> param = new List<string>();
            param.Add(connStr);

            using (var context = new DBContextFactory().CreateDbContext(connStr))
            using (var dbConn = context.Database.GetDbConnection())
            {
                Console.WriteLine($"This will migrate {dbConn.DataSource}\\{dbConn.Database}. Continue?");
                
                if (Console.ReadLine().ToLower() == "y")
                {
                    Console.WriteLine(context.Database.GetMigrations().Count() + " migration(s) found.");
                    Console.WriteLine(context.Database.GetAppliedMigrations().Count() + " migration(s) applied found.");
                    Console.WriteLine(context.Database.GetPendingMigrations().Count() + " to be applied.");
                    context.Database.Migrate();
                    Console.WriteLine("Done");
                }
                else
                {
                    Console.WriteLine("Cancelled");
                }

                Console.ReadKey();
            }
        }
    }
}
