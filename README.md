# XafEFPlaygound

XafPlayground is a sample project to demonstrate the use of Entity Framework Core in XAF.

This solution makes use of the following additional modules:
- SonarAnalyzer for C# (https://rules.sonarsource.com/csharp/)
  - Configuration is done global within the file: Directory.Build.props. In production we set TreatWarningsAsErrors and CodeAnalysisTreatWarningsAsErrors to true.
- Entity Framework Core Exceptions for MS SQL Server
  - Instead of using DbUpdateException a more specific exception like UniqueConstraintException can be used.
- DBMigration setup with powershell within XafEfPlaygound.Module folder
  - run following command: dotnet ef migrations add InitialSetup --context XafEFPlaygoundEFCoreDbContext
  - this will create migration file(s) with the Migrations folder
  - run DBMigrator project to create or update your database
- ...

## Features

- [x] #739
- [ ] #740
- [ ] #741

## Features ToDo

- [ ] https://github.com/octo-org/octo-repo/issues/740
- [ ] Add logic/code for soft-delete support :tada:
- [ ] Add logic/code concurrency support :tada:

## Discussions

- [ ] Data-seeding
- [ ] Use of DBContext
- 
