using System.ComponentModel;

namespace StaffManager.ViewModel
{
    public abstract class ViewModel : INotifyPropertyChanged
    {
        protected ViewModel() { }

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
