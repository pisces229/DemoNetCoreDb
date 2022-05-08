## PostgreSQL [https://hub.docker.com/_/postgres]

`docker pull postgres`

`docker run -d -p 5432:5432 --name demo-postgres -e POSTGRES_PASSWORD=1234 postgres`

`Host=<host>;Database=<ddatabase>;Username=<username>;Password=<password>;`

`SELECT VERSION()`

> pgadmin password:1qaz@WSX

## Framework Core Tool [https://docs.microsoft.com/zh-tw/ef/core/cli/powershell]

### Code First

`Add-Migration <Name> -Verbose`

`Update-Database <Name> -Verbose`

`Script-Migration <From.Name> <To.Name> -Verbose`

### Db First

`Scaffold-DbContext "Host=<host>;Database=<database>;Username=<username>;Password=<password>;" Npgsql.EntityFrameworkCore.PostgreSQL -Namespace <namespace> -UseDatabaseNames -OutputDir <path>`

`Scaffold-DbContext "Host=localhost;Database=DemoNetCoreDb;Username=postgres;Password=1234;" Npgsql.EntityFrameworkCore.PostgreSQL  -Namespace DemoNamespace -UseDatabaseNames -OutputDir ScaffoldTempTemp`
