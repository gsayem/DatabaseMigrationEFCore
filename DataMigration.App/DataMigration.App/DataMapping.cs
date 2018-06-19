using Microsoft.EntityFrameworkCore;

namespace DataMigration.App
{
    interface IEntityMapper
    {
        void MapEntity(ModelBuilder modelBuilder);        
    }
    public class BlogMapping : IEntityMapper
    {

        public void MapEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>().ToTable("Blog");
            modelBuilder.Entity<Blog>().Property(g => g.BlogId).ValueGeneratedOnAdd().UseSqlServerIdentityColumn();
            modelBuilder.Entity<Blog>().HasMany(s => s.Posts).WithOne(a => a.Blog);

        }
    }
    public class PostMapping : IEntityMapper
    {
        public void MapEntity(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Post>().ToTable("Post");
            modelBuilder.Entity<Post>().Property(g => g.PostId).ValueGeneratedOnAdd().UseSqlServerIdentityColumn();
            //modelBuilder.Entity<Post>().HasOne(a => a.Blog).WithMany(s => s.Posts);
        }

    }
}
