using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DemoNetCoreDb.SQLite
{
    public class Runner
    {
        private readonly ILogger<Runner> _logger;
        private readonly DemoNetCoreDbContext _dbContext;
        public Runner(ILogger<Runner> logger,
            DemoNetCoreDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }
        public void Run()
        {
            try
            {
                Task.Run(async () => await DoAction()).Wait();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }
        private async Task DoAction()
        {
            await DoCRUD();
            DoQueryableToQueryString();
            //await DoCreateUpdate();
            //await ClearCreate();
            //await DoFindInclude();
            //await DoQueryable();
            //await DoExpression();
        }
        private async Task DoCRUD()
        {
            var defaultData = new Person()
            {
                Id = "1",
                Name = "1",
                Age = 1,
                Birthday = DateOnly.FromDateTime(DateTime.Now).ToString(),
                Remark = "",
            };
            await _dbContext.People.AddAsync(defaultData);
            await _dbContext.SaveChangesAsync();
            var findDatas = await _dbContext.People.Where(w => w.Id == "1").ToListAsync();
            _logger.LogInformation($"{findDatas.Any()}");
            defaultData.Remark = Guid.NewGuid().ToString();
            _dbContext.People.Update(defaultData);
            await _dbContext.SaveChangesAsync();
            _dbContext.People.Remove(defaultData);
            await _dbContext.SaveChangesAsync();
        }
        private void DoQueryableToQueryString()
        {
            var query = _dbContext.People.AsQueryable();
            // EF.Functions.

            // string
            var value = new { first = "1", second = "2" };
            query = query
                //.Where(p => p.Name.Equals(value.first))
                //.Where(p => !p.Name.Equals(value.first))
                //.Where(p => string.Compare(p.Name, value.first) > 0)
                //.Where(p => string.Compare(p.Name, value.first) >= 0)
                //.Where(p => string.Compare(p.Name, value.first) == 0)
                //.Where(p => string.Compare(p.Name, value.first) <= 0)
                //.Where(p => string.Compare(p.Name, value.first) < 0)
                //.Where(p => p.Name.StartsWith(value.first))
                //.Where(p => !p.Name.StartsWith(value.first))
                //.Where(p => p.Name.EndsWith(value.first))
                //.Where(p => !p.Name.EndsWith(value.first))
                //.Where(p => p.Name.Contains(value.first))
                //.Where(p => !p.Name.Contains(value.first))
                //.Where(p => new List<string>() { value.first, value.second }.Contains(p.Name))
                //.Where(p => !new List<string>() { value.first, value.second }.Contains(p.Name))
                .Where(p => EF.Functions.Like(p.Name, value.first))
                .OrderBy(s => s.Row);

            // number
            //var value = new { first = 1, second = 2 };
            //query = query
            //    .Where(p => p.Age == value.first)
            //    .Where(p => p.Age != value.first)
            //    .Where(p => p.Age > value.first)
            //    .Where(p => p.Age >= value.first)
            //    .Where(p => p.Age < value.first)
            //    .Where(p => p.Age <= value.first)
            //    .Where(p => new[] { value.first, value.second }.Contains(p.Age))
            //    .Where(p => !new[] { value.first, value.second }.Contains(p.Age))
            //    .OrderBy(s => s.Row);

            // date
            //var value = new { first = DateTime.Now, second = DateTime.Now };
            //query = query
            //    .Where(p => p.Birthday == value.first)
            //    .Where(p => p.Birthday != value.first)
            //    .Where(p => p.Birthday > value.first)
            //    .Where(p => p.Birthday >= value.first)
            //    .Where(p => p.Birthday < value.first)
            //    .Where(p => p.Birthday <= value.first)
            //    .Where(p => new[] { value.first, value.second }.Contains(p.Birthday))
            //    .Where(p => !new[] { value.first, value.second }.Contains(p.Birthday))
            //    .OrderBy(s => s.Row);

            Console.WriteLine(query.ToQueryString().Replace("AND", $"{Environment.NewLine}AND"));
        }
    }
}
