using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pima.Model
{
    [Table("SongsUser")]
    public class SongsUser
    {
        public int SongsUserId { get; set; }

        public int? SongId_SongsUser { get; set; }
        [ForeignKey("SongId_SongsUser")]
        public virtual Songs Songs { get; set; }

        public int? UserId_SongsUser { get; set; }
        [ForeignKey("UserId_SongsUser")]
        public virtual User User { get; set; }
    }
}
