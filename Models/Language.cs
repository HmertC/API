using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KitapService.Models
{
    public class Language
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [Required]
        public string String_Id { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        [Required]
        public string LanguAge { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [Required]
        public string Description { get; set; }
    }
}
