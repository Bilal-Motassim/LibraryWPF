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

        public MainWindow()
        {
            InitializeComponent();
            var theme = palette.GetTheme();
            theme.SetPrimaryColor(System.Windows.Media.Color.FromArgb(255, 125, 10, 10));
            palette.SetTheme(theme);

            //string imagePath = @"C:\Users\PC\Desktop\book.jpg";

            //byte[] imageBytes = ImageToByteArray(imagePath);


            dbContext.Database.EnsureCreated();

            //Book book = new() { Title = "title22", Genre = "gerneee", Image = imageBytes, Available = true };
            //dbContext.Books.Add(book);
            //dbContext.SaveChanges();



            refreshBooks();

            refreshusers();

            refreshRes();
            

            logger.Error("HI");
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
                dbContext.users.Remove(user);
                dbContext.SaveChanges();
            }
        }

        private void Editeres(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            Reservation res = (Reservation)btn.DataContext;
            Editres er = new(res);
            er.ShowDialog();
            
        }

        private void Deleteres(object sender, RoutedEventArgs e)
        {

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