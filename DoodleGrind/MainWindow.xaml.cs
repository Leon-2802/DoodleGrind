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

namespace DoodleGrind
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

        async void button_Click(object sender, RoutedEventArgs e)
        {
            // Show message box when button is clicked.
            try
            {
                string test = await AIChat.SendChat("Please provide three ideas for drawing a doodle matching the topic plants");
                MessageBox.Show("AI Response: " + test);
            }
            catch(Exception ex) 
            {
                MessageBox.Show("An error occured: " + ex.Message);
            }
        }

    }
}