// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using XafEFPlayground.Module.BusinessObjects;

// ToDo Support file logging
// ToDo Support script generation

var configuration = new ConfigurationBuilder().AddJsonFile($"appsettings.json");

var config = configuration.Build();
var connectionString = config.GetConnectionString("ConnectionString");

ArgumentNullException.ThrowIfNull(connectionString);

var optionsBuilder = new DbContextOptionsBuilder<XafEFPlaygoundEFCoreDbContext>();
optionsBuilder.UseSqlServer(connectionString);
optionsBuilder.UseChangeTrackingProxies();
optionsBuilder.UseObjectSpaceLinkProxies();
optionsBuilder.LogTo(
        Console.WriteLine,
        new[] { DbLoggerCategory.Database.Command.Name },
        LogLevel.Information)
    .EnableSensitiveDataLogging(Debugger.IsAttached);

using var context = new XafEFPlaygoundEFCoreDbContext(optionsBuilder.Options);
context.Database.Migrate();

//Console.ReadKey();

return 0;