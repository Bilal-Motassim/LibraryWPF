using System;
using System.Collections.Generic;
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
    /// Interaction logic for EditUser.xaml
    /// </summary>
    public partial class EditUser : Window
    {
        public EditUser(User us)
        {
            InitializeComponent();
            lastname.Text = us.LastName;
            firstname.Text = us.FirstName;
            email.Text = us.Email;
            isAdmin.IsChecked = us.IsAdmin;
        }

        private void addbooktxt_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public User getUser()
        {
            if (string.IsNullOrEmpty(lastname.Text) || string.IsNullOrEmpty(firstname.Text) || string.IsNullOrEmpty(email.Text))
            {
                this.Close();
                return null;
            }

            return new User { FirstName = firstname.Text, LastName = lastname.Text, Email = email.Text, IsAdmin = (bool)isAdmin.IsChecked! };
        }
    }
}
