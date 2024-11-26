using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Taska.Models;

namespace Taska.ModelView
{
    public partial class TaskPageModelView : ObservableObject
    {
        private readonly DatabaseHelper _databaseHelper; // Private field for DatabaseHelper
        public ObservableCollection<TaskModel> Tasks { get; } = new ObservableCollection<TaskModel>();

        [ObservableProperty]
        private string taskName;

        [ObservableProperty]
        private string taskDescription;

        [ObservableProperty]
        private bool isCompleted;

        // Public property to access the DatabaseHelper
        public DatabaseHelper DatabaseHelper => _databaseHelper;

        public TaskPageModelView(DatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper; // Correctly assign the database helper

            // Adding mock data to the Tasks collection for initial testing
            //Tasks.Add(new TaskModel { Id = 1, Name = "Task 1", Description = "Description for Task 1", IsCompleted = false });
            //Tasks.Add(new TaskModel { Id = 2, Name = "Task 2", Description = "Description for Task 2", IsCompleted = true });
            //Tasks.Add(new TaskModel { Id = 3, Name = "Task 3", Description = "Description for Task 3", IsCompleted = false });
        }

        // Initialize method to load tasks asynchronously
        public async Task InitializeAsync()
        {
            await LoadTasksAsync(); // Load tasks from the database
        }

        // Method to load tasks from the database
        public async Task LoadTasksAsync()
        {
            try
            {
                var tasks = await _databaseHelper.GetTasksAsync();
                Tasks.Clear(); // Clear existing tasks

                foreach (var task in tasks)
                {
                    Tasks.Add(task); // Add each task to the ObservableCollection
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading tasks: {ex.Message}");
            }
        }

        // Command to navigate to the task form
        [RelayCommand]
        public async Task GoToForm()
        {
            await Shell.Current.GoToAsync("taskForm");
        }

        // Command to display an alert when a task is added
        [RelayCommand]
        public async Task DisplayAlert()
        {
            await Application.Current.MainPage.DisplayAlert("Task Added!", $"Your task '{TaskName}' with description '{TaskDescription}' has been added.", "Ok");
        }

        // Command to add a new task
        [RelayCommand]
        public void AddTask()
        {
            if (!string.IsNullOrWhiteSpace(TaskName) && !string.IsNullOrWhiteSpace(TaskDescription)
                && Tasks.Count < 100) // Optional: Limit the number of tasks
            {
                Tasks.Add(new TaskModel { Id = Tasks.Count + 1, Name = TaskName, Description = TaskDescription, IsCompleted = IsCompleted });
                TaskName = string.Empty; // Clear input fields after adding
                TaskDescription = string.Empty; // Clear input fields after adding
            }
        }
    }
}