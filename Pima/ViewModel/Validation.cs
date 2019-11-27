using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Pima.ViewModel
{
    public class Validation
    {
        public static bool ValidateRegisterAndLogin(TextBox Username, Label ErrorUsername, PasswordBox Password, Label ErrorPassword, 
                                                    PasswordBox ConfirmPassword, Label ErrorConfirmPassword)
        {
            bool IsValid = false;
            if(Username.Text == String.Empty)
            {
                IsValid = false;
                ErrorUsername.Content = "Field is required.";
                ErrorUsername.Visibility = Visibility.Visible;
            }
            else if(Username.Text.Length < 4 || Username.Text.Length > 20)
            {
                IsValid = false;
                ErrorUsername.Content = "Value length should be from 4 to 20 characters.";
                ErrorUsername.Visibility = Visibility.Visible;
            }
            else if(Username.Text.Any(char.IsWhiteSpace))
            {
                IsValid = false;
                ErrorUsername.Content = "Cannot contain empty characters.";
                ErrorUsername.Visibility = Visibility.Visible;
            }
            else
            {
                IsValid = true;
                ErrorUsername.Visibility = Visibility.Hidden;
            }

            if(Password.Password == String.Empty)
            {
                IsValid = false;
                ErrorPassword.Content = "Field is required.";
                ErrorPassword.Visibility = Visibility.Visible;
            }
            else if(Password.Password.Length < 4 || Password.Password.Length > 20)
            {
                IsValid = false;
                ErrorPassword.Content = "Value length should be from 4 to 20 characters.";
                ErrorPassword.Visibility = Visibility.Visible;
            }
            else if(Password.Password.Any(char.IsWhiteSpace))
            {
                IsValid = false;
                ErrorPassword.Content = "Cannot contain empty characters.";
                ErrorPassword.Visibility = Visibility.Visible;
            }
            else
            {
                IsValid = true;
                ErrorPassword.Visibility = Visibility.Hidden;
            }

            if(ConfirmPassword != null)
            {
                if (ConfirmPassword.Password == String.Empty)
                {
                    IsValid = false;
                    ErrorConfirmPassword.Content = "Field is required.";
                    ErrorConfirmPassword.Visibility = Visibility.Visible;
                }
                else if (Password.Password != ConfirmPassword.Password)
                {
                    IsValid = false;
                    ErrorConfirmPassword.Content = "Passwords do not match.";
                    ErrorConfirmPassword.Visibility = Visibility.Visible;
                }
                else
                {
                    IsValid = true;
                    ErrorConfirmPassword.Visibility = Visibility.Hidden;
                }
            }
            return IsValid;
        }
    }
}
