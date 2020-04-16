using System;
using System.Windows.Input;

namespace GoogleDriveTreeView
{
    /// <summary>
    /// A basic command that runs an action
    /// </summary>
    public class RelayCommand : ICommand
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="action">The action to perform</param>
        public RelayCommand(Action action)
        {
            mAction = action;
        }

        /// <summary>
        /// The action to perform
        /// </summary>
        private Action mAction;

        /// <summary>
        /// The event thats fired when the <see cref="CanExecute(object)"/> value has changed
        /// </summary>
        public event EventHandler CanExecuteChanged = (sender, e) => { };

        /// <summary>
        /// A relay command can always execute
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter) { return true; }

        /// <summary>
        /// Executes the commands Action
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter) { mAction(); }
    }
}
