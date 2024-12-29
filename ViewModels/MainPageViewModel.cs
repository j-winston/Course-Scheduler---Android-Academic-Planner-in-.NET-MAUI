using System.Collections.ObjectModel;
using System.ComponentModel;
using c971.Models;
using c971.Models.c971.Models;
using Plugin.LocalNotification;


namespace c971.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        // Public Interface  
        private ObservableCollection<Term> _terms;

        public ObservableCollection<Term> Terms
        {
            get => _terms;
            set
            {
                _terms = value;
                OnPropertyChanged(nameof(_terms));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;


        public MainPageViewModel()
        {
            Terms = new ObservableCollection<Term>();
            
            LoadAllTerms();

        }




        public async void LoadAllTerms()
        {
            Terms.Clear();

            var termsTask = App.Database.GetAllTerms();
            var terms = await termsTask;

            foreach (var term in terms)
            {
                AddTerm(term);
            }
        }

        public void AddTerm(Term term)
        {
            Terms.Add(term);
        }

        public async Task RemoveTerm(Term term)
        {
            if (Terms.Contains(term))
            {
                Terms.Remove(term);

                await App.Database.DeleteTerm(term);

                OnPropertyChanged(nameof(term));
            }

        }

        public bool MaxTermsReached()
        {
            return _terms.Count > 2; 
        }

        
        // Courses Notifications  
        public void CancelCourseStartDateNotification(Course model)
        {
            LocalNotificationCenter.Current.Cancel(model.StartDateNotificationId);
        }

        public void SetCourseStartDateNotification(Course model)
        {
            var notification = new NotificationRequest()
            {
                NotificationId = model.StartDateNotificationId,
                Title = "Course Reminder",
                Description = $"{model.Title} starts on {model.StartDate.ToShortDateString()}",
                ReturningData = "Course data",
                Schedule = new NotificationRequestSchedule()
                {
                    NotifyTime = CreateNotifyTime(model.StartDate)
                }
            };

            LocalNotificationCenter.Current.Show(notification);
        }

        public void CancelCourseEndDateNotification(Course model)
        {
            LocalNotificationCenter.Current.Cancel(model.EndDateNotificationId);
        }

        public void SetCourseEndDateNotification(Course model)
        {
            var notification = new NotificationRequest()
            {
                NotificationId = model.EndDateNotificationId,
                Title = "Course Reminder",
                Description = $"{model.Title} ends on {model.EndDate.ToShortDateString()}",
                ReturningData = "Course data",
                Schedule = new NotificationRequestSchedule()
                {
                    NotifyTime = CreateNotifyTime(model.EndDate)
                }
            };

            LocalNotificationCenter.Current.Show(notification);
        }

        public DateTime CreateNotifyTime(DateTime modelDateTime)
        {
            return new DateTime(
                modelDateTime.Year,
                modelDateTime.Month,
                modelDateTime.Day,
                DateTime.Now.Hour, DateTime.Now.Minute, 0);
        }

        public int GenerateUniqueId(int courseId, string notificationName)
        {
            string idString = $"{courseId}{notificationName}";
            return idString.GetHashCode();
        }




        // Assessment Notifications 
        public void CancelAssessmentStartDateNotification( Assessment model )
        {
            LocalNotificationCenter.Current.Cancel( model.StartDateNotificationId );
        }

        public void SetAssessmentStartDateNotification( Assessment model )
        {
            var notification = new NotificationRequest()
            {
                NotificationId = model.StartDateNotificationId,
                Title = "Course Reminder",
                Description = $"{model.Title} starts on {model.StartDate.ToShortDateString()}",
                ReturningData = "Course data",
                Schedule = new NotificationRequestSchedule()
                {
                    NotifyTime = CreateNotifyTime( model.StartDate )
                }
            };

            LocalNotificationCenter.Current.Show( notification );
        }

        public void CancelAssessmentEndDateNotification( Assessment model )
        {
            LocalNotificationCenter.Current.Cancel( model.EndDateNotificationId );
        }

        public void SetAssessmentEndDateNotification( Assessment model )
        {
            var notification = new NotificationRequest()
            {
                NotificationId = model.EndDateNotificationId,
                Title = "Course Reminder",
                Description = $"{model.Title} ends on {model.EndDate.ToShortDateString()}",
                ReturningData = "Course data",
                Schedule = new NotificationRequestSchedule()
                {
                    NotifyTime = CreateNotifyTime( model.EndDate )
                }
            };

            LocalNotificationCenter.Current.Show( notification );
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}