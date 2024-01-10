using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using static System.Net.Mime.MediaTypeNames;

namespace LibraryWPF
{
    /// <summary>
    /// Interaction logic for Editres.xaml
    /// </summary>
    public partial class Editres : Window
    {
        public bool edited = false;
        public Editres(Reservation r)
        {
            InitializeComponent();
            userid.Text = r.UserId.ToString();
            bookid.Text = r.BookId.ToString();
            duration.Text = r.Duration.ToString();
            DateTime.TryParse(r.Date, out DateTime date);
            datePicker.SelectedDate = date;
        }

        private void edituser_Click(object sender, RoutedEventArgs e)
        {
            edited = true;
            this.Close();
        }

        public Reservation getRes()
        {
            if(string.IsNullOrEmpty(userid.Text) || string.IsNullOrEmpty(bookid.Text) || string.IsNullOrEmpty(duration.Text))
            {
                return null;
            }
            else
            {
                string formattedDate = datePicker.SelectedDate?.ToString("yyyy-MM-dd")!;
                return new Reservation { UserId = Convert.ToInt32(userid.Text), BookId = Convert.ToInt32(bookid.Text), Duration = Convert.ToInt32(duration.Text), Date = formattedDate };
            }
        }
    }
}
