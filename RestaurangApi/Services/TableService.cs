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

            foreach (var booking in bookings)
            {
                if (time <= booking.Time && timeEnd <= booking.TimeEnd)
                {
                    booking.Table.IsAvailable = false;
                }

                if (time >= booking.Time && timeEnd <= booking.TimeEnd)
                {
                    booking.Table.IsAvailable = false;
                }

                if (time >= booking.Time && time <= booking.TimeEnd && timeEnd >= booking.TimeEnd)
                {
                    booking.Table.IsAvailable = false;
                }

                if (time <= booking.Time && timeEnd >= booking.TimeEnd)
                {
                    booking.Table.IsAvailable = false;
                }
            }

            tables = tables.Where(t => t.IsAvailable);

            var tableList = tables.Select(table => new TableGetDTO
            {
                TableID = table.TableID,
                TableSeats = table.TableSeats,
                IsAvailable = table.IsAvailable
            }).ToList();

            return tableList;

            // booked tables is equal too the booking time and timeEnd if a tableID is within the time window
            // it shall be temporarly unavailable so that the Get endpoint can return a list with tables that are
            // available within i certain time.
        }
    }
}
