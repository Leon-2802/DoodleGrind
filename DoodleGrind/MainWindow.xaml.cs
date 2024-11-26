using System.Linq;
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
        private readonly IdeaGenerator generator;
        private string ideas;
        public MainWindow()
        {
            InitializeComponent();
            this.generator = new();
            ideas = "";
        }

        private Button CreateNewBtn(string content, int width, int height, int margin, int fontsize)
        {
            Button dynamicButton = new Button();
            dynamicButton.Content = content;
            dynamicButton.Width = width;
            dynamicButton.Height = height;
            dynamicButton.Margin = new Thickness(margin);
            dynamicButton.FontSize = fontsize;

            return dynamicButton;
        }

        private void GenerateIdea_btn(object sender, RoutedEventArgs e)
        {
            // Show message box when button is clicked.
            try
            {
                string[] randWords = this.generator.GetRandomWords(3);
                this.ideas = string.Join(", ", randWords);
                string ideaText = "Here are three random words: " + ideas;
                MessageBox.Show(ideaText);

                // Generate Button for AI-chat with random words
                Button AIChatBtn = CreateNewBtn("Ask AI for further elaboration!", 100, 50, 5, 8);
                AIChatBtn.Click += Ai_chat_btn;
                MainDock.Children.Add(AIChatBtn);

                // Generate Button for start task
            }
            catch(Exception ex) 
            {
                MessageBox.Show("An error occured: " + ex.Message);
            }
        }

        private async void Ai_chat_btn(object sender, RoutedEventArgs e)
        {
            try
            {
                string test = await AIChat.SendChat("Please give me drawing inspiration matching the provided words: " + ideas);
                MessageBox.Show("AI Response: " + test);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("An invalid operation error occured: " + ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show("Argument Null Exception: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured: " + ex.Message);
            }
        }

    }
}