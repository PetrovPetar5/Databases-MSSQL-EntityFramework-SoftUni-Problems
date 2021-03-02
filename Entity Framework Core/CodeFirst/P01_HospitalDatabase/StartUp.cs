namespace P01_HospitalDatabase
{
    using P01_HospitalDatabase.Data;
    public class StartUp
    {
        static void Main(string[] args)
        {
            var context = new HospitalContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}
