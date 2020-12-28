using hrHorizonT.UI.Event;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace hrHorizonT.UI.ViewModel
{
    public abstract class DetailViewModelBase : ViewModelBase, IDetailViewModel
    {
        public bool _hasChanges;
        private IEventAggregator EventAggregator;
        private int _id;

        public DetailViewModelBase(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            DeleteCommand = new DelegateCommand(OnDeleteExecute);
        }

        public abstract Task LoadAsync(int? Id);

        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }

        public int Id
        { get { return _id; }
            protected set { _id = value; }
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

        protected abstract void OnDeleteExecute();
        protected abstract bool OnSaveCanExecute();
        protected abstract void OnSaveExecute();

        protected virtual void RaiseDetailDeletedEvent(int modelId)
        {
            EventAggregator.GetEvent<AfterDetailDeletedEvent>().Publish(new AfterDetailDeletedEventArgs
            {
                Id = modelId,
                ViewModelName = this.GetType().Name
            });
        }

        protected virtual void RaiseDetailSavedEvent(int modelId, string displayMember)
        {
            EventAggregator.GetEvent<AfterDetailSavedEvent>().Publish( new AfterDetailSavedEventArgs
            {
                Id = modelId,
                DisplayMember = displayMember,
                ViewModelName = this.GetType().Name
            });
        }
    }
}
