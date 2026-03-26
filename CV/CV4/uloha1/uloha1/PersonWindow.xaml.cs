using BigLib;
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

namespace uloha1
{
    /// <summary>
    /// Interaction logic for PersonWindow.xaml
    /// </summary>
    public partial class PersonWindow : Window
    {
        public Person CreatedPerson { get; private set; }
        public PersonWindow()
        {
            InitializeComponent();
        }
        private void OkClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtFirstName.Text) ||
                string.IsNullOrWhiteSpace(TxtLastName.Text) || !DateBirthday.SelectedDate.HasValue)
            {
                MessageBox.Show("Please fill all the inputs.");
                return;
            }

            CreatedPerson = new Person
            {
                FirstName = TxtFirstName.Text,
                LastName = TxtLastName.Text,
                Birthdate = DateBirthday.SelectedDate.Value
            };

            this.DialogResult = true;
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
