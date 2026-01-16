using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ResturangDB_API.Models
{
    public class MenuItem
    {
        [Key]
        public int MenuItemID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        [ForeignKey("Menu")]
        public int FK_MenuID { get; set; }

        [JsonIgnore]
        public Menu Menu { get; set; }

        public string ?ImgUrl { get; set; }
        public string Description { get; set; }
    }
}