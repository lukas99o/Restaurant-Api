using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ResturangDB_API.Models.DTOs.MenuItem
{
    public class MenuItemCreateDTO
    {
        public int MenuID { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public bool IsAvailable { get; set; }
        public string ImgUrl { get; set; }
        public string Description { get; set; }
    }
}
