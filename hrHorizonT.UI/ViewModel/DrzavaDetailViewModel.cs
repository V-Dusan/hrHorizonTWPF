using ClosedXML.Excel;
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
using System.Windows.Data;
using System.Windows.Input;

namespace hrHorizonT.UI.ViewModel
{
    public class DrzavaDetailViewModel : DetailViewModelBase
    {
        private IDrzavaRepository _drzavaRepository;
        private DrzavaWrapper _selectedDrzava;
        private string _searchText;

        private readonly string _drzave = "Države";

        public DrzavaDetailViewModel(IEventAggregator eventAggregator, IMessageDialogService messageDialogService,
                                    IDrzavaRepository drzavaRepository) : base(eventAggregator, messageDialogService)
        {
            _drzavaRepository = drzavaRepository;
            Title = _drzave;
            Drzava = new ObservableCollection<DrzavaWrapper>();

            AddCommand = new DelegateCommand(OnAddExecute);
            RemoveCommand = new DelegateCommand(OnRemoveExecute, OnRemoveCanExecute);
            ExcelCommand = new DelegateCommand(Excel);
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
        public ICommand ExcelCommand { get; }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged("SearchText");
                OnSearchMemberExecute(_searchText);
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
            Drzava.Insert(0, wrapper);
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

        private async void OnSearchMemberExecute(string searchText)
        {
            foreach (var wrapper in Drzava)
            {
                wrapper.PropertyChanged -= Wrapper_PropertyChanged;
            }

            Drzava.Clear();

            var filterdrzave = await _drzavaRepository.GetAllAsync();

            var drzave = filterdrzave
                .Where(n => n.Naziv.ToLower().Contains(searchText.ToLower()) 
                || n.Oznaka.ToLower().Contains(searchText.ToLower())
                || n.Sifra.ToString().Contains(searchText))
                .OrderBy(n => n.Naziv)
                .ToList();

            foreach (var model in drzave)
            {
                var wrapper = new DrzavaWrapper(model);
                wrapper.PropertyChanged += Wrapper_PropertyChanged;
                Drzava.Add(wrapper);
            }
        }

        private void Excel()
        {

            using (var wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("Contacts");

                //Adding text
                //Title
                ws.Cell("B2").Value = "Contacts";
                //First Names
                ws.Cell("B3").Value = "FName";
                ws.Cell("B4").Value = "John";
                ws.Cell("B5").Value = "Hank";
                ws.Cell("B6").Value = "Dagny";
                //Last Names
                ws.Cell("C3").Value = "LName";
                ws.Cell("C4").Value = "Galt";
                ws.Cell("C5").Value = "Rearden";
                ws.Cell("C6").Value = "Taggart";

                //Adding more data types
                //Is an outcast?
                ws.Cell("D3").Value = "Outcast";
                ws.Cell("D4").Value = true;
                ws.Cell("D5").Value = false;
                ws.Cell("D6").Value = false;
                //Date of Birth
                ws.Cell("E3").Value = "DOB";
                ws.Cell("E4").Value = new DateTime(1919, 1, 21);
                ws.Cell("E5").Value = new DateTime(1907, 3, 4);
                ws.Cell("E6").Value = new DateTime(1921, 12, 15);
                //Income
                ws.Cell("F3").Value = "Income";
                ws.Cell("F4").Value = 2000;
                ws.Cell("F5").Value = 40000;
                ws.Cell("F6").Value = 10000;

                //Defining ranges
                //From worksheet
                var rngTable = ws.Range("B2:F6");
                //From another range
                var rngDates = rngTable.Range("E4:E6");
                var rngNumbers = rngTable.Range("F4:F6");

                //Formatting dates and numbers
                //Using a OpenXML's predefined formats
                rngDates.Style.NumberFormat.NumberFormatId = 15;
                //Using a custom format
                rngNumbers.Style.NumberFormat.Format = "$ #,##0";

                //Formatting headers
                var rngHeaders = rngTable.Range("B3:F3");
                rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                rngHeaders.Style.Font.Bold = true;
                rngHeaders.Style.Fill.BackgroundColor = XLColor.Aqua;

                //Adding grid lines
                rngTable.Style.Border.BottomBorder = XLBorderStyleValues.Thin;

                //Format title cell
                rngTable.Cell(1, 1).Style.Font.Bold = true;
                rngTable.Cell(1, 1).Style.Fill.BackgroundColor = XLColor.CornflowerBlue;
                rngTable.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                //Merge title cells
                rngTable.Row(1).Merge(); // We could've also used: rngTable.Range("A1:E1").Merge()

                //Add thick borders
                rngTable.Style.Border.OutsideBorder = XLBorderStyleValues.Thick;

                // You can also specify the border for each side with:
                // rngTable.FirstColumn().Style.Border.LeftBorder = XLBorderStyleValues.Thick;
                // rngTable.LastColumn().Style.Border.RightBorder = XLBorderStyleValues.Thick;
                // rngTable.FirstRow().Style.Border.TopBorder = XLBorderStyleValues.Thick;
                // rngTable.LastRow().Style.Border.BottomBorder = XLBorderStyleValues.Thick;

                // Adjust column widths to their content
                ws.Columns(2, 6).AdjustToContents();
                wb.SaveAs("HelloWorld.xlsx");
            }
        }
    }
}
