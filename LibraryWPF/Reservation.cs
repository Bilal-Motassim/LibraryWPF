using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWPF
{
    internal class Reservation
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int BookId { get; set; }

        [Required]
        public string Date { get; set; }

        [Required]
        public int Duration { get; set; }
    }
}
