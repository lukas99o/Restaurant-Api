using ResturangDB_API.Data.Repos.IRepos;
using ResturangDB_API.Models;
using ResturangDB_API.Models.DTOs.Table;
using ResturangDB_API.Services.IServices;
using System.Net.WebSockets;

namespace ResturangDB_API.Services
{
    public class TableService : ITableService
    {
        private readonly ITableRepo _tableRepo;
        private readonly IBookingRepo _bookingRepo;

        private static DateTime GetNextWholeHourUtc(DateTime utcNow)
        {
            var truncated = new DateTime(utcNow.Year, utcNow.Month, utcNow.Day, utcNow.Hour, 0, 0, DateTimeKind.Utc);
            return truncated.AddHours(1);
        }

        public TableService(ITableRepo tableRepo, IBookingRepo bookingRepo)
        {
            _tableRepo = tableRepo;
            _bookingRepo = bookingRepo;
        }

        public async Task AddTableAsync(TableCreateDTO tableCreate)
        {
            var table = new Table
            {
                TableSeats = tableCreate.TableSeats,
                IsAvailable = tableCreate.IsAvailable
            };

            await _tableRepo.AddTableAsync(table);
        }

        public async Task<IEnumerable<TableGetDTO>> GetAllTablesAsync()
        {
            var tables = await _tableRepo.GetAllTablesAsync();

            var tableList = tables.Select(table => new TableGetDTO
            {
                TableID = table.TableID,
                TableSeats = table.TableSeats,
                IsAvailable = table.IsAvailable
            }).ToList();

            return tableList;
        }

        public async Task<TableGetDTO> GetTableByIdAsync(int tableID)
        {
            var tableFound = await _tableRepo.GetTableByIDAsync(tableID);

            if (tableFound != null)
            {
                var table = new TableGetDTO
                {
                    TableID = tableFound.TableID,
                    TableSeats = tableFound.TableSeats,
                    IsAvailable = tableFound.IsAvailable
                };

                return table;
            }

            return null;
        }

        public async Task<bool> UpdateTableAsync(TableUpdateDTO table)
        {
            var updatedTable = await _tableRepo.GetTableByIDAsync(table.TableID);

            if (updatedTable != null)
            {
                updatedTable.TableSeats = table.TableSeats;
                updatedTable.IsAvailable = table.IsAvailable;
                await _tableRepo.UpdateTableAsync(updatedTable);

                return true;
            }

            return false;
        }

        public async Task<bool> DeleteTableAsync(int tableID)
        {
            var foundTable = await _tableRepo.GetTableByIDAsync(tableID);

            if (foundTable != null)
            {
                await _tableRepo.DeleteTableAsync(foundTable);
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<TableGetDTO>> GetAvailableTablesAsync(DateTime time, DateTime timeEnd)
        {
            var tables = await _tableRepo.GetAllTablesAsync();
            var bookings = await _bookingRepo.GetAllBookingsAsync();

            var nowUtc = DateTime.UtcNow;
            var earliestUtc = GetNextWholeHourUtc(nowUtc);
            var latestUtc = nowUtc.AddMonths(1);

            if (timeEnd <= time)
            {
                return Enumerable.Empty<TableGetDTO>();
            }

            // Booking window rules:
            // - start must be >= next whole hour from now (UTC)
            // - start must be within 1 month from now
            if (time.ToUniversalTime() < earliestUtc)
            {
                return Enumerable.Empty<TableGetDTO>();
            }

            if (time.ToUniversalTime() > latestUtc)
            {
                return Enumerable.Empty<TableGetDTO>();
            }

            var bookedTableIds = bookings
                .Where(b => time < b.TimeEnd && timeEnd > b.Time)
                .Select(b => b.FK_TableID)
                .ToHashSet();

            tables = tables.Where(t => t.IsAvailable && !bookedTableIds.Contains(t.TableID));

            var tableList = tables.Select(table => new TableGetDTO
            {
                TableID = table.TableID,
                TableSeats = table.TableSeats,
                IsAvailable = table.IsAvailable
            }).ToList();

            return tableList;
        }
    }
}
