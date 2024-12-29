using c971.Models;
using c971.ViewModels;

namespace c971.Views;

public partial class AssessmentsPage
{
    private readonly int _courseId;

    public AssessmentsPage(int courseId)
    {
        InitializeComponent();
        _courseId = courseId;

        BindingContext = new AssessmentsViewModel(_courseId);
        var assessmentsViewModel = BindingContext as AssessmentsViewModel;

        assessmentsViewModel?.LoadAssessments();
    }

    public async void OnEditAssessmentClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var assessment = button?.BindingContext as Assessment;

        if (BindingContext is AssessmentsViewModel viewModel && assessment != null)
        {
            var editAssessmentPage = new EditAssessmentPage(assessment);
            await Navigation.PushAsync(editAssessmentPage);
        }
        else
        {
            await DisplayAlert("Display Error", "There's no assessment to edit", "OK");
        }
    }


    public async void OnDeleteAssessmentClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var assessment = button?.BindingContext as Assessment;


        if (BindingContext is AssessmentsViewModel assessmentsViewModel)
        {
            if (assessment != null)
            {
                await assessmentsViewModel.RemoveAssessment(assessment);
            }
        }
    }

    public async void OnAddAssessmentClicked(object sender, EventArgs e)
    {

        if (BindingContext is AssessmentsViewModel viewModel)
        {
            if (!viewModel.MaxAssessmentsReached())
            {
                var addAssessmentsPage = new AddAssessmentsPage( _courseId );
                await Navigation.PushAsync( addAssessmentsPage );
            }
            else
            {
                DisplayAlert( "Max assessments reached", "No more than two assessments may be added.", "OK" );
            }

        }

    }
}