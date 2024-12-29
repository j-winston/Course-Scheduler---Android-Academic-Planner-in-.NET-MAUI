using System.Collections.ObjectModel;
using System.ComponentModel;
using c971.Models;


namespace c971.ViewModels;

public class AssessmentsViewModel : INotifyPropertyChanged
{
    private readonly int _courseId;

    private DateTime _startDate;
    private DateTime _endDate;
    private string _title;
    private ObservableCollection<string> _options;
    private string _selectedStatus;

    private ObservableCollection<Assessment> _assessments;

    public event PropertyChangedEventHandler PropertyChanged;


    public ObservableCollection<Assessment> Assessments
    {
        get => _assessments;

        set
        {
            _assessments = value;
            OnPropertyChanged(nameof(_assessments));
        }
    }

    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            OnPropertyChanged(nameof(Title));
        }
    }

    public DateTime StartDate
    {
        get => _startDate;
        set
        {
            _startDate = value;

            OnPropertyChanged(nameof(StartDate));
        }
    }


    public DateTime EndDate
    {
        get => _endDate;
        set
        {
            _endDate = value;
            OnPropertyChanged(nameof(EndDate));
        }
    }


    public string SelectedStatus
    {
        get => _selectedStatus;
        set
        {
            _selectedStatus = value;
            OnPropertyChanged(nameof(SelectedStatus));
        }
    }


    public ObservableCollection<string> Options
    {
        get => _options;
        set
        {
            _options = value;
            OnPropertyChanged(nameof(Options));
        }
    }

    public AssessmentsViewModel(int courseId)
    {
        _courseId = courseId;

        Assessments = new ObservableCollection<Assessment>();

        Options = new ObservableCollection<string>()
        {
            "Objective Assessment",
            "Performance Assessment"
        };
    }

    public AssessmentsViewModel()
    {

    }

    // create a method that checks the length of assessments to prevent > 2 being added. 
    // repeat for courses and term view model 

    public bool MaxAssessmentsReached( )
    {
        return Assessments.Count > 1; 
    }

    public async void LoadAssessments()
    {
        Assessments.Clear();

        var tasks = App.Database.GetAssessmentsByCourseId(_courseId);
        var assessments = await tasks;

        foreach (var assessment in assessments)
        {
            Assessments.Add(assessment);
        }

       
    }

    public async Task RemoveAssessment( Assessment assessment )
    {
        
        RemoveNotifications(assessment); 

        await App.Database.DeleteAssessment( assessment );

        Assessments.Remove(assessment );
    }

    public void RemoveNotifications( Assessment assessment )
    {
        var mainPageModel = new MainPageViewModel();
        mainPageModel.CancelAssessmentStartDateNotification( assessment );
        mainPageModel.CancelAssessmentEndDateNotification( assessment );
    }

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}