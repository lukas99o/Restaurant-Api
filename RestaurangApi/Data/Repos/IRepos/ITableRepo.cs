using ResturangDB_API.Models;

namespace ResturangDB_API.Data.Repos.IRepos
{
    public interface ITableRepo
    {
        Task AddTableAsync(Table table);
        Task<IEnumerable<Table>> GetAllTablesAsync();
        Task<Table> GetTableByIDAsync(int tableID);
        Task UpdateTableAsync(Table table);
        Task DeleteTableAsync(Table table);
    }
}
