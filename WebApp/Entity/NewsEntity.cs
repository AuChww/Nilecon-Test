using System.ComponentModel.DataAnnotations;

namespace WebApp.Entity
{
    public class News
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string NewsName { get; set; }

        [Required]
        public string NewsDetail { get; set; }

        public string? NewsImage { get; set; }
    }
}
