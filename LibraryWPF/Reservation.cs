using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWPF
{
    public class Reservation
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int UserId { get; set; }
        public User user { get; set; }

        public int BookId { get; set; }
        public Book book { get; set; }

        [Required]
        public string Date { get; set; }

        [Required]
        public int Duration { get; set; }
    }
}
