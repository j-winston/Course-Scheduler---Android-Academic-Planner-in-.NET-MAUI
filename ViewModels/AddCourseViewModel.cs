using System.Collections.ObjectModel;
using System.ComponentModel;
using c971.Data;
using c971.Models.c971.Models;
using Plugin.LocalNotification;


namespace c971.ViewModels
{
    public class AddCourseViewModel : INotifyPropertyChanged

    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly Course _model;
        private readonly MainPageViewModel _mainPageViewModel = new MainPageViewModel();
        public ObservableCollection<string> Options { get; }

        // Public properties to bind to
        public int TermId
        {
            get => _model.TermId;
            set
            {
                _model.TermId = value;
                OnPropertyChanged(nameof(TermId));
            }
        }

        public int Id
        {
            get => _model.Id;
            set
            {
                _model.Id = value;
                OnPropertyChanged(nameof(Id));
            }
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


        public string SelectedStatus
        {
            get => _model.SelectedStatus;
            set
            {
                _model.SelectedStatus = value;
                OnPropertyChanged(nameof(SelectedStatus));
            }
        }

        public string InstructorName
        {
            get => _model.InstructorName;
            set
            {
                _model.InstructorName = value;
                OnPropertyChanged(nameof(InstructorName));
            }
        }

        public string InstructorPhone
        {
            get => _model.InstructorPhone;
            set
            {
                _model.InstructorPhone = value;
                OnPropertyChanged(nameof(InstructorPhone));
            }
        }

        public string InstructorEmail
        {
            get => _model.InstructorEmail;
            set
            {
                _model.InstructorEmail = value;
                OnPropertyChanged(nameof(InstructorEmail));
            }
        }

        public string Notes
        {
            get => _model.Notes;
            set
            {
                _model.Notes = value;
                OnPropertyChanged(nameof(Notes));
            }
        }

        public bool StartDateNotification
        {
            get => _model.StartDateNotification;
            set
            {
                _model.StartDateNotification = value;
                OnPropertyChanged(nameof(StartDateNotification));
            }
        }

        public bool EndDateNotification
        {
            get => _model.EndDateNotification;
            set
            {
                _model.EndDateNotification = value;
                OnPropertyChanged(nameof(EndDateNotification));
            }
        }


        public int StartDateNotificationId
        {
            get => _model.StartDateNotificationId;

            set
            {
                _model.StartDateNotificationId = value;
                OnPropertyChanged(nameof(StartDateNotificationId));
            }
        }

        public int EndDateNotificationId
        {
            get => _model.EndDateNotificationId;

            set
            {
                _model.EndDateNotificationId = value;
                OnPropertyChanged(nameof(EndDateNotificationId));
            }
        }

        public AddCourseViewModel()
        {
            _model = new Course();

            Options = new ObservableCollection<string>()
            {
                "In progress",
                "Completed",
                "Dropped"
            };
        }

        public void AddCourse(int termId)
        {
            HandleNotifications(); 

            var course = new Course()
            {
                TermId = termId,
                Id = -1,

                Title = _model.Title,
                StartDate = _model.StartDate,
                EndDate = _model.EndDate,
                InstructorName = _model.InstructorName,
                InstructorPhone = _model.InstructorPhone,
                InstructorEmail = _model.InstructorEmail,
                Notes = _model.Notes,

                StartDateNotification = _model.StartDateNotification,
                StartDateNotificationId = _model.StartDateNotificationId,

                EndDateNotification = _model.EndDateNotification,
                EndDateNotificationId = _model.EndDateNotificationId,

                SelectedStatus = _model.SelectedStatus
            };


            App.Database.AddCourse(course);
        }

        

        public void HandleNotifications()
        {
            if ( _model.StartDateNotification )
            {
                var id = _mainPageViewModel.GenerateUniqueId( _model.Id, "courseStartDate" );
                _model.StartDateNotificationId = id;

                _mainPageViewModel.SetCourseStartDateNotification( _model );
            }

            if ( _model.EndDateNotification )
            {
                var notificationId = _mainPageViewModel.GenerateUniqueId( _model.Id, "courseEndDate" );
                _model.EndDateNotificationId = notificationId;

                _mainPageViewModel.SetCourseEndDateNotification( _model );
            }

        }
       
protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}