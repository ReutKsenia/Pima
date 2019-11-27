using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Pima.Model
{
    [Table("User")]
    public class User
    {
        public int UserId { get; set; }

        [StringLength(50)]
        public string Login { get; set; }

        [StringLength(100)]
        public string Password { get; set; }

        public byte[] Foto { get; set; }

        public virtual ICollection<SongsUser> SongsUsers { get; set; }
        public virtual ICollection<NotesUser> NotesUsers { get; set; }
        public virtual ICollection<TABsUser> TABsUsers { get; set; }
        public virtual ICollection<ArticlesUser> ArticlesUsers { get; set; }

        public static string getHash(string password)
        {
            if (String.IsNullOrEmpty(password))
            {
                return "-1";
            }
            else
            {
                var md5 = MD5.Create();
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hash);
            }
        }
    }
}
