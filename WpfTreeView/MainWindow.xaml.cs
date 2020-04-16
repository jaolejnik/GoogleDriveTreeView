using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;


namespace GoogleDriveTreeView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructor
        /// <summary>
        /// Defualt constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new DirectoryStructureViewModel();
        }
        #endregion
       
    }
}
