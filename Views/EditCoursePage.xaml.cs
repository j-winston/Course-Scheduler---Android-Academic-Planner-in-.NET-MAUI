using System.Formats.Asn1;
using c971.Models;
using c971.Models.c971.Models;
using Microsoft.Maui.Controls;
using c971.ViewModels;
using c971.Views;

namespace c971
{
    public partial class EditCoursePage
    {
        private MainPageViewModel _mainPageViewModel = new();

        public EditCoursePage(Course course)
        {
            InitializeComponent();

            BindingContext = new EditCourseViewModel(course);
        }

        public void OnShareNotesClicked(object sender, EventArgs e)
        {
            var button = sender as Button; 

            if (BindingContext is EditCourseViewModel viewModel)
            {
                var notes  = viewModel.Notes;

                ShareNotes(notes); 
            }
        }

        public async void ShareNotes(string text)
        {
            await Share.Default.RequestAsync( new ShareTextRequest
            {
                Text = text,
                Title = "Share Text"
            } );

      
        }


        public async void OnSaveClicked(object sender, EventArgs e)

            // if you want to use a _model property you will have to reconstruct the model based on 
            // view model properties. 
        {
            var viewModel = BindingContext as EditCourseViewModel; // check if selected status exists

            if (viewModel != null && IsValidated(viewModel))
            {
                viewModel.SaveCourse();
                UpdateNotifications();

                var coursesPage = new CoursesPage(viewModel.TermId);
                await Application.Current.MainPage.DisplayAlert("Success", "Course updated", "OK");

                Navigation.PushAsync(coursesPage);
            }
        }

        public bool IsValidated(EditCourseViewModel viewModel)
        {
            if (string.IsNullOrWhiteSpace(viewModel.Title))
            {
                DisplayAlert("Validation Error", "Title is required.", "OK");
                return false;
            }

            if (viewModel.StartDate == default(DateTime))
            {
                DisplayAlert("Validation Error", "Start date is required.", "OK");
                return false;
            }

            if (viewModel.EndDate == default(DateTime))
            {
                DisplayAlert("Validation Error", "End date is required.", "OK");
                return false;
            }

            if (viewModel.EndDate < viewModel.StartDate)
            {
                DisplayAlert("Validation Error", "End date must be after the start date.", "OK");
                return false;
            }

            if (string.IsNullOrWhiteSpace(viewModel.SelectedStatus))
            {
                DisplayAlert("Validation Error", "Status is required.", "OK");
                return false;
            }

            if (string.IsNullOrWhiteSpace(viewModel.InstructorName))
            {
                DisplayAlert("Validation Error", "Instructor name is required.", "OK");
                return false;
            }

            if (string.IsNullOrWhiteSpace(viewModel.InstructorPhone))
            {
                DisplayAlert("Validation Error", "Instructor phone is required.", "OK");
                return false;
            }

            if (!IsValidPhoneNumber(viewModel.InstructorPhone))
            {
                DisplayAlert("Validation Error", "Instructor phone must contain only digits.", "OK");
                return false;
            }


            if (string.IsNullOrWhiteSpace(viewModel.InstructorEmail))
            {
                DisplayAlert("Validation Error", "Instructor email is required.", "OK");
                return false;
            }

            if (!IsValidEmail(viewModel.InstructorEmail))
            {
                DisplayAlert("Validation Error", "Instructor email is not valid.", "OK");
                return false;
            }


            return true;
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            int atIndex = email.IndexOf("@");
            int dotIndex = email.LastIndexOf(".");


            if (atIndex < 1 || atIndex == email.Length - 1)
                return false;

            if (dotIndex <= atIndex + 1 || dotIndex == email.Length - 1)
                return false;

            return true;
        }

        private bool IsValidPhoneNumber(string phone)
        {
            foreach ( var character in phone )
            {
                if ( !char.IsDigit( character ) && character != '-' )
                {
                    return false;
                }
            }

            return true;
        }


        public void UpdateNotifications()
        {
            var viewModel = BindingContext as EditCourseViewModel;
            var course = new Course()
            {
                Id = viewModel.Id,
                Title = viewModel.Title,

                StartDate = viewModel.StartDate,
                EndDate = viewModel.EndDate,
                SelectedStatus = viewModel.SelectedStatus,
                InstructorName = viewModel.InstructorName,
                InstructorPhone = viewModel.InstructorPhone,
                InstructorEmail = viewModel.InstructorEmail,
                Notes = viewModel.Notes,

                StartDateNotification = viewModel.StartDateNotification,
                StartDateNotificationId = viewModel.StartDateNotificationId,
                EndDateNotification = viewModel.EndDateNotification,
                EndDateNotificationId = viewModel.EndDateNotificationId,

                TermId = viewModel.TermId
            };

            // if theres no notification set at time of update 
            // cancel existing just in case

            // if there is a notification set, cancel and set again 

            if (course.StartDateNotification)
            {
                _mainPageViewModel.CancelCourseStartDateNotification(course);
                course.StartDateNotificationId = _mainPageViewModel.GenerateUniqueId(course.Id, "courseStartDate");

                _mainPageViewModel.SetCourseStartDateNotification(course);
            }
            else
            {
                _mainPageViewModel.CancelCourseStartDateNotification(course);
            }

            if (course.EndDateNotification)
            {
                _mainPageViewModel.CancelCourseEndDateNotification(course);
                _mainPageViewModel.SetCourseEndDateNotification(course);
            }
            else
            {
                _mainPageViewModel.CancelCourseEndDateNotification(course);
            }
        }
    }
}