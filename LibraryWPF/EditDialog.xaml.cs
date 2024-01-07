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
    /// Interaction logic for EditDialog.xaml
    /// </summary>
    /// 
  
    public partial class EditDialog : Window
    {

        // Properties to store the book information
        private string Title;
        private string Genre;

        // TextBox controls for title and genre
        private TextBox titleTextBox;
        private TextBox genreTextBox;

        // Property to store the Book being edited
        public Book EditedBook { get; private set; }
        public Book Book { get; }
        

            // Constructor that takes the Book to be edited as a parameter
            public EditDialog(string title, string genre, TextBox titleTextBox, TextBox genreTextBox)
        {
            //InitializeComponent();

            // Initialize the controls with the existing book information
            Title = title;
            Genre = genre;

            // Initialize the TextBox controls
            this.titleTextBox = titleTextBox;
            this.genreTextBox = genreTextBox;

            // Add other controls initialization as needed
            this.titleTextBox.Text = title;
            this.genreTextBox.Text = genre;
        }

        public EditDialog(Book book)
        {

            Book = book;
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            // Update the properties of the EditedBook with the modified values
            EditedBook = new Book
            {
                Title = titleTextBox.Text,
                Genre = genreTextBox.Text,
                // Set other properties as needed
            };

            // Close the dialog
            DialogResult = true;
        }
    }
}