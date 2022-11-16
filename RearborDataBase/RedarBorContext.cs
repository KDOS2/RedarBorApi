
namespace RedarBorDB
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.Configuration;
    using RedarBorModels.Entities;

    /// <summary>
    /// clase que accede a la base de datos RedarBoar
    /// </summary>
    public class RedarBorContext : DbContext
    {

        public RedarBorContext(DbContextOptions<RedarBorContext> options) : base(options)
        { }

        #region "Datasets"
        /// <summary>
        /// Define el obgeto que tendra acceso a la tabla Employees
        /// </summary>
        public DbSet<RedarBorEmployeesEntity> Employees { get; set; }
        #endregion

        #region DbContext overrides

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RedarBorContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        #endregion

        public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<RedarBorContext>
        {
            public RedarBorContext CreateDbContext(string[] Args)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile(Directory.GetCurrentDirectory() + "./appsettings.json")
                    .Build();
                var builder = new DbContextOptionsBuilder<RedarBorContext>();
                var connectionString = configuration.GetConnectionString("RedarborConnection");
                builder.UseSqlServer(connectionString);
                return new RedarBorContext(builder.Options);
            }
        }

        public override int SaveChanges()
        {
            var changes = from e in this.ChangeTracker.Entries()
                          where e.State != EntityState.Unchanged
                          select e;
            var entris = this.ChangeTracker.Entries();

            foreach (var change in changes)
            {
                if (change.State == EntityState.Added)
                { }
                else if (change.State == EntityState.Modified)
                { }
                else if (change.State == EntityState.Deleted)
                { }
            }
            return base.SaveChanges();
        }
    }
}
