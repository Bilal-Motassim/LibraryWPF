using Microsoft.EntityFrameworkCore;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
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
using MessageBox = System.Windows.MessageBox;

namespace LibraryWPF
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private readonly LibraryContext dbContext = new();
        private Logger logger = LogManager.Setup().GetCurrentClassLogger();
        public Login()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //User us = dbContext.users.FirstOrDefault(u => u.Email == email.Text && u.Password == passw.Password);
            //if (us == null)
            //{
            //    MessageBox.Show("Email or password incorrect", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}
            //if (!us.IsAdmin)
            //{
            //    MessageBox.Show("Access denied", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}
            //MainWindow m = new();
            //m.Show();
            //this.Close();
            var existingUser = dbContext.users.FirstOrDefault(u => u.Email == email.Text);

            if (existingUser != null)
            {
                string hashedPassword = HashPassword(passw.Password);


                if (existingUser.Password == hashedPassword)
                {
                    if (existingUser.IsAdmin)
                    {
                        MainWindow m = new(existingUser.Email);
                        m.Show();
                        logger.Info($"{existingUser.Email} logged in.");
                        this.Close();
                        return;
                    }
                    else
                    {
                        logger.Info($"{existingUser.Email} tried to log in but he is not an admin");
                        MessageBox.Show("Access denied", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
            }
            MessageBox.Show("Invalid email or password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
