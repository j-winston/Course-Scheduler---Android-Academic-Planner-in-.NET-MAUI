using SQLite;
using System.ComponentModel;
using System; 

namespace c971.Models
{
    public class Term : INotifyPropertyChanged
    {
        // Changes in values will be published to UI bindings
        private string _title;
        private DateTime _startDate = DateTime.Now;
        private DateTime _endDate = DateTime.Now;
        private int _id;

        // Automatically update the id
        [PrimaryKey, AutoIncrement] 
        public int Id
        {
            get => _id;
            set
            {
                if ( _id != value )
                {
                    _id = value;
                    OnPropertyChanged( nameof( Id ) );
                }
            }
        }

        public bool IsTestData { get; set; }
        public string Title
        {
            get => _title;
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }

        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                if (_startDate != value)
                {
                    _startDate = value;
                    OnPropertyChanged(nameof(StartDate));
                }
            }
        }

        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                if (_endDate != value)
                {
                    _endDate = value;
                    OnPropertyChanged(nameof(EndDate));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}