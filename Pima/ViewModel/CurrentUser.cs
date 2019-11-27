using Pima.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pima.ViewModel
{
    public static class CurrentUser
    {
        private static User user;

        public static User User
        {
            get { return user; }
            set { user = value; }
        }
    }
}
