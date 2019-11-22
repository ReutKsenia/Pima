using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pima.Model
{
    [Table("ChordsSong")]
    public class ChordsSong
    {
        public int Id { get; set; }

        public int? SongId_ChordsSong { get; set; }
        [ForeignKey("SongId_ChordsSong")]
        public virtual Songs Songs { get; set; }

        public int? ChordId_ChordsSong { get; set; }
        [ForeignKey("ChordId_ChordsSong")]
        public virtual Chords Chords { get; set; }
    }
}
