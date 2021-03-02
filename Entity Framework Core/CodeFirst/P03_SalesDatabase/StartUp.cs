namespace P03_SalesDatabase
{
    using P03_SalesDatabase.Data;
    public class StartUp
    {
        static void Main(string[] args)
        {
            var context = new SalesContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}
