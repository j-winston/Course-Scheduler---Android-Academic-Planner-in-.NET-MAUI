using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using c971.Models.c971.Models;


namespace c971.ViewModels
{
    public class CoursesViewModel : INotifyPropertyChanged
    {
        private int _termId;

        private DateTime _termStartDate;
        private DateTime _termEndDate;

        private string _termTitle; 

        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Course> _courses;

        // Observable must be an ACTUAL property to be 'observed' 
        public ObservableCollection<Course> Courses
        {
            get => _courses;

            set
            {
                _courses = value;
                OnPropertyChanged(nameof(_courses));
            }
        }

        public DateTime TermStartDate
        {
            get => _termStartDate;
            set
            {
                _termStartDate = value;
                
                OnPropertyChanged(nameof(TermStartDate));
            }
        }


        public DateTime TermEndDate
        {
            get => _termEndDate;
            set
            {
                _termEndDate = value;
                OnPropertyChanged(nameof(TermEndDate));
            }
        }

        public string TermTitle
        {
            get => _termTitle;
            set
            {
                _termTitle = value; 
                OnPropertyChanged(nameof(TermTitle)); 
            }
        }

        public CoursesViewModel(int termId)
        {
            _termId = termId;

            Courses = new ObservableCollection<Course>();

            SetTermDates();
            SetTermTitle(); 
        }

        public CoursesViewModel()
        {
        }


        //public void LoadDummyData(int id)
        //{
        //   //Courses.Clear();

        //    AddCourse(new Course
        //    {
        //        Title = "Machine Learning 101",
        //        TermId = id,
        //        StartDate = DateTime.Now,
        //        EndDate = DateTime.Now,
        //        Status = "Dropped"
        //    });

        //    AddCourse(new Course
        //    {
        //        Title = "Javascript",
        //        TermId = id,
        //        StartDate = DateTime.Now,
        //        EndDate = DateTime.Now,
        //        Status = "Current"
        //    });
        //    AddCourse(new Course
        //    {
        //        Title = "C#",
        //        TermId = id,
        //        StartDate = DateTime.Now,
        //        EndDate = DateTime.Now,
        //        Status = "Current"
        //    });


        //}

        //public async void AddCourse(Course course)
        //{
        //   App.Database.AddCourse(course);
        //   Courses.Add(course);

        //}

        public async void SetTermDates()
        {
            var term = await App.Database.GetTermById(_termId);

            TermStartDate = term.StartDate.Date;
            TermEndDate = term.EndDate.Date;




        }

        public async void SetTermTitle( )
        {
            var term = await App.Database.GetTermById( _termId );

            TermTitle = term.Title; 
        }


        public async void LoadCourses(int termId)
        {
            Courses.Clear();

            var tasks = App.Database.GetCoursesByTermId(termId);
            var courses = await tasks;

            foreach (var course in courses)
            {
                Courses.Add(course);
            }
        }

        public async Task RemoveCourse(Course course)
        {
            // Remove any notifications, just in case

            RemoveNotifications(course); 

            await App.Database.DeleteCourse(course);

            Courses.Remove(course); 

            await Task.CompletedTask;

        }


        public void RemoveNotifications(Course course)
        {
            var mainPageModel = new MainPageViewModel();
            mainPageModel.CancelCourseStartDateNotification(course);
            mainPageModel.CancelCourseEndDateNotification(course);
        }

        public bool MaxCoursesReached()
        {
            return Courses.Count > 5; 
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}