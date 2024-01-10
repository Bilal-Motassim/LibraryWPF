using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Drawing;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryWPF
{
    public class Book
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Genre { get; set; }

        public string Available { get; set; } = "YES";

        [Required]
        public byte[] ImageData { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public bool IsFavorite {  get; set; }

    }
}
