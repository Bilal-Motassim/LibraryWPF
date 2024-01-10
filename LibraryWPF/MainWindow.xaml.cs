using MaterialDesignThemes.Wpf;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using NLog;
using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using WinFrom = System.Windows.Forms;
using Button = System.Windows.Controls.Button;
using MessageBox = System.Windows.MessageBox;
using Microsoft.VisualBasic.ApplicationServices;

namespace LibraryWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Logger logger = LogManager.Setup().GetCurrentClassLogger();
        private readonly PaletteHelper palette = new PaletteHelper();

        private readonly LibraryContext dbContext = new();
        private string email;

        public MainWindow(string e)
        {
            InitializeComponent();
            var theme = palette.GetTheme();
            theme.SetPrimaryColor(System.Windows.Media.Color.FromArgb(255, 125, 10, 10));
            palette.SetTheme(theme);
            email = e;
            dbContext.Database.EnsureCreated();

            refreshBooks();

            refreshusers();

            refreshRes();
            
        }

        private void refreshusers()
        {
            List<User> users = dbContext.users.ToList();
            griduserss.ItemsSource = users;
        }

        private void refreshBooks()
        {
            List<Book> Books = dbContext.books.ToList();
            griduser.ItemsSource = null;
            griduser.ItemsSource = Books;
        }

        private void refreshRes()
        {
            List<Reservation> res = dbContext.reservations
                                            .Include(r => r.user)
                                            .Include(r => r.book).ToList();
            gridres.ItemsSource = res;
        }
        

        private void Editbook(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            Book book = (Book)btn.DataContext;
            Editbook eb = new Editbook(book);
            eb.ShowDialog();
            if(eb.edited)
            {
                Book bk = eb.getBook();
                if (bk == null)
                {
                    MessageBox.Show("Error");
                }
                else
                {
                    var booktoupdate = dbContext.books.Find(bk.Id);
                    if (booktoupdate != null)
                    {
                        booktoupdate.Title = bk.Title;
                        booktoupdate.Description = bk.Description;
                        booktoupdate.Genre = bk.Genre;
                        if (bk.ImageData != null)
                        {
                            booktoupdate.ImageData = bk.ImageData;
                        }
                        logger.Info($"{email} edited {booktoupdate.Title}");
                        dbContext.SaveChanges();
                        MessageBox.Show("Edited");
                        refreshBooks();
                    }
                }
            }
        }

        private void Deletebook(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            Book book = (Book)btn.DataContext;
            DeleteDialog dd = new DeleteDialog(book.Title);
            dd.ShowDialog();
            if (dd.getres())
            {
                var booktodelete = dbContext.books.Find(book.Id);
                if (booktodelete != null)
                {
                    logger.Info($"{email} deleted {booktodelete.Title}");
                    dbContext.books.Remove(booktodelete);
                    dbContext.SaveChanges();
                    MessageBox.Show("Book deleted");
                    refreshBooks();
                }
            }
            
        }

        private void addBook_Click(object sender, RoutedEventArgs e)
        {
            Addbook ab = new();
            ab.ShowDialog();
            Book book = ab.getBook();
            if(book == null)
            {
                MessageBox.Show("Error");
                return;
            }
            dbContext.books.Add(book);
            int res = dbContext.SaveChanges();
            if (res == 1)
            {
                logger.Info($"{email} added {book.Title}");
                MessageBox.Show("Book added", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                refreshBooks();
            }
            else
            {
                MessageBox.Show("Error2");
            }
        }


        private void Edituser(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            User user = (User)btn.DataContext;
            EditUser eu = new(user); eu.ShowDialog();
            User us = eu.getUser();
            if (us == null)
            {
                MessageBox.Show("Error");
                return;
            }
            var usertoupdate = dbContext.users.Find(user.Id);
            if (usertoupdate != null)
            {
                usertoupdate.FirstName = us.FirstName;
                usertoupdate.LastName = us.LastName;
                usertoupdate.Email = us.Email;
                usertoupdate.IsAdmin = us.IsAdmin;
                logger.Info($"{email} edited {usertoupdate.Email}");
                dbContext.SaveChanges();
                MessageBox.Show("User Edited");
                refreshusers();
            }

        }

        private void Deleteuser(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            User user = (User)btn.DataContext;
            DeleteUser du = new DeleteUser(user); du.ShowDialog();
            if (du.getres())
            {
                logger.Info($"{email} edited {user.Email}");
                dbContext.users.Remove(user);
                dbContext.SaveChanges();
                MessageBox.Show("User Deleted");
                refreshusers();
            }
        }

        private void Editeres(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            Reservation res = (Reservation)btn.DataContext;
            Editres er = new(res);
            er.ShowDialog();
            if(er.edited)
            {
                Reservation re = er.getRes();
                if(re == null)
                {
                    MessageBox.Show("Error");
                    return;
                }
                var restoupdate = dbContext.reservations.Find(res.Id);
                if (restoupdate != null)
                {
                    restoupdate.BookId = re.BookId;
                    restoupdate.UserId = re.UserId;
                    restoupdate.Duration = re.Duration;
                    restoupdate.Date = re.Date;
                    dbContext.SaveChanges();
                    logger.Info($"{email} edited {res.Id} reservation");
                    MessageBox.Show("Reservation edited");
                    refreshRes();
                    return;
                }
            }
            
        }

        private void Deleteres(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            Reservation res = (Reservation)btn.DataContext;
            Deleteres dr = new(res);
            dr.ShowDialog();
            if (dr.getres())
            {
                dbContext.reservations.Remove(res);
                dbContext.SaveChanges();
                MessageBox.Show("Reservation Deleted");
                refreshRes();
            }
        }

        private void exportcsv_Click(object sender, RoutedEventArgs e)
        {
            WinFrom.FolderBrowserDialog folderBrowserDialog = new WinFrom.FolderBrowserDialog();
            WinFrom.DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == WinFrom.DialogResult.OK)
            {
                string path = folderBrowserDialog.SelectedPath;
                List<User> users = dbContext.users.ToList();
                WriteCsv(users, $@"{path}\users.csv");
                MessageBox.Show($@"csv file generated in :{path}\users.csv");
            }
        }

        private void WriteCsv<T>(IEnumerable<T> records, string filePath)
        {
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture);

            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, csvConfig))
            {
                csv.WriteRecords(records);
            }
        }
    }
}