using PropertyChanged;
using System.ComponentModel;

namespace GoogleDriveTreeView
{ 
    /// <summary>
    /// A base view model that tracks and fires event when property is changed
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
    }
}
