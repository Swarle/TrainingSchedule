namespace Tests
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var test = new Tests();

            //Console.WriteLine("Початок тестів з базою даних в MongoDb");

            //test.TestMongoDbAsync(100);
            //test.TestMongoDbAsync(1000);
            //test.TestMongoDbAsync(50000);
            //test.TestMongoDbAsync(100000);
            //test.TestMongoDbAsync(500000);

            //Console.WriteLine("Кінець тестів з базою даних в MongoDb");

            Console.WriteLine("Початок тестів з базою даних в Sql Server");

            test.TestSqlAsync(100);
            test.TestSqlAsync(1000);
            test.TestSqlAsync(50000);
            test.TestSqlAsync(100000);
            test.TestSqlAsync(500000);

            Console.WriteLine("Кінець тестів з базою даних в Sql Server");
        }
    }
}
