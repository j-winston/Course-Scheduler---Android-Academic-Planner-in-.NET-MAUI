using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using c971.Models;
using c971.Models.c971.Models;

namespace c971.ViewModels;

public partial class EditAssessmentViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private readonly Assessment? _model;

    public ObservableCollection<string> Options { get; }

    public int Id
    {
        get => _model.Id;
        set
        {
            _model.Id = value;
            OnPropertyChanged( nameof( Id ) );
        }
    }
    public string Title
    {
        get => _model.Title;
        set
        {
            _model.Title = value;
            OnPropertyChanged( nameof( Title ) );
        }
    }

    public DateTime StartDate
    {
        get => _model.StartDate;
        set
        {
            _model.StartDate = value;
            OnPropertyChanged( nameof( StartDate ) );
        }
    }

    public DateTime EndDate
    {
        get => _model.EndDate;
        set
        {
            _model.EndDate = value;
            OnPropertyChanged( nameof( EndDate ) );
        }
    }



    public bool StartDateNotification
    {
        get => _model.StartDateNotification;
        set
        {
            _model.StartDateNotification = value;
            OnPropertyChanged( nameof( StartDateNotification ) );
        }
    }

    public bool EndDateNotification
    {
        get => _model.EndDateNotification;
        set
        {
            _model.EndDateNotification = value;
            OnPropertyChanged( nameof( EndDateNotification ) );
        }
    }


    public int StartDateNotificationId
    {
        get => _model.StartDateNotificationId;

        set
        {
            _model.StartDateNotificationId = value;
            OnPropertyChanged( nameof( StartDateNotificationId ) );
        }
    }

    public int EndDateNotificationId
    {
        get => _model.EndDateNotificationId;

        set
        {
            _model.EndDateNotificationId = value;
            OnPropertyChanged( nameof( EndDateNotificationId ) );
        }
    }

    public string SelectedType
    {
        get => _model.SelectedType;
        set
        {
            _model.SelectedType = value;
            OnPropertyChanged( nameof( SelectedType ) );
        }
    }


    public int CourseId
    {
        get => _model.CourseId;
        set
        {
            _model.CourseId = value;
            OnPropertyChanged( nameof( CourseId ) );
        }
    }


    public EditAssessmentViewModel(Assessment assessment )
    {
        _model = assessment; 

        Options = new ObservableCollection<string>()
        {
            "Objective Assessment",
            "Performance Assessment"
        };


    }

    public EditAssessmentViewModel()
    {
        _model = new Assessment();
    }

    public void SaveAssessment()
    {
        App.Database.SaveAssessment(_model); 
    }

    protected virtual void OnPropertyChanged( string propertyName )
    {
        PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( propertyName ) );
    }
}