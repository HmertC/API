using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace KitapService.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [Required(ErrorMessage = "Öğrenci Adı Boş Geçilemez...")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [Required(ErrorMessage = "Öğrenci Soyadı Boş Geçilemez...")]
        public string SurName { get; set; }

        [Column(TypeName = "int")]
        [Required(ErrorMessage = "Öğrenci Numarası Boş Geçilemez...")]
        public int StudentNo { get; set; }

        [Column(TypeName = "nvarchar(15)")]
        [Required(ErrorMessage = "Öğrenci Doğum Günü Boş Geçilemez...")]
        public string Birtday { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
