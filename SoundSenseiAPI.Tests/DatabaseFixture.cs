using Backend.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundSenseiAPI.Tests
{
    public class DatabaseFixture : IDisposable
    {
        public DbContextOptions<SoundSenseiContext> Options { get; private set; }

        public DatabaseFixture()
        {
            Options = new DbContextOptionsBuilder<SoundSenseiContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
        }

        public void Dispose()
        {
            // Clean up the database resources if needed
        }
    }
}
