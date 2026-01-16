namespace ResturangDB_API.Models.DTOs.Table
{
    public class TableUpdateDTO
    {
        public int TableID { get; set; }
        public int TableSeats { get; set; }
        public bool IsAvailable { get; set; }
    }
}
