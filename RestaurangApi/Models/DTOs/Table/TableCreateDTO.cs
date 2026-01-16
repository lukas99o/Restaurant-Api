using System.ComponentModel.DataAnnotations;

namespace ResturangDB_API.Models.DTOs.Table
{
    public class TableCreateDTO
    {
        public int TableSeats { get; set; }
        public bool IsAvailable { get; set; } = true;
    }
}
