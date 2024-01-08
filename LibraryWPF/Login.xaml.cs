using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace LibraryWPF
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private readonly LibraryContext dbContext = new();
        public Login()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            User us = dbContext.Users.FirstOrDefault(u => u.Email == email.Text && u.Password == passw.Password);
            if (us == null)
            {
                MessageBox.Show("Email or password incorrect", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!us.IsAdmin)
            {
                MessageBox.Show("Access denied", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MainWindow m = new();
            m.Show();
            this.Close();
        }
    }
}
