using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace KitapService.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [Required(ErrorMessage ="Kullanıcı Adı Boş Geçilemez...")]
        public string Username { get; set; } = string.Empty;
        [Column(TypeName = "nvarchar(500)")]
        [Required(ErrorMessage = "Parola Boş Geçilemez....")]
        public string Password { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
    }
}
