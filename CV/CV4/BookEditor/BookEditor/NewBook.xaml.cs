using BookRecords.Library;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookEditor
{
    /// <summary>
    /// Interaction logic for NewBook.xaml
    /// </summary>
    public partial class NewBook : Window
    {
        public Book CreatedBook { get; private set; }
        public NewBook()
        {
            InitializeComponent();
        }

        private void Ok_Click(object sender, RoutedEventArgs e) 
        {
            if (
                string.IsNullOrEmpty(AuthorBox.Text) ||
                string.IsNullOrEmpty(TitleBox.Text) ||
                string.IsNullOrEmpty(ISBNBox.Text) ||
                string.IsNullOrEmpty(PublisherBox.Text) ||
                string.IsNullOrEmpty(YearBox.Text) ||
                string.IsNullOrEmpty(AnnotationBox.Text)
                )
            {
                MessageBox.Show("Please fill all the inputs.");
                return;
            }

            CreatedBook = new Book
            {
                Author = AuthorBox.Text,
                Title = TitleBox.Text,
                Isbn = ISBNBox.Text,
                Publisher = PublisherBox.Text,
                Year = int.Parse(YearBox.Text),
                Annotation = AnnotationBox.Text,
            };

            this.DialogResult = true;
        }


        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

