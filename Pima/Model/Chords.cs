using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pima.Model
{
    [Table("Chords")]
    public class Chords
    {
        public int ChordsId { get; set; }

        [StringLength(20)]
        public string Name { get; set; }

        public byte[] Chord { get; set; }

        public virtual ICollection<ChordsSong> ChordsSongs { get; set; }
    }
}
