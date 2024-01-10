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
    /// Interaction logic for DeleteUser.xaml
    /// </summary>
    public partial class DeleteUser : Window
    {
        private string fn;
        private string ln;
        private bool res = false;
        public DeleteUser(User us)
        {
            InitializeComponent();
            fn = us.FirstName; ln = us.LastName;
            titletxt.Text = $"Are you sure you want to delete {fn} {ln}?";
        }

        private void yes_Click(object sender, RoutedEventArgs e)
        {
            res = true;
            this.Close();
        }

        private void no_Click(object sender, RoutedEventArgs e)
        {
            res = false;
            this.Close();
        }

        public bool getres()
        {
            return res;
        }
    }
}
