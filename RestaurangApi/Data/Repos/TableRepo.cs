using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using ResturangDB_API.Data.Repos.IRepos;
using ResturangDB_API.Models;

namespace ResturangDB_API.Data.Repos
{
    public class TableRepo : ITableRepo
    {
        private readonly ResturangContext _context;

        public TableRepo(ResturangContext context)
        {
            _context = context;
        }

        public async Task AddTableAsync(Table table)
        {
            await _context.Tables.AddAsync(table);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Table>> GetAllTablesAsync()
        {
            var tableList = await _context.Tables.ToListAsync();
            return tableList;
        }

        public async Task<Table> GetTableByIDAsync(int tableID)
        {
            var table = await _context.Tables.FindAsync(tableID);
            return table;
        }

        public async Task UpdateTableAsync(Table table)
        {
            _context.Tables.Update(table);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTableAsync(Table table)
        {
            _context.Tables.Remove(table);
            await _context.SaveChangesAsync();
        }
    }
}
