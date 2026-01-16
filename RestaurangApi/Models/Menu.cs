using System.ComponentModel.DataAnnotations;

namespace ResturangDB_API.Models
{
    public class Menu
    {
        [Key]
        public int MenuID { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<MenuItem> MenuItems { get; set; }
    }
}
