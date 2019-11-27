using Oracle.ManagedDataAccess.Client;
using Pima.Model;
using Pima.View.Pages.AdminPages;
using Pima.ViewModel;
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
    /// <summary>
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        OracleDbContext db = null;
        public static int UserId = 0;
        public Admin()
        {
            InitializeComponent();
            db = new OracleDbContext();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            db = new OracleDbContext();

            if (Pima.ViewModel.Validation.ValidateRegisterAndLogin(UserName, ErrorUsername, PasswordBox, ErrorPassword, null, null))
            {
                try
                {
                    User user1 = db.Users.FirstOrDefault(x => x.Login == UserName.Text);
                    if (user1 != null && user1.Login == "admin")
                    {

                        if (User.getHash(PasswordBox.Password).Equals(user1.Password))
                        {
                            string Password1 = User.getHash(PasswordBox.Password);
                            CurrentUser.User = user1;

                            var Login = new OracleParameter("Login", OracleDbType.NVarchar2, UserName.Text, ParameterDirection.Input);
                            var Password = new OracleParameter("Password", OracleDbType.NVarchar2, Password1, ParameterDirection.Input);
                            var UserId_OUT = new OracleParameter("UserId_OUT", OracleDbType.Int32, ParameterDirection.Output);

                            var sql = "BEGIN LOGIN(:Login, :Password, :UserId_OUT); END;";

                            var checkuser = db.Database.ExecuteSqlCommand(sql, Login, Password, UserId_OUT);

                            UserId = Convert.ToInt32(UserId_OUT.Value.ToString());
                            Close();
                            foreach (Window window in Application.Current.Windows)
                            {
                                if (window.GetType() == typeof(MainWindow))
                                {
                                    AdminPage user = new AdminPage();
                                    (window as MainWindow).CurrentPage.Navigate(user);

                                }
                            }
                        }
                        else
                        {
                            ErrorPassword.Content = "Invalid password.";
                            ErrorPassword.Visibility = Visibility.Visible;
                        }

                    }
                    else
                    {
                        ErrorUsername.Content = "A user with this name does not exist.";
                        ErrorUsername.Visibility = Visibility.Visible;
                    }

                }
                catch
                {


                }
            }
        }
    }
}
