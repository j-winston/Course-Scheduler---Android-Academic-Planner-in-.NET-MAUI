
using c971.ViewModels;
using c971.Models;


namespace c971.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage( )
        {
            InitializeComponent();

            // Set the window size to mirror android screen 

            //Microsoft.Maui.Controls.Application.Current.MainPage.WidthRequest = 235;
            //Microsoft.Maui.Controls.Application.Current.MainPage.HeightRequest = 497;

            BindingContext = new MainPageViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = new MainPageViewModel();

            var MainPageViewModel  = new MainPageViewModel();
            MainPageViewModel.LoadAllTerms();
     
        }

        private async void OnEditTermClicked( object sender, EventArgs e )
        {
            var button = sender as Button;
            var term = button?.BindingContext as Term;

            if ( term != null )
            {
                var editPage = new EditTermPage( new EditTermViewModel( term ) );
                await Navigation.PushAsync( editPage );
            }
            else
            {
                await DisplayAlert( "Display Error", "There's no term to edit", "OK" );
            }
        }

        private async void OnDeleteTermClicked( object sender, EventArgs e )
        {
            var button = sender as Button;
            var term = button?.BindingContext as Term;

            if ( term != null )
            {
                var mainPageViewModel = BindingContext as MainPageViewModel;
                if ( mainPageViewModel != null )
                {
                    await mainPageViewModel.RemoveTerm( term );
                }
            }

            
        }

        private async void OnAddTermClicked( object sender, EventArgs e )
        {
            if (BindingContext is MainPageViewModel viewModel)
            {
                if (viewModel != null && !viewModel.MaxTermsReached())
                {
                    var addPage = new AddTermPage();
                    await Navigation.PushAsync( addPage );
                }
                else
                {
                    DisplayAlert("Max terms reached", "Unable to add more than 3 terms", "OK"); 
                }
            }
           
        }

        private async void OnTermLabelTapped( object sender, EventArgs e )
        {
            var label = sender as Label;
            var term = label?.BindingContext as Term;

            if ( term != null )
            {
                var coursesPage = new CoursesPage(term.Id);
                
                await Navigation.PushAsync( coursesPage );

               
            }
        }
    }
}
