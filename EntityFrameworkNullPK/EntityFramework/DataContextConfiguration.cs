namespace EntityFrameworkNullPK.EntityFramework
{
    public class DataContextConfiguration
    {
        public string ConnectionString { get; set; }

        public DataContextConfiguration()
        {
            ConnectionString = string.Empty;
        }

        public DataContextConfiguration(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}
