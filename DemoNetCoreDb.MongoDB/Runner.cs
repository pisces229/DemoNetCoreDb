using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace DemoNetCoreDb.MongoDB
{
    public class Runner
    {
        private readonly ILogger<Runner> _logger;
        private readonly DefaultMongoClient _defaultMongoClient;
        private readonly IClientSessionHandle _clientSessionHandle;
        public Runner(ILogger<Runner> logger,
            DefaultMongoClient defaultMongoClient,
            IClientSessionHandle clientSessionHandle)
        {
            _logger = logger;
            _defaultMongoClient = defaultMongoClient;
            // Standalone servers do not support transactions.
            _clientSessionHandle = clientSessionHandle;
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
            _logger.LogInformation("Run Begin");
            var mongoDatabase = _defaultMongoClient.GetDatabase("Demo");
            var bookCollection = mongoDatabase.GetCollection<Book>("Book");

            //using (var clientSessionHandle = await _defaultMongoClient.StartSessionAsync())
            //{
            //    clientSessionHandle.StartTransaction();
            //    var datas = new List<Book>();
            //    for (var i = 1; i <= 10; ++i)
            //    {
            //        datas.Add(new Book()
            //        {
            //            BookId = "BookId" + i,
            //            BookName = "BookName" + i,
            //            Price = 100,
            //            Category = "Category" + i,
            //            Author = "Author" + i
            //        });
            //    }
            //    await bookCollection.InsertManyAsync(clientSessionHandle, datas);
            //    await clientSessionHandle.CommitTransactionAsync();
            //}

            {
                // Standalone servers do not support transactions.
                // _clientSessionHandle.StartTransaction();
                await bookCollection.DeleteManyAsync(Builders<Book>.Filter.Empty);
                var datas = new List<Book>();
                for (var i = 1; i <= 10; ++i)
                {
                    datas.Add(new Book()
                    {
                        BookId = "BookId" + i,
                        BookName = "BookName" + i,
                        Price = 100,
                        Category = "Category" + i,
                        Author = "Author" + i
                    });
                }
                await bookCollection.InsertManyAsync(_clientSessionHandle, datas);
                // Standalone servers do not support transactions.
                // await _clientSessionHandle.CommitTransactionAsync();
            }

            //{
            //    var datas = new List<Book>();
            //    for (var i = 1; i <= 10; ++i)
            //    {
            //        datas.Add(new Book()
            //        {
            //            BookId = "BookId" + i,
            //            BookName = "BookName" + i,
            //            Price = i * 100,
            //            Category = "Category" + i,
            //            Author = "Author" + i
            //        });
            //    }
            //    await bookCollection.InsertManyAsync(datas);
            //}

            //var fieldDefinition = Builders<Book>.Filter.In("book-id", new List<string>() { "BookId1", "BookId2" });
            //var datas = await bookCollection.Find(fieldDefinition).SortByDescending(s => s.BookId).ToListAsync();

            //var datas = await _books.Find(f => true).ToListAsync();

            //datas.ForEach(f =>
            //{

            //});

            //var datas = await bookCollection.Find(f => f.Id == new ObjectId("")).FirstOrDefaultAsync();

            //await bookCollection.InsertOneAsync(new Book());

            //await bookCollection.ReplaceOneAsync(f => f.Id == new ObjectId(""), new Book()
            //{
            //    Id = new ObjectId(""),
            //    BookId = "A1",
            //    BookName = "B1",
            //    Price = 200,
            //    Category = "C1",
            //    Author = "D1"
            //});

            //await bookCollection.FindOneAndReplaceAsync(f => f.Id == new ObjectId(""), new Book());

            //await bookCollection.DeleteOneAsync(f => f.BookId == "A1");
            //await bookCollection.FindOneAndDeleteAsync(f => f.Id == new ObjectId(""));

            //var fieldDefinition = Builders<Book>.Filter.Eq("book-id", "A1");
            //var updateDefinition = Builders<Book>.Update.Set("BookName", "456");
            //await bookCollection.UpdateOneAsync(fieldDefinition, updateDefinition);
            //await bookCollection.FindOneAndUpdateAsync(fieldDefinition, updateDefinition);

            _logger.LogInformation("Run End");
        }
    }
}
