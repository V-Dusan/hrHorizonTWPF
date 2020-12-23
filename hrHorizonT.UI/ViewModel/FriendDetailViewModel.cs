using hrHorizonT.Model;
using hrHorizonT.UI.Data;
using hrHorizonT.UI.Data.Repositories;
using hrHorizonT.UI.Event;
using hrHorizonT.UI.Wrapper;
using Prism.Commands;
using Prism.Events;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace hrHorizonT.UI.ViewModel
{
    public class FriendDetailViewModel : ViewModelBase, IFriendDetailViewModel
    {
        private IHorizonTRepository _horizonTRepository;
        private IEventAggregator _eventAggregator;
        private FriendWrapper _friend;
        private bool _hasChanges;

        public FriendDetailViewModel(IHorizonTRepository hrHorizonTRepository, IEventAggregator eventAggregator)
        {
            _horizonTRepository = hrHorizonTRepository;
            _eventAggregator = eventAggregator;

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
        }

        public async Task LoadAsync(int friendId)
        {
            var friend = await _horizonTRepository.GetByIdAsync(friendId);

            Friend = new FriendWrapper(friend);
            Friend.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _horizonTRepository.HasChanges();
                }
                if (e.PropertyName == nameof(Friend.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        public FriendWrapper Friend
        {
            get { return _friend; }
            private set
            {
                _friend = value;
                OnPropertyChanged();
            }
        }        

        public bool HasChanges
        {
            get { return _hasChanges; }
            set
            {
                if (_hasChanges != value)
                {
                    _hasChanges = value;
                    OnPropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }


        public ICommand SaveCommand { get; }

        private async void OnSaveExecute()
        {
            await _horizonTRepository.SaveAsync();
            HasChanges = _horizonTRepository.HasChanges();
            _eventAggregator.GetEvent<AfterFriendSavedEvent>().Publish(
                 new AfterFriendSavedEventArgs
                 {
                     Id = Friend.Id,
                     DisplayMember = $"{Friend.FirstName} {Friend.LastName}"
                 });
        }

        private bool OnSaveCanExecute()
        {
            return Friend != null && !Friend.HasErrors && HasChanges;
        }
    }
}
