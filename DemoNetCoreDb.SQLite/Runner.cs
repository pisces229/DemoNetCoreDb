using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DemoNetCoreDb.SQLite
{
    public class Runner
    {
        private readonly DemoNetCoreDbContext _dbContext;
        public Runner(DemoNetCoreDbContext dbContext)
        {
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
        public async Task DoAction()
        {
            await DoQueryableToQueryString();
            //await DoCreateUpdate();
            //await ClearCreate();
            //await DoFindInclude();
            //await DoQueryable();
            //await DoExpression();
        }
        private async Task DoQueryableToQueryString()
        {
            await Task.FromResult("DoQueryableToScript");
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
        private async Task DoCreateUpdate()
        {
            //_dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
            //_dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            // clear record
            var Person = new Person()
            {
                //Row = 27,
                Id = "1",
                Name = "1",
                Age = 1,
                Birthday = DateOnly.FromDateTime(DateTime.Now).ToString(),
                Remark = "",
            };
            // --------------------------------------------------
            // Add
            _dbContext.People.Add(Person);
            //_dbContext.Set<Person>().Add(Person);
            //_dbContext.Add(Person);
            // Attach: EntityState.Added
            //_dbContext.People.Attach(Person).State = EntityState.Added;
            // --------------------------------------------------
            await _dbContext.SaveChangesAsync();
            // QueryTrackingBehavior
            //Person = await _dbContext.Set<Person>().FirstAsync();
            // QueryTrackingBehavior
            Person.Remark = Guid.NewGuid().ToString();
            // --------------------------------------------------
            // Update
            _dbContext.People.Update(Person);
            //_dbContext.Set<Person>().Update(Person);
            //_dbContext.Update(Person);
            // Attach: EntityState.Modified
            //_dbContext.People.Attach(Person).State = EntityState.Modified;
            // --------------------------------------------------
            await _dbContext.SaveChangesAsync();
        }
        private async Task ClearCreate()
        {
            //_dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
            //_dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dbContext.RemoveRange(await _dbContext.People.ToListAsync());
            await _dbContext.SaveChangesAsync();
            for (var i = 1; i < 5; ++i)
            {
                _dbContext.People.Add(new Person()
                {
                    Id = $"F{i}",
                    Name = $"F[{i}]",
                    Age = i,
                    Birthday = DateOnly.FromDateTime(DateTime.Now).ToString(),
                    Remark = Guid.NewGuid().ToString(),
                });
            }
            await _dbContext.SaveChangesAsync();
            var Person = await _dbContext.People.ToListAsync();
            Person.ForEach(f =>
            {
                for (var i = 1; i < 5; ++i)
                {
                    _dbContext.Addresses.Add(new Address()
                    {
                        Id = f.Id,
                        Text = $"{f.Name}[{i}]",
                    });
                }
            });
            await _dbContext.SaveChangesAsync();
        }
        private async Task DoFindInclude()
        {
            //_dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
            //_dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            {
                var Person = await _dbContext.People
                    .Where(p => p.Id == "A123456789")
                    // QueryTrackingBehavior
                    .Include(p => p.Addresses)
                    // QueryTrackingBehavior
                    .ToListAsync();
                Console.WriteLine($"Person.Count():{ Person.Count() }");
                Console.WriteLine($"Person.First().Addresses.Count():{ Person.First().Addresses.Count() }");
            }
            {
                var Person = await _dbContext.People
                    .Where(p => p.Id == "A123456789")
                    // QueryTrackingBehavior
                    //.Include(p => p.Addresses)
                    // QueryTrackingBehavior
                    .ToListAsync();
                Console.WriteLine($"Person.Count():{ Person.Count() }");
                Console.WriteLine($"Person.First().Addresses.Count():{ Person.First().Addresses.Count() }");
            }
        }
        private async Task DoQueryable()
        {
            _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            {
                // Where
                Expression<Func<Person, bool>> predicate = (p) => p.Id == "1";
                Console.WriteLine($"ToQueryString:{_dbContext.People.Where(predicate).ToQueryString()}");
                await _dbContext.People.Where(predicate).ToListAsync();
            }
            {
                // No Where
                Func<Person, bool> predicate = (p) => p.Id == "1";
                _dbContext.People.Where(predicate).ToList();
            }
            {
                await _dbContext.People.Skip(1).Take(2).ToListAsync();
            }
            {
                Func<IQueryable<Person>, IQueryable<Person>> delegateQuery = (query) =>
                {
                    query = query.Where(p => p.Id == "1");
                    query = query.Where(p => p.Id == "2");
                    query = query.OrderBy(e => e.Row).Include(e => e.Addresses);
                    return query;
                };
                await delegateQuery(_dbContext.People.AsQueryable()).ToListAsync();
            }
            {
                List<Expression<Func<Person, bool>>> expressionWheres = new List<Expression<Func<Person, bool>>>();
                expressionWheres.Add(p => p.Id == "aaaa");
                expressionWheres.Add(p => p.Id == "bbbb");
                var query = _dbContext.People.AsQueryable();
                expressionWheres.ForEach(expression =>
                {
                    query = query.Where(expression);
                });
                query = query.OrderBy(s => s.Row);
                await query.AnyAsync();
                await query.CountAsync();
                await query.ToListAsync();
            }
            //{
            //    await _dbContext.People.FirstOrDefaultAsync(a => a.Name == "6666");
            //    await _dbContext.People.SingleOrDefaultAsync(a => a.Name == "6666");
            //    await _dbContext.People.FromSqlRaw("select * from Person where 1=1").ToListAsync();
            //}
        }
        private async Task DoExpression()
        {
            Expression<Func<Person, bool>> expressionWhere = (e) => e.Name == "1";
            Func<Person, bool> func = (e) => e.Name == "1";
            Expression<Func<IQueryable<Person>>> expression = () => _dbContext.People.AsQueryable().Where(p => true);
            var a = expression.Compile()().Where(p => true);
            await _dbContext.FromExpression(expression).ToListAsync();
        }
    }
}
