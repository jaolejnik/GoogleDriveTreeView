using System.Windows.Input;

namespace GoogleDriveTreeView
{   
    /// <summary>
    /// Helper class for better ContextMenu binding
    /// </summary>
    public class ContextMenuItem
    {
        public string ItemHeader { get; set; }
        public ICommand ItemAction { get; set; }
    }
}
