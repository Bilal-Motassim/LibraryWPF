using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using static System.Net.Mime.MediaTypeNames;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;

namespace LibraryWPF
{
    /// <summary>
    /// Interaction logic for Editbook.xaml
    /// </summary>
    public partial class Editbook : Window
    {
        private string selectedpath;
        private int id;
        public bool edited = false;
        public Editbook(Book bk)
        {
            InitializeComponent();
            title.Text = bk.Title;
            genre.Text = bk.Genre;
            description.Text = bk.Description;
            id = bk.Id;
        }

        private void selectimagepath_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();

            openFileDialog.Filter = "Image Files|*.png;*.jpg;*.jpeg";
            openFileDialog.FilterIndex = 1;

            bool? result = openFileDialog.ShowDialog();

            if (result == true)
            {
                selectedpath = openFileDialog.FileName;
                selectedpathtxt.Text = $"Selected image : {openFileDialog.FileName}";
            }
        }

        private void editbooktxt_Click(object sender, RoutedEventArgs e)
        {
            edited = true;
            this.Close();
        }

        public Book getBook()
        {
            if(string.IsNullOrEmpty(title.Text) || string.IsNullOrEmpty(genre.Text) || string.IsNullOrEmpty(description.Text))
            {
                return null;
            }
            if (selectedpath != null)
            {
                return new Book { Id = id, Title = title.Text, Genre = genre.Text, Description = description.Text, ImageData = ImageToByteArray(selectedpath) };
            }
            else
            {
                return new Book { Id = id, Title = title.Text, Genre = genre.Text, Description = description.Text };
            }

        }

        static byte[] ImageToByteArray(string imagePath)
        {
            using (FileStream fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    fileStream.CopyTo(memoryStream);
                    return memoryStream.ToArray();
                }
            }
        }
    }
}
