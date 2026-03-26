using BookRecords.Library;
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

namespace BookEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public BookCollection MojeKnihy {  get; set; } = new BookCollection();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            Books.ItemsSource = this.MojeKnihy;
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog { Filter = "JSON Files (*.json)|*.json" };
            if (dialog.ShowDialog() == true)
            {
                var noveKnihy = BookCollection.LoadFromJson(new FileInfo(dialog.FileName));
                if (noveKnihy != null)
                {
                    MojeKnihy.Clear();
                    foreach (var b in noveKnihy)
                    {
                        MojeKnihy.Add(b);
                    }
                }
            }
        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog
            {
                Title = "Uložiť knižnicu",
                Filter = "JSON súbor (*.json)|*.json|CSV súbor (*.csv)|*.csv",
                FileName = "moje_knihy",
                DefaultExt = ".json"
            };

            if (dialog.ShowDialog() == true)
            {
                FileInfo fileInfo = new FileInfo(dialog.FileName);

                try
                {
                    if (fileInfo.Extension.ToLower() == ".json")
                    {
                        this.MojeKnihy.SaveToJson(fileInfo);
                    }
                    else if (fileInfo.Extension.ToLower() == ".csv")
                    {
                        this.MojeKnihy.SaveToCsv(fileInfo, delimiter: ",");
                    }

                    MessageBox.Show("Dáta boli úspešne uložené.", "Uložené", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Chyba pri ukladaní: {ex.Message}", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void AddBook_Click(object sender, RoutedEventArgs e) 
        {
            NewBook NewBookWindow = new NewBook();

            if (NewBookWindow.ShowDialog() == true) 
            {
                this.MojeKnihy.Add(NewBookWindow.CreatedBook);
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            if (Books.SelectedItem is Book vybranaKniha)
            {
                MojeKnihy.Remove(vybranaKniha);
            }
            else
            {
                MessageBox.Show("Najprv vyber knihu zo zoznamu!");
            }
        }
    }
}