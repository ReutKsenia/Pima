using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pima.Model
{
    [Table("ArticlesUser")]
    public class ArticlesUser
    {
        public int ArticlesUserId { get; set; }

        public int? ArticleId_ArticlesUser { get; set; }
        [ForeignKey("ArticleId_ArticlesUser")]
        public virtual Article Article { get; set; }

        public int? UserId_ArticlesUser { get; set; }
        [ForeignKey("UserId_ArticlesUser")]
        public virtual User User { get; set; }
    }
}
