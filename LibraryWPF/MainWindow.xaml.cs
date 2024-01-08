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

namespace LibraryWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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

            


            List<Book> Books = dbContext.Books.ToList();
            List<User> users = dbContext.Users.ToList();

            griduser.ItemsSource = Books;
            griduserss.ItemsSource = users;


        }
        

        private void Editbook(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            Book book = (Book)btn.DataContext;
            MessageBox.Show(book.Id.ToString());
        }

        private void Deletebook(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            Book book = (Book)btn.DataContext;
            DeleteDialog dd = new DeleteDialog(book.Title);
            dd.ShowDialog();
            
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
            dbContext.Books.Add(book);
            int res = dbContext.SaveChanges();
            if (res == 1)
            {
                MessageBox.Show("Book added", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Error2");
            }
        }


        private void Edituser(object sender, RoutedEventArgs e)
        {

        }

        private void Deleteuser(object sender, RoutedEventArgs e)
        {

        }
    }
}