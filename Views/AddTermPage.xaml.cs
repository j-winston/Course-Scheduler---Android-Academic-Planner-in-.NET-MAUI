 using Microsoft.Maui.Controls;
using c971.Models;
using c971.ViewModels;
using System;
using System.Linq;

namespace c971.Views
{
    public partial class AddTermPage : ContentPage
    {
        public AddTermPage( )
        {
            InitializeComponent();
            BindingContext = new AddTermViewModel();
        }

        private async void OnSubmitClicked( object? sender, EventArgs e )
        {
            try
            {
                var viewModel = BindingContext as AddTermViewModel;

                if ( viewModel == null  )
                {
                    await DisplayAlert( "Error", "No viewmodel here.", "OK" );
                    return;
                }

                var term = new Term
                {
                    Title = viewModel.Title,
                    StartDate = viewModel.StartDate,
                    EndDate = viewModel.EndDate
                };

                if (IsValidated(viewModel))
                {
                    await App.Database.AddTerm(term);

                    // Send a message to MainPage to update the term list

                    MessagingCenter.Send(this, "AddTerm", term);

                    await Navigation.PopAsync(); 
                    await DisplayAlert("Term Submitted", "New term added", "OK");
                }

            }
            catch ( Exception ex )
            {
                await DisplayAlert( "Error", $"Unable to add: {ex.Message}", "OK" );
            }
        }

        public bool IsValidated(AddTermViewModel viewModel)
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