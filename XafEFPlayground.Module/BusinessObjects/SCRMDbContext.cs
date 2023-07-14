using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Design;
using DevExpress.ExpressApp.EFCore.DesignTime;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Persistent.BaseImpl.EF.Kpi;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using DevExpress.Persistent.BaseImpl.EFCore.AuditTrail;
using EntityFramework.Exceptions.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using XafEFPlayground.Module.Configuration;
using XafEFPlayground.Module.Entities;

namespace XafEFPlayground.Module.BusinessObjects;

// This code allows our Model Editor to get relevant EF Core metadata at design time.
// For details, please refer to https://supportcenter.devexpress.com/ticket/details/t933891.
public class XafEFPlaygoundContextInitializer : DbContextTypesInfoInitializerBase {
    protected override DbContext CreateDbContext() {
        var optionsBuilder = new DbContextOptionsBuilder<XafEFPlaygoundEFCoreDbContext>()
            .UseSqlServer(";")
            .UseChangeTrackingProxies()
            .UseObjectSpaceLinkProxies()
            .UseExceptionProcessor();
        return new XafEFPlaygoundEFCoreDbContext(optionsBuilder.Options);
    }
}

//This factory creates DbContext for design-time services. For example, it is required for database migration.
public class XafEFPlaygoundDesignTimeDbContextFactory : IDesignTimeDbContextFactory<XafEFPlaygoundEFCoreDbContext> {
    public XafEFPlaygoundEFCoreDbContext CreateDbContext(string[] args) {
        //throw new InvalidOperationException("Make sure that the database connection string and connection provider are correct. After that, uncomment the code below and remove this exception.");
        var optionsBuilder = new DbContextOptionsBuilder<XafEFPlaygoundEFCoreDbContext>();
        optionsBuilder.UseSqlServer("Integrated Security=SSPI;Data Source=(localdb)\\mssqllocaldb;Initial Catalog=XafEFPlaygound");
        optionsBuilder.UseChangeTrackingProxies();
        optionsBuilder.UseObjectSpaceLinkProxies();
        return new XafEFPlaygoundEFCoreDbContext(optionsBuilder.Options);
    }
}

[TypesInfoInitializer(typeof(XafEFPlaygoundContextInitializer))]
public class XafEFPlaygoundEFCoreDbContext : DbContext {
    public XafEFPlaygoundEFCoreDbContext(DbContextOptions<XafEFPlaygoundEFCoreDbContext> options) : base(options) { }

    //public DbSet<ModuleInfo> ModulesInfo { get; set; }
    public DbSet<ModelDifference> ModelDifferences { get; set; }
    public DbSet<ModelDifferenceAspect> ModelDifferenceAspects { get; set; }
    public DbSet<PermissionPolicyRole> Roles { get; set; }
    public DbSet<ApplicationUser> Users { get; set; }
    public DbSet<ApplicationUserLoginInfo> UserLoginInfos { get; set; }
    public DbSet<FileData> FileData { get; set; }
    public DbSet<ReportDataV2> ReportDataV2 { get; set; }
    public DbSet<KpiDefinition> KpiDefinition { get; set; }
    public DbSet<KpiInstance> KpiInstance { get; set; }
    public DbSet<KpiHistoryItem> KpiHistoryItem { get; set; }
    public DbSet<KpiScorecard> KpiScorecard { get; set; }
    public DbSet<DashboardData> DashboardData { get; set; }
    public DbSet<AuditDataItemPersistent> AuditData { get; set; }
    public DbSet<AuditEFCoreWeakReference> AuditEFCoreWeakReference { get; set; }
    public DbSet<Event> Event { get; set; }
    public DbSet<Analysis> Analysis { get; set; }

    public DbSet<Book> Book { get; set; }

    public override int SaveChanges() {
        var entities = ChangeTracker
            .Entries()
            .Where(e => e.Entity is IAuditedObject && (
                e.State == EntityState.Added ||
                e.State == EntityState.Modified ||
                e.State == EntityState.Deleted));

        foreach (var entity in entities) {
            switch (entity.State) {
                case EntityState.Detached:
                    break;
                case EntityState.Unchanged:
                    break;
                case EntityState.Deleted:
                    ((IAuditedObject)entity.Entity).Deleted = DateTime.Now;
                    ((IAuditedObject)entity.Entity).DeletedBy = SecuritySystem.Instance?.UserName ?? "(Automated)";
                    break;
                case EntityState.Modified:
                    ((IAuditedObject)entity.Entity).LastModified = DateTime.Now;
                    ((IAuditedObject)entity.Entity).LastModifiedBy = SecuritySystem.Instance?.UserName ?? "(Automated)";
                    break;
                case EntityState.Added:
                    ((IAuditedObject)entity.Entity).Created = DateTime.Now;
                    ((IAuditedObject)entity.Entity).CreatedBy = SecuritySystem.Instance?.UserName ?? "(Automated)";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        return base.SaveChanges();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasChangeTrackingStrategy(ChangeTrackingStrategy.ChangingAndChangedNotificationsWithOriginalValues);
        modelBuilder.UsePropertyAccessMode(PropertyAccessMode.PreferFieldDuringConstruction);
        modelBuilder.Entity<ApplicationUserLoginInfo>(b => {
            b.HasIndex(nameof(DevExpress.ExpressApp.Security.ISecurityUserLoginInfo.LoginProviderName),
                nameof(DevExpress.ExpressApp.Security.ISecurityUserLoginInfo.ProviderUserKey)).IsUnique();
        });
        modelBuilder.Entity<AuditEFCoreWeakReference>()
            .HasMany(p => p.AuditItems)
            .WithOne(p => p.AuditedObject);
        modelBuilder.Entity<AuditEFCoreWeakReference>()
            .HasMany(p => p.OldItems)
            .WithOne(p => p.OldObject);
        modelBuilder.Entity<AuditEFCoreWeakReference>()
            .HasMany(p => p.NewItems)
            .WithOne(p => p.NewObject);
        modelBuilder.Entity<AuditEFCoreWeakReference>()
            .HasMany(p => p.UserItems)
            .WithOne(p => p.UserObject);
        modelBuilder.Entity<ModelDifference>()
            .HasMany(t => t.Aspects)
            .WithOne(t => t.Owner)
            .OnDelete(DeleteBehavior.Cascade);
        
        /* Configure your own tables/entities inside here */

        BookConfiguration.BookConfuguration(modelBuilder);
    }
}

public class XafEFPlaygoundAuditingDbContext : DbContext {
    public XafEFPlaygoundAuditingDbContext(DbContextOptions<XafEFPlaygoundAuditingDbContext> options) : base(options) { }
    public DbSet<AuditDataItemPersistent> AuditData { get; set; }
    public DbSet<AuditEFCoreWeakReference> AuditEFCoreWeakReference { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasChangeTrackingStrategy(ChangeTrackingStrategy.ChangingAndChangedNotificationsWithOriginalValues);
        modelBuilder.Entity<AuditEFCoreWeakReference>()
            .HasMany(p => p.AuditItems)
            .WithOne(p => p.AuditedObject);
        modelBuilder.Entity<AuditEFCoreWeakReference>()
            .HasMany(p => p.OldItems)
            .WithOne(p => p.OldObject);
        modelBuilder.Entity<AuditEFCoreWeakReference>()
            .HasMany(p => p.NewItems)
            .WithOne(p => p.NewObject);
        modelBuilder.Entity<AuditEFCoreWeakReference>()
            .HasMany(p => p.UserItems)
            .WithOne(p => p.UserObject);
    }
}