using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pima.Model
{
    [Table("Songs")]
    public class Songs
    {
        public int SongsId { get; set; }

        [StringLength(500)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Author { get; set; }

        public string Text { get; set; }

        public byte[] Music { get; set; }

        public string Description { get; set; }

        public virtual ICollection<ChordsSong> ChordsSongs { get; set; }
        public virtual ICollection<SongsUser> SongsUsers { get; set; }
    }
}
