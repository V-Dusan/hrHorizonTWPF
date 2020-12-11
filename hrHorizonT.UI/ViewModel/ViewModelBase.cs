using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace hrHorizonT.UI.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            //if (PropertyChanged != null)
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
