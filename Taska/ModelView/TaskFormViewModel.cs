using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Taska.Models;

namespace Taska.ModelView
{
    public partial class TaskFormViewModel : ObservableObject
    {
        private readonly DatabaseHelper _databaseHelper;

        public ObservableCollection<TaskModel> Tasks { get; } = new ObservableCollection<TaskModel>();

        [ObservableProperty]
        private string taskName;

        [ObservableProperty]
        private string taskDescription;

        [ObservableProperty]
        private bool isCompleted;

        public TaskFormViewModel(DatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }

        [RelayCommand]
        public async Task AddTask()
        {
            // Create a new TaskModel instance
            var newTask = new TaskModel
            {
                Name = TaskName,
                Description = TaskDescription,
                IsCompleted = IsCompleted
            };

            // Add the task to the database
            await _databaseHelper.AddTaskAsync(newTask);

            // Add the task to the ObservableCollection
            Tasks.Add(newTask);

            // Display alert
            await DisplayAlert();

            // Clear the input fields
            TaskName = string.Empty;
            TaskDescription = string.Empty;
            IsCompleted = false;
        }

        [RelayCommand]
        public async Task DisplayAlert()
        {
            await Application.Current.MainPage.DisplayAlert("Task Added!", $"Your task '{TaskName}' has been added.", "Ok");
        }
    }
}