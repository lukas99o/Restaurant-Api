namespace ResturangDB_API.Models.DTOs.MenuItem
{
    public class MenuItemGetDTO
    {
        public int MenuItemID { get; set; }
        public int MenuID { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public bool IsAvailable { get; set; }
        public string ImgUrl { get; set; }
        public string Description { get; set; }
    }
}
