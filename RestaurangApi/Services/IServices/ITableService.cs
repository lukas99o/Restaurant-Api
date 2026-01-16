using ResturangDB_API.Models;
using ResturangDB_API.Models.DTOs.Table;

namespace ResturangDB_API.Services.IServices
{
    public interface ITableService
    {
        Task AddTableAsync(TableCreateDTO table);
        Task<IEnumerable<TableGetDTO>> GetAllTablesAsync();
        Task<TableGetDTO> GetTableByIdAsync(int tableID);
        Task<IEnumerable<TableGetDTO>> GetAvailableTablesAsync(DateTime time, DateTime timeEnd);
        Task<bool> UpdateTableAsync(TableUpdateDTO table);
        Task<bool> DeleteTableAsync(int tableID);
    }
}
