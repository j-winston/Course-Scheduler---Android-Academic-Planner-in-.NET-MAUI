using System.Collections.ObjectModel;
using c971.Models;
using c971.ViewModels;


namespace c971
{
    public partial class EditTermPage 
    {

        public EditTermPage(EditTermViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel; 

            // Routing.RegisterRoute(nameof(EditTermPage), typeof(EditTermPage));
        }

     
        public async void OnSaveClicked(object? sender, EventArgs e)
        {
            var viewModel = BindingContext as EditTermViewModel;

            if (viewModel != null && IsValidated(viewModel))
            {
                await viewModel.SaveTerm(); 
                await Navigation.PopAsync(); 
            }


        }



        public bool IsValidated( EditTermViewModel viewModel )
        {
            // Check if Title is blank before submitting 

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

            // End date must be after start date

            if ( viewModel.EndDate < viewModel.StartDate )
            {
                DisplayAlert( "Validation Error", "End date must be after the start date.", "OK" );
                return false;
            }

            return true;
        }
    }
   
}