using EmployeeManagementSystem.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeemanagementSystem.Tests
{
    public abstract class TestWithSqllite : IDisposable
    {
        private const string connectionString = "Data Source=:memory:";
        private readonly SqliteConnection connection;

        protected readonly AppDBContext context;

        protected TestWithSqllite()
        {
            connection = new SqliteConnection(connectionString);
            connection.Open();
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseSqlite(connection)
                .Options;

            context = new AppDBContext(options);
            context.Database.EnsureCreated();
        }
        public void Dispose()
        {
            connection.Close();
        }
    }
}
