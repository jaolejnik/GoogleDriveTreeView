using System;
using System.Windows;

namespace GoogleDriveTreeView
{
	/// <summary>
	/// Interaction logic for InputWindow.xaml.
	/// A custom window with input field. 
	/// </summary>
	public partial class InputWindow : Window
	{
		/// <summary>
		/// Defualt constructor.
		/// </summary>
		/// <param name="question">The question to be displayed.</param>
		/// <param name="defaultAnswer">The defualt answer that will be returned if no input was given</param>
		public InputWindow(string question, string defaultAnswer = "")
		{
			InitializeComponent();
			lblQuestion.Content = question;
			txtAnswer.Text = defaultAnswer;
		}

		/// <summary>
		/// Click action handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDialogOk_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = true;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Window_ContentRendered(object sender, EventArgs e)
		{
			txtAnswer.SelectAll();
			txtAnswer.Focus();
		}

		/// <summary>
		/// The given answer.
		/// </summary>
		public string Answer
		{
			get { return txtAnswer.Text; }
		}
	}
}