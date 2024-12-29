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

public partial class AddAssessmentsViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private readonly Assessment _model;
    private readonly MainPageViewModel _mainPageViewModel = new MainPageViewModel();

    public ObservableCollection<string> Options { get; }

    public int Id
    {
        get => _model.Id;
        set
        {
            _model.Id = value;
            OnPropertyChanged(nameof(Id));
        }
    }

    public string Title
    {
        get => _model.Title;
        set
        {
            _model.Title = value;
            OnPropertyChanged(nameof(Title));
        }
    }

    public DateTime StartDate
    {
        get => _model.StartDate;
        set
        {
            _model.StartDate = value;
            OnPropertyChanged(nameof(StartDate));
        }
    }

    public DateTime EndDate
    {
        get => _model.EndDate;
        set
        {
            _model.EndDate = value;
            OnPropertyChanged(nameof(EndDate));
        }
    }


    public bool StartDateNotification
    {
        get => _model.StartDateNotification;
        set
        {
            _model.StartDateNotification = value;
            OnPropertyChanged(nameof(StartDateNotification));
        }
    }

    public bool EndDateNotification
    {
        get => _model.EndDateNotification;
        set
        {
            _model.EndDateNotification = value;
            OnPropertyChanged(nameof(EndDateNotification));
        }
    }


    public int StartDateNotificationId
    {
        get => _model.StartDateNotificationId;

        set
        {
            _model.StartDateNotificationId = value;
            OnPropertyChanged(nameof(StartDateNotificationId));
        }
    }

    public int EndDateNotificationId
    {
        get => _model.EndDateNotificationId;

        set
        {
            _model.EndDateNotificationId = value;
            OnPropertyChanged(nameof(EndDateNotificationId));
        }
    }


    public string SelectedType
    {
        get => _model.SelectedType;
        set
        {
            _model.SelectedType = value;
            OnPropertyChanged(nameof(SelectedType));
        }
    }


    public int CourseId
    {
        get => _model.CourseId;
        set
        {
            _model.CourseId = value;
            OnPropertyChanged(nameof(CourseId));
        }
    }

    public AddAssessmentsViewModel()
    {
        _model = new Assessment();

        Options = new ObservableCollection<string>()
        {
            "Objective Assessment",
            "Performance Assessment"
        };
    }

    public void AddAssessment(int courseId)
    {
        HandleNotifications();

        var assessment = new Assessment()
        {
            Id = -1,

            Title = _model.Title,
            StartDate = _model.StartDate,
            EndDate = _model.EndDate,
            SelectedType = _model.SelectedType,
            CourseId = courseId,

            StartDateNotification = _model.StartDateNotification,
            StartDateNotificationId = _model.StartDateNotificationId,

            EndDateNotification = _model.EndDateNotification,
            EndDateNotificationId = _model.EndDateNotificationId,
        };

        // Add to database 
        App.Database.AddAssessment(assessment);
    }


    public void HandleNotifications()
    {
        if (_model.StartDateNotification)
        {
            var id = _mainPageViewModel.GenerateUniqueId(_model.Id, "assessmentStartDate");
            _model.StartDateNotificationId = id;

            _mainPageViewModel.SetAssessmentStartDateNotification(_model);
        }

        if (_model.EndDateNotification)
        {
            var notificationId = _mainPageViewModel.GenerateUniqueId(_model.Id, "assessmentEndDate");
            _model.EndDateNotificationId = notificationId;

            _mainPageViewModel.SetAssessmentEndDateNotification(_model);
        }
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}