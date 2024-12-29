using Microsoft.Maui.Controls;
using c971.Views;

namespace c971
{
    public partial class AppShell : Shell
    {
        public AppShell( )
        {
            InitializeComponent();

            // Register routes for all pages
            Routing.RegisterRoute( nameof( MainPage ), typeof( MainPage ) );
            Routing.RegisterRoute( nameof( AddTermPage ), typeof( AddTermPage ) );
            Routing.RegisterRoute( nameof( EditTermPage ), typeof( EditTermPage ) );
            Routing.RegisterRoute( nameof( CoursesPage ), typeof( CoursesPage ) );
            Routing.RegisterRoute( nameof( AddCoursePage ), typeof( AddCoursePage ) );
            Routing.RegisterRoute( nameof( EditCoursePage ), typeof( EditCoursePage ) );
            Routing.RegisterRoute(nameof(AssessmentsPage), typeof(AssessmentsPage));
        }
    }
}