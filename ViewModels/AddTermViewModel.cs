using System;
using System.ComponentModel;
using c971.Models;

namespace c971.ViewModels
{
    public class AddTermViewModel : INotifyPropertyChanged
    {
     
        private Term _model;
        
        // Public properties to bind to 
        public event PropertyChangedEventHandler? PropertyChanged;
        public string Title
        {
            get => _model.Title;
            set
            {
                _model.Title = value;
                OnPropertyChanged( nameof( Title ) );

            }
        }

        public DateTime StartDate
        {
            get => _model.StartDate;
            set
            {
                _model.StartDate = value;
                OnPropertyChanged( nameof( StartDate ) );
            }
        }

        public DateTime EndDate
        {
            get => _model.EndDate; 
            set
            {
                    _model.EndDate = value;
                    OnPropertyChanged( nameof( EndDate ) );
                
            }
        }

        public AddTermViewModel()
        {
            _model = new Term(); 
        }
       

        protected virtual void OnPropertyChanged( string propertyName )
        {
            PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( propertyName ) );
        }

       
    }
}