using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pima.Model
{
    [Table("TABsUser")]
    public class TABsUser
    {
        public int TABsUserId { get; set; }

        public int? TABsId_TABsUser { get; set; }
        [ForeignKey("TABsId_TABsUser")]
        public virtual TABs TABs { get; set; }

        public int? UserId_TABsUser { get; set; }
        [ForeignKey("UserId_TABsUser")]
        public virtual User User { get; set; }
    }
}
