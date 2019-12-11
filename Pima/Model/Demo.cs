using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pima.Model
{
    [Table("Demo")]
    public class Demo
    {
        public int DemoId { get; set; }

        public byte[] Record { get; set; }

        public DateTime Date { get; set; }

        public int? UserId_Demo { get; set; }
        [ForeignKey("UserId_Demo")]
        public virtual User User { get; set; }
    }
}
