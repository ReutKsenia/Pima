using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pima.Model
{
    [Table("Notes")]
    public class Notes
    {
        public int NotesId { get; set; }

        [StringLength(500)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Author { get; set; }

        public byte[] Note { get; set; }

        public string Description { get; set; }

        public virtual ICollection<NotesUser> NotesUsers { get; set; }
    }
}
