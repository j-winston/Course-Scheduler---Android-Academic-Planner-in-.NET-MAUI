using System.Net.Http.Headers;
using c971.Models.c971.Models;
using c971.ViewModels;
using Plugin.LocalNotification;


namespace c971.Views
{
    public partial class AddCoursePage : ContentPage
    {
        private readonly int _termId;

        public AddCoursePage()
        {
            InitializeComponent();
            BindingContext = new AddCourseViewModel();
        }

        public AddCoursePage(int termId)
        {
            _termId = termId;

            InitializeComponent();
        }

        public void OnSaveClicked(object sender, EventArgs e)
        {
            var viewModel = BindingContext as AddCourseViewModel;

            // Comment this OUT to skip validation 
            if (viewModel != null && IsValidated(viewModel))
            {
                viewModel.AddCourse(_termId);

                // Push updated page to the stack 
                var coursesPage = new CoursesPage(_termId);
                Navigation.PushAsync(coursesPage);
            }


            // Comment this IN to skip validation
            //viewModel?.AddCourse( _termId );

            //// Push updated page to the stack 
            //var coursesPage = new CoursesPage( _termId );
            //Navigation.PushAsync( coursesPage );
        }


        public bool IsValidated(AddCourseViewModel viewModel)
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
            // Ensure only numbers and separators

            foreach (var character in phone)
            {
                if (!char.IsDigit(character) && character !='-')
                {
                    return false; 
                }
            }

            return true; 
        }
    }
}