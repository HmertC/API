using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace KitapService.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [Required(ErrorMessage = "Kitap Adı Boş Geçilemez...")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Description { get; set; }= string.Empty;

        [Column(TypeName = "nvarchar(100)")]
        public string Category { get; set; } = "Kategorisiz";
        public bool IsActive { get; set; } = true;
    }
}
