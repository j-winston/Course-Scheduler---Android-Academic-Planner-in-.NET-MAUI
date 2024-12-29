
using c971.Models;
using c971.ViewModels;

namespace c971.Views;

public partial class EditAssessmentPage
{
    public EditAssessmentPage(Assessment assessment)
    {
        InitializeComponent();
        BindingContext = new EditAssessmentViewModel(assessment);


    }

    public async void OnSaveClicked(object sender, EventArgs e)
    {
        if (BindingContext is EditAssessmentViewModel viewModel)
        {
            if (IsValidated(viewModel))
            {
                viewModel.SaveAssessment();

                UpdateNotifications();

                var assessmentsPage = new AssessmentsPage( viewModel.CourseId );
                await Application.Current.MainPage.DisplayAlert( "Success", "Assessment updated", "OK" );

                await Navigation.PushAsync( assessmentsPage );

            }
           

        }
    }

    public void UpdateNotifications()
    {
        var viewModel = BindingContext as  EditAssessmentViewModel;
        var mainPageViewModel = new MainPageViewModel(); 

        var assessment = new Assessment()
        {
            Id = viewModel.Id,
            CourseId = viewModel.CourseId,

            Title = viewModel.Title,

            StartDate = viewModel.StartDate,
            EndDate = viewModel.EndDate,

            SelectedType = viewModel.SelectedType,

            StartDateNotification = viewModel.StartDateNotification,
            StartDateNotificationId = viewModel.StartDateNotificationId,
            EndDateNotification = viewModel.EndDateNotification,
            EndDateNotificationId = viewModel.EndDateNotificationId,

        };


        if ( assessment.StartDateNotification )
        {
            mainPageViewModel.CancelAssessmentStartDateNotification( assessment );
            assessment.StartDateNotificationId = mainPageViewModel.GenerateUniqueId( assessment.Id, "assessmentStartDate" );

            mainPageViewModel.SetAssessmentStartDateNotification( assessment );
        }
        else
        {
            mainPageViewModel.CancelAssessmentStartDateNotification( assessment );
        }

        if ( assessment.EndDateNotification )
        {
            mainPageViewModel.CancelAssessmentEndDateNotification( assessment );
            mainPageViewModel.SetAssessmentEndDateNotification( assessment );
        }
        else
        {
            mainPageViewModel.CancelAssessmentEndDateNotification( assessment );
        }
    }

    public bool IsValidated( EditAssessmentViewModel viewModel )
    {
        if ( string.IsNullOrWhiteSpace( viewModel.Title ) )
        {
            DisplayAlert( "Validation Error", "Title is required.", "OK" );
            return false;
        }

        if ( viewModel.StartDate == default( DateTime ) )
        {
            DisplayAlert( "Validation Error", "Start date is required.", "OK" );
            return false;
        }

        if ( viewModel.EndDate == default( DateTime ) )
        {
            DisplayAlert( "Validation Error", "End date is required.", "OK" );
            return false;
        }

        if ( viewModel.EndDate < viewModel.StartDate )
        {
            DisplayAlert( "Validation Error", "End date must be after the start date.", "OK" );
            return false;
        }

        if ( string.IsNullOrWhiteSpace( viewModel.SelectedType ) )
        {
            DisplayAlert( "Validation Error", "Assessment type is required.", "OK" );
            return false;
        }



        return true;
    }
}