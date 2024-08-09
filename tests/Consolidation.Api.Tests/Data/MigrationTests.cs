using Consolidation.Api.Data.Context;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Consolidation.Api.Tests.Data
{
    public class MigrationTests : IDisposable
    {
        private ConsolidationContext _context;


        [Fact]
        public async Task Should_ApplyWithSuccess()
        {
            string result;

            var options = new DbContextOptionsBuilder<ConsolidationContext>()
                .UseSqlite("Data Source=:memory:")
                .Options;

            using (_context = new ConsolidationContext(options))
            {
                DeleteDB();
                _context.Database.OpenConnection();
                _context.Database.Migrate();

                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "SELECT 1 FROM sqlite_master WHERE type = 'table' AND tbl_name = 'Consolidations'";
                    result = command.ExecuteScalar().ToString();
                }

                _context.Database.CloseConnection();
                DeleteDB();

                Assert.True(result == "1");
            }
        }

        private void DeleteDB() => _context.Database.EnsureDeleted();

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
