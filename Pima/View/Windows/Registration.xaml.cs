using Oracle.ManagedDataAccess.Client;
using Pima.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Pima.View.Windows
{
    public partial class Registration : Window
    {
        OracleDbContext db = null;
        public Registration()
        {
            InitializeComponent();
            db = new OracleDbContext();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            db = new OracleDbContext();

            if (Pima.ViewModel.Validation.ValidateRegisterAndLogin(UserName, ErrorUsername, PasswordBox, ErrorPassword, PasswordBox2, ErrorConfirmPassword))
            {
                try
                {
                    User user1 = db.Users.FirstOrDefault(x => x.Login == UserName.Text);
                    if (user1 == null)
                    {
                        string Password1 = User.getHash(PasswordBox.Password);
                        var Login = new OracleParameter("Login", OracleDbType.NVarchar2, UserName.Text, ParameterDirection.Input);
                        var Password = new OracleParameter("Password", OracleDbType.NVarchar2, Password1, ParameterDirection.Input);

                        var sql = "BEGIN REGISTRATION(:Login, :Password); END;";
                        db.Database.ExecuteSqlCommand(sql, Login, Password);
                        Reg.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        ErrorUsername.Content = "A user with the same name already exists.";
                        ErrorUsername.Visibility = Visibility.Visible;
                    }
                }
                catch
                {
                    Reg.Foreground = new SolidColorBrush(Colors.Red);
                    Reg.Visibility = Visibility.Visible;
                }
            }    
        }
    }
}
