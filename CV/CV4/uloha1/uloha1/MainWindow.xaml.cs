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
using BigLib;

namespace uloha1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hello WPF!");
        }

        private void ChangeColorClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                "Would you like to change background color?",
                "Change color",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                MainGrid.Background = Brushes.DeepSkyBlue;
            }
        }

        private void AddPersonClick(object sender, RoutedEventArgs e)
        {
            PersonWindow win = new PersonWindow();

            if (win.ShowDialog() == true)
            {
                PeopleList.Items.Add(win.CreatedPerson);
            }
        }

    }
}