using System.Text;

namespace DemoNetCoreDb.Redis
{
    internal class RedisWriterLogger : TextWriter
    {
        public RedisWriterLogger()
        {
        }
        public override void Write(string? value)
        {
            Console.WriteLine(value);
        }
        public override void WriteLine(string? value)
        {
            Console.WriteLine(value);
        }
        public override async Task WriteAsync(string? value)
        {
            Console.WriteLine(value);
        }
        public override async Task WriteLineAsync(string? value)
        {
            Console.WriteLine(value);
        }
        public override Encoding Encoding => Encoding.Default;
    }
}
