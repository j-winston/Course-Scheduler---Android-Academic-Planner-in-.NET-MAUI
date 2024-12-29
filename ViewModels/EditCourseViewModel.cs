using System.Collections.ObjectModel;
using System.ComponentModel;
using c971.Data;
using c971.Models;
using c971.Models.c971.Models;
using MvvmHelpers;

namespace c971.ViewModels
{
    public class EditCourseViewModel : INotifyPropertyChanged
    {
        private readonly Course _model;
        public ObservableCollection<string> Options { get; }

        public event PropertyChangedEventHandler PropertyChanged;
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
                OnPropertyChanged( nameof( SelectedStatus ) );
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
                OnPropertyChanged( nameof( StartDateNotification ) );
            }
        }

        public bool EndDateNotification
        {
            get => _model.EndDateNotification;
            set
            {
                _model.EndDateNotification = value;
                OnPropertyChanged( nameof( EndDateNotification ) );
            }
        }


        public int StartDateNotificationId
        {
            get => _model.StartDateNotificationId;

            set
            {
                _model.StartDateNotificationId = value;
                OnPropertyChanged( nameof( StartDateNotificationId ) );
            }
        }

        public int EndDateNotificationId
        {
            get => _model.EndDateNotificationId;

            set
            {
                _model.EndDateNotificationId = value;
                OnPropertyChanged( nameof( EndDateNotificationId ) );
            }
        }
        public void SaveCourse()
        {
            App.Database.SaveCourse(_model);
        }


        public EditCourseViewModel(Course course)
        {
            _model = course;

            Options = new ObservableCollection<string>()
            {
                "In progress",
                "Completed",
                "Dropped"
            };

            OnPropertyChanged(nameof(Options));


        }

        public EditCourseViewModel()
        {
            _model = new Course();
           
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}