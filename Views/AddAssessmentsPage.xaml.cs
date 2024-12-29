
using c971.ViewModels;

namespace c971.Views;

public partial class AddAssessmentsPage : ContentPage
{
    private int _courseId; 
    public AddAssessmentsPage()
    {
        InitializeComponent();
        BindingContext = new AddAssessmentsViewModel(); 

    }

    public AddAssessmentsPage( int courseId)
    {
        _courseId = courseId; 

        InitializeComponent();
    }

    public void OnSaveClicked(object sender, EventArgs e)
    {

        var viewModel = BindingContext as AddAssessmentsViewModel;


        // Comment this out to skip validation 
        if (viewModel != null && IsValidated(viewModel)) 
        {

            // Try block here to limit number of assessments 

            viewModel.AddAssessment(_courseId);

            // Push updated page to the stack 
            //var assessmentsPage = new AssessmentsPage(_courseId);
            //Navigation.PushAsync(assessmentsPage);

            DisplayAlert("Saved", "Assessment added.", "OK"); 

            var mainPage = new MainPage(); 
            Navigation.PushAsync( mainPage );

        }
    }



    public bool IsValidated( AddAssessmentsViewModel viewModel )
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
