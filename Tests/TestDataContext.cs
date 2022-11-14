using EntityFrameworkNullPK.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Tests
{
    internal class TestDataContext : DataContext
    {
        public TestDataContext()
           : base()
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
           => optionsBuilder.UseSqlServer("Server=tcp:localhost,1433;User ID=SA;Password=&Unit_Testing&;Connection Timeout=30;Max Pool Size=1000;TrustServerCertificate=true;");

        public void EnsureCreated()
           => Database.EnsureCreated();

        public void Reset()
        {
            List<string> tableNames = Model.GetEntityTypes()
             .Select(x => x.GetTableName())
             .Distinct()
             .ToList();

            foreach (string table in tableNames)
            {
                string nocheckQuery = $"ALTER TABLE {table} NOCHECK CONSTRAINT ALL;";
                Database.ExecuteSqlRaw(nocheckQuery);
            }

            foreach (string table in tableNames)
            {
                string truncateQuery = $"DELETE FROM master.dbo.{table};";
                Database.ExecuteSqlRaw(truncateQuery);

                try
                {
                    string reseedQuery = $"DBCC CHECKIDENT('master.dbo.{table}', RESEED, 0)";
                    Database.ExecuteSqlRaw(reseedQuery);
                }
                catch (Exception)
                {
                    // Some tables do not have an identity column and cannot be reseeded.
                }
            }
        }
    }
}
