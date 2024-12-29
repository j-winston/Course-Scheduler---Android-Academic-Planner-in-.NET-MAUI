 using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using c971.Models;
using Microsoft.Maui.Controls;

namespace c971.ViewModels
{
    public class EditTermViewModel : INotifyPropertyChanged
    {
        private readonly Term _model;

        // Public properties to bind to 
        public EditTermViewModel(Term term)
        {
            _model = term;
        }
        public EditTermViewModel(  )
        {
           
        }

        public string Title
        {
            get => _model.Title;
            set
            {
                _model.Title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public DateTime StartDate
        {
            get => _model.StartDate;
            set
            {
                _model.StartDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }

        public DateTime EndDate
        {
            get => _model.EndDate;
            set
            {
                _model.EndDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }

        public async Task SaveTerm()
        {
            await App.Database.SaveTerm(_model);

            await Application.Current.MainPage.DisplayAlert("Success", "Term updated successfully", "OK");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}