using System.Windows;

namespace GoogleDriveTreeView
{
    /// <summary>
    /// Interaction logic for LoadingWindow.xaml
    /// </summary>
    public partial class LoadingWindow : Window
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public LoadingWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        /// <summary>
        /// The message to be displayed.
        /// </summary>
        public string Message { get; set; }
    }
}
