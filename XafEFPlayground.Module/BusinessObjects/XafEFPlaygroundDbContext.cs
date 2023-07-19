#region MIT License

// ==========================================================
// 
// XafEFPlayground project - Copyright (c) 2023 XAFers Arizona User Group
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// 
// ===========================================================

#endregion

#region usings

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Design;
using DevExpress.ExpressApp.EFCore.DesignTime;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Persistent.BaseImpl.EF.Kpi;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using DevExpress.Persistent.BaseImpl.EFCore.AuditTrail;
using EntityFramework.Exceptions.Common;
using EntityFramework.Exceptions.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using XafEFPlayground.Module.Configuration;
using XafEFPlayground.Module.Configuration.Xaf;
using XafEFPlayground.Module.Entities;

#endregion

namespace XafEFPlayground.Module.BusinessObjects;

// This code allows our Model Editor to get relevant EF Core metadata at design time.
// For details, please refer to https://supportcenter.devexpress.com/ticket/details/t933891.
public class XafEFPlaygoundContextInitializer : DbContextTypesInfoInitializerBase {
    protected override DbContext CreateDbContext() {
        var optionsBuilder = new DbContextOptionsBuilder<XafEFPlaygoundEFCoreDbContext>()
            .UseSqlServer(";")
            .UseChangeTrackingProxies()
            .UseObjectSpaceLinkProxies();
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

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken)) {
        UpdateSoftDeleteStatuses();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public override int SaveChanges() {
        UpdateSoftDeleteStatuses();

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

        try {
            return base.SaveChanges();
        }
        catch (Exception ex) {
            switch (ex) {
                case DbUpdateConcurrencyException:
                    throw new UserFriendlyException(
                        "The database operation was expected to affect 1 row(s), but actually affected 0 row(s); data may have been modified or deleted since entities were loaded.");
                case UniqueConstraintException uniqueConstraintException: {
                    // This unique constraint exception message lacks details.
                    var message = uniqueConstraintException.InnerException?.Message;
                    throw new UserFriendlyException(message);
                }
                case DbUpdateException dbUpdateException: {
                    var message = dbUpdateException.InnerException?.Message;
                    throw new UserFriendlyException(message);
                }
                default:
                    throw;
            }
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder.AddInterceptors(new BookSavingChangesInterceptor());
        optionsBuilder.UseExceptionProcessor();
    }

    private void UpdateSoftDeleteStatuses() {
        foreach (var entry in ChangeTracker.Entries()) {
            if (entry.Entity is BaseObjectWithAudit) {
                switch (entry.State) {
                    case EntityState.Added:
                        entry.CurrentValues["IsDeleted"] = false;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                        entry.CurrentValues["Deleted"] = DateTime.Now;
                        entry.CurrentValues["DeletedBy"] = SecuritySystem.Instance?.UserName;
                        break;
                }
            }
        }
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder) {
        configurationBuilder.Properties<string>().HaveColumnType("nvarchar(100)");
        configurationBuilder.Properties<decimal>().HaveColumnType("money");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);

        // XAF entity configurations
        modelBuilder.ApplyConfiguration(new ApplicationUserLoginInfoConfiguration());
        modelBuilder.ApplyConfiguration(new AuditDataItemPersistentConfiguration());
        modelBuilder.ApplyConfiguration(new AuditEfCoreWeakReferenceConfiguration());
        modelBuilder.ApplyConfiguration(new DashboardDataConfiguration());
        modelBuilder.ApplyConfiguration(new KpiDefinitionConfiguration());
        modelBuilder.ApplyConfiguration(new KpiInstanceConfiguration());
        modelBuilder.ApplyConfiguration(new ModelDifferenceAspectConfiguration());
        modelBuilder.ApplyConfiguration(new ModelDifferenceConfiguration());
        modelBuilder.ApplyConfiguration(new PermissionPolicyMemberPermissionsObjectConfiguration());
        modelBuilder.ApplyConfiguration(new PermissionPolicyNavigationPermissionObjectConfiguration());
        modelBuilder.ApplyConfiguration(new PermissionPolicyObjectPermissionsObjectConfiguration());
        modelBuilder.ApplyConfiguration(new ReportDataV2Configuration());

        // My Solution entities configurations
        modelBuilder.ApplyConfiguration(new BookConfiguration());

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

        //new BookConfiguration().Configure(modelBuilder.Entity<Book>());
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