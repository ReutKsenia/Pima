using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pima.Model
{
    [Table("NotesUser")]
    public class NotesUser
    {
        public int NotesUserId { get; set; }

        public int? NotesId_NotesUser { get; set; }
        [ForeignKey("NotesId_NotesUser")]
        public virtual Notes Notes { get; set; }

        public int? UserId_NotesUser { get; set; }
        [ForeignKey("UserId_NotesUser")]
        public virtual User User { get; set; }
    }
}
