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

using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using XafEFPlayground.Module.BusinessObjects;

#endregion

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