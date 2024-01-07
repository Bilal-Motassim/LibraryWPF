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

namespace LibraryWPF
{
    /// <summary>
    /// Interaction logic for Addbook.xaml
    /// </summary>
    public partial class Addbook : Window
    {
        private string selectedpath;
        public Addbook()
        {
            InitializeComponent();
        }

        private void selectimagepath_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Image Files|*.png;*.jpg;*.jpeg";
            openFileDialog.FilterIndex = 1;

            bool? result = openFileDialog.ShowDialog();

            if (result == true)
            {
                selectedpath = openFileDialog.FileName;
                selectedpathtxt.Text = $"Selected image : {openFileDialog.FileName}";
            }
        }

        private void addbooktxt_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public Book getBook()
        {
            if(selectedpath == null || title.Text == null || genre.Text == null || description.Text == null)
            {
                return null;
            }
            return new Book() { Title = title.Text, Genre = genre.Text, Description = description.Text, Image = ImageToByteArray(selectedpath) };
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
