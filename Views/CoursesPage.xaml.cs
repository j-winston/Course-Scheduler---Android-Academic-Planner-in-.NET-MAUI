using System.Collections.ObjectModel;
using c971.Models;
using c971.Models.c971.Models;
using Microsoft.Maui.Controls;
using c971.ViewModels;

namespace c971.Views
{
    [QueryProperty(nameof(_termId), "TermId")] // TODO omit if possible 
    public partial class CoursesPage
    {

        private readonly int _termId;

        public CoursesPage(int termId)
        {
            InitializeComponent();
            _termId = termId;

            BindingContext = new CoursesViewModel(_termId);
            var coursesViewModel = BindingContext as CoursesViewModel; 

            coursesViewModel?.LoadCourses(_termId);

        }


        public async void OnAddCourseClicked(object sender, EventArgs e) // TODO is best practice to use async here?
        {
            // Open add course page 
            if (BindingContext is CoursesViewModel viewModel)
            {
                if (!viewModel.MaxCoursesReached())
                {
                    var addCourseView = new AddCoursePage( _termId );
                    await Navigation.PushAsync( addCourseView );

                }
                else
                {
                    DisplayAlert("Max courses reached", "Unable to add more than six courses.", "OK"); 
                }
            }
         
        }

        public async void OnEditCourseClicked(object sender, EventArgs e) // TODO is best practice to use async here?
        {
            // Get course that was clicked on 
            var button = sender as Button;
            var course = button?.BindingContext as Course;

            if (course != null)
            {
                // populate new edit page with that course
                var editCoursePage = new EditCoursePage(course);
                await Navigation.PushAsync(editCoursePage);
            }
            else
            {
                await DisplayAlert("Display Error", "There's no term to edit", "OK");
            }
        }

        public async void OnDeleteCourseClicked( object sender, EventArgs e )
        {

            var button = sender as Button; 
            var course = button?.BindingContext as Course;

           // var coursesViewModel = BindingContext as CoursesViewModel;
            
            if (BindingContext is CoursesViewModel viewModel)
            {
                await viewModel.RemoveCourse(course);
            }

        }
        public async void OnAssessmentsClicked( object sender, EventArgs e )
        {
            var button = sender as Button;
            var course = button?.BindingContext as Course;

            // open assessments view
            var assessmentsPage = new AssessmentsPage(course.Id);
            await Navigation.PushAsync(assessmentsPage); 


        }
    }
}