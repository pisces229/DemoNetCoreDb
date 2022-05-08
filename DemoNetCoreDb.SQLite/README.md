## Framework Core Tool [https://docs.microsoft.com/zh-tw/ef/core/cli/powershell]

### Code First

`Add-Migration <Name> -Verbose`

`Update-Database <Name> -Verbose`

`Script-Migration <From.Name> <To.Name> -Verbose`

### Db First

`Scaffold-DbContext "Data Source=<dataSource>;" Microsoft.EntityFrameworkCore.Sqlite -Namespace <namespace> -UseDatabaseNames -OutputDir <path>`

`Scaffold-DbContext "Data Source=d:/Database/SQLite/DemoNetCoreDb.db;" Microsoft.EntityFrameworkCore.Sqlite -Namespace DemoNamespace -UseDatabaseNames -OutputDir ScaffoldTempTemp`

