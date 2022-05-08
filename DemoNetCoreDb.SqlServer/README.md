## Framework Core Tool [https://docs.microsoft.com/zh-tw/ef/core/cli/powershell]

### Code First

`Add-Migration <Name> -Verbose`

`Update-Database <Name> -Verbose`

`Script-Migration <From.Name> <To.Name> -Verbose`

### Db First

`Scaffold-DbContext "Server=<server>;Database=<database>;User ID=<userId>;Password=<password>;" Microsoft.EntityFrameworkCore.SqlServer -Namespace <namespace> -UseDatabaseNames -OutputDir <path>`

`Scaffold-DbContext "Server=(LocalDB)\MSSQLLocalDB;Database=DemoNetCoreDb;User ID=sa;Password=1qaz@WSX;" Microsoft.EntityFrameworkCore.SqlServer -Namespace DemoNamespace -UseDatabaseNames -OutputDir ScaffoldTemp`
