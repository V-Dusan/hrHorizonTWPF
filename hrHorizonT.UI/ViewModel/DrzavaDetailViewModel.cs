using hrHorizonT.DataAccess;
using hrHorizonT.Model;
using hrHorizonT.UI.Data.Repositories;
using hrHorizonT.UI.View.Services;
using hrHorizonT.UI.Wrapper;
using Microsoft.EntityFrameworkCore;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace hrHorizonT.UI.ViewModel
{
    public class DrzavaDetailViewModel : DetailViewModelBase
    {
        private IDrzavaRepository _drzavaRepository;
        private hrHorizonTDbContext _horizonTDbContext;
        private DrzavaWrapper _selectedDrzava;
        private string _searchText;

        public DrzavaDetailViewModel(IEventAggregator eventAggregator, IMessageDialogService messageDialogService,
                                    IDrzavaRepository drzavaRepository, hrHorizonTDbContext horizonTDbContext) : base(eventAggregator, messageDialogService)
        {
            _drzavaRepository = drzavaRepository;
            _horizonTDbContext = horizonTDbContext;
            Title = "Države";
            Drzava = new ObservableCollection<DrzavaWrapper>();

            AddCommand = new DelegateCommand(OnAddExecute);
            RemoveCommand = new DelegateCommand(OnRemoveExecute, OnRemoveCanExecute);
            SearchMemberCommand = new DelegateCommand<string>(OnSearchMemberExecute);
        }

        public async override Task LoadAsync(int id)
        {
            // Load data here
            Id = id;
            foreach (var wrapper in Drzava)
            {
                wrapper.PropertyChanged -= Wrapper_PropertyChanged;
            }

            Drzava.Clear();

            var drzave = await _drzavaRepository.GetAllAsync();

            foreach (var model in drzave)
            {
                var wrapper = new DrzavaWrapper(model);
                wrapper.PropertyChanged += Wrapper_PropertyChanged;
                Drzava.Add(wrapper);
            }
        }


        private void Wrapper_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!HasChanges)
            {
                HasChanges = _drzavaRepository.HasChanges();
            }
            if (e.PropertyName == nameof(DrzavaWrapper.HasErrors))
            {
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<DrzavaWrapper> Drzava { get; }

        public ICommand RemoveCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand SearchMemberCommand { get; }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged();
            }
        }

        public DrzavaWrapper SelectedDrzava
        {
            get { return _selectedDrzava; }
            set
            {
                _selectedDrzava = value;
                OnPropertyChanged();
                ((DelegateCommand)RemoveCommand).RaiseCanExecuteChanged();
            }
        }


        protected override void OnDeleteExecute()
        {
            throw new NotImplementedException();
        }

        protected override bool OnSaveCanExecute()
        {
            return HasChanges && Drzava.All(p => !p.HasErrors);
        }

        protected async override void OnSaveExecute()
        {
            try
            {
                await _drzavaRepository.SaveAsync();
                HasChanges = _drzavaRepository.HasChanges();
                RaiseCollectionSavedEvent();
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                await MessageDialogService.ShowInfoDialogAsync($"{ ex.Message }" +
                $" ne može biti dodata, jer vec postoji");
                return;
            }
        }

        private void OnAddExecute()
        {
            var wrapper = new DrzavaWrapper(new Drzava());
            wrapper.PropertyChanged += Wrapper_PropertyChanged;
            _drzavaRepository.Add(wrapper.Model);
            Drzava.Insert(0,wrapper);
            //Trigger the validation
            wrapper.Sifra = null;
            wrapper.Oznaka = "";
            wrapper.Naziv = "";
        }

        private void OnRemoveExecute()
        {
            //var isReferenced = await _drzavaRepository.IsReferencedByFriendAsync(SelectedDrzava.Id);
            //if (isReferenced)
            //{
            //await MessageDialogService.ShowInfoDialogAsync($"Država { SelectedDrzava.Naziv }" +
            //$" ne može biti obrisana, jer je povezana bar sa jednim katalogom");
            //return;
            //}

            SelectedDrzava.PropertyChanged -= Wrapper_PropertyChanged;
            _drzavaRepository.Remove(SelectedDrzava.Model);
            Drzava.Remove(SelectedDrzava);
            SelectedDrzava = null;
            HasChanges = _drzavaRepository.HasChanges();
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        private bool OnRemoveCanExecute()
        {
            return SelectedDrzava != null;
        }

        private async void OnSearchMemberExecute(string memberName)
        {
            foreach (var wrapper in Drzava)
            {
                wrapper.PropertyChanged -= Wrapper_PropertyChanged;
            }

            Drzava.Clear();

            var drzave = await _horizonTDbContext.Drzavas.Where(n => n.Naziv.ToLower().Contains(memberName.ToLower())).OrderBy(n => n.Naziv).ToListAsync();

            foreach (var model in drzave)
            {
                var wrapper = new DrzavaWrapper(model);
                wrapper.PropertyChanged += Wrapper_PropertyChanged;
                Drzava.Add(wrapper);
            }



        }
    }
}
