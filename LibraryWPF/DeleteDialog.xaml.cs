﻿using System;
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
    /// Interaction logic for DeleteDialog.xaml
    /// </summary>
    public partial class DeleteDialog : Window
    {
        private string title;
        public DeleteDialog(string title)
        {
            InitializeComponent();
            this.title = title;
            titletxt.Text = title;
        }

        private void Button_Clickss(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("ss");
            //this.Close();
        }
        private void Button_no(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("no");
            this.Close();
        }

    }
}
