using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataMigration.App
{
    public class MyDBContext : DbContext//, IDataContext
    {
        protected string _connectionString;
        private readonly bool _optionsConfigured = false;

        public MyDBContext()
        {
        }

        public MyDBContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options)
        {
            _optionsConfigured = true;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (string.IsNullOrWhiteSpace(_connectionString))
            //{
            //    foreach (var item in optionsBuilder.Options.Extensions)
            //    {
            //        if (item.GetType() == typeof(Microsoft.EntityFrameworkCore.Infrastructure.Internal.SqlServerOptionsExtension))
            //        {
            //            _connectionString = ((Microsoft.EntityFrameworkCore.Infrastructure.Internal.SqlServerOptionsExtension)item).ConnectionString;
            //            break;
            //        }
            //    }
            //}
            //if (!string.IsNullOrWhiteSpace(_connectionString) && !_optionsConfigured)
            //{
            //    optionsBuilder
            //        .UseSqlServer(_connectionString)
            //        .UseSqlServerReliableConnection(new SqlServerReliableOptions { DelayMilliseconds = 500, RetryCount = 5 });
            //}

            base.OnConfiguring(optionsBuilder);
        }

        public new DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public new EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
        {
            return base.Entry<TEntity>(entity);
        }

        public new EntityEntry Entry(object entity)
        {
            return base.Entry(entity);
        }

        public IEnumerable<EntityEntry> GetChangeTrackerEntries()
        {
            return ChangeTracker.Entries();
        }

        private string GetDbConnectionString()
        {
            var dbName = "In Memory ?";

            try
            {
                //dbName = base.Database.GetDbConnection().ConnectionString;
            }
            catch
            {
                // dont care about the exception here"
            }

            return dbName;
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        
        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        public DbSet<Post> Post { get; set; }
        public DbSet<Blog> Blog { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Ignore<BaseModel>();
            //foreach (var entity in modelBuilder.Model.GetEntityTypes())
            //{
            //    //entity.Relational().TableName = entity.ClrType.Name;


            //} 
        }
    }

}
