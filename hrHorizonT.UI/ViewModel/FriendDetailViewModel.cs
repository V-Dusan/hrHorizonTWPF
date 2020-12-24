using hrHorizonT.Model;
using hrHorizonT.UI.Data;
using hrHorizonT.UI.Data.Lookups;
using hrHorizonT.UI.Data.Repositories;
using hrHorizonT.UI.Event;
using hrHorizonT.UI.View.Services;
using hrHorizonT.UI.Wrapper;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace hrHorizonT.UI.ViewModel
{
    public class FriendDetailViewModel : ViewModelBase, IFriendDetailViewModel
    {
        private IHorizonTRepository _horizonTRepository;
        private IEventAggregator _eventAggregator;
        private IMessageDialogService _messageDialogService;
        private IProgramingLanguageLookupDataService _programingLanguageLookupDataService;
        private FriendWrapper _friend;
        private bool _hasChanges;

        public FriendDetailViewModel(IHorizonTRepository hrHorizonTRepository, IEventAggregator eventAggregator, 
            IMessageDialogService messageDialogService, IProgramingLanguageLookupDataService programingLanguageLookupDataService)
        {
            _horizonTRepository = hrHorizonTRepository;
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;
            _programingLanguageLookupDataService = programingLanguageLookupDataService;

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            DeleteCommand = new DelegateCommand(OnDeleteExecute);

            ProgrammingLanguages = new ObservableCollection<LookupItem>();
        }

        public async Task LoadAsync(int? friendId)
        {
            var friend = friendId.HasValue
               ? await _horizonTRepository.GetByIdAsync(friendId.Value) : CreateNewFriend();
            InitializeFriend(friend);

            await LoadProgramingLanguagesLookupAsync();
        }

        private void InitializeFriend(Friend friend)
        {
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
            if (Friend.Id == 0)
            {   //Little trick to trigger the validation
                Friend.FirstName = "";
            }
        }

        private async Task LoadProgramingLanguagesLookupAsync()
        {
            ProgrammingLanguages.Clear();
            var lookup = await _programingLanguageLookupDataService.GetProgramingLanguageLookupAsync();
            foreach (var lookupItem in lookup)
            {
                ProgrammingLanguages.Add(lookupItem);
            }
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
        public ICommand DeleteCommand { get; }
        public ObservableCollection<LookupItem> ProgrammingLanguages { get; }

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

        private async void OnDeleteExecute()
        {
            var result = _messageDialogService.ShowOkCancelDialog($"Do you really want to delete the friend {Friend.FirstName} {Friend.LastName}?", "Question");

            if (result == MessageDialogResult.OK)
            {
                _horizonTRepository.Remove(Friend.Model);
                await _horizonTRepository.SaveAsync();
                _eventAggregator.GetEvent<AfterFriendDeletedEvent>().Publish(Friend.Id);
            }

        }

        private Friend CreateNewFriend()
        {
            var friend = new Friend();
            _horizonTRepository.Add(friend);
            return friend;
        }
    }
}
