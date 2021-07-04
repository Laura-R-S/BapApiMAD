using Microsoft.EntityFrameworkCore;

namespace BapApi.Models
{
    /// <summary>
    /// DATE: 04/07/2021 
    /// 
    /// [1] A DbSet represents the collection of all entities in the context, or that can be queried from the database,
    /// of a given type. DbSet objects are created from a DbContext using the DbContext.Set method.
    /// Data querying in Entity Framework Core is performed against the DbSet properties of the DbContext. The DbSet represents a 
    /// collection of entities of a specific type - the type specified by the type parameter.      
    /// Queries are specified using Language Integrated Query(LINQ), a component in the.NET Framework that provides query capability
    /// against collections in C# or VB. LINQ queries can be written using query syntax or method syntax. Query syntax shares a
    /// resemblance with SQL. The EF Core provider that you use is responsible for translating the LINQ query into the actual SQL
    /// to be executed against the database.
    /// https://docs.microsoft.com/en-us/dotnet/api/system.data.entity.dbset-1?view=entity-framework-6.2.0
    /// https://www.learnentityframeworkcore.com/dbset/querying-data
    /// </summary>

    public class StoreAppsContext : DbContext
    {
        public DbSet<StoreApp> StoreApps { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(@"Data Source=storeapps.db");
    }
}
