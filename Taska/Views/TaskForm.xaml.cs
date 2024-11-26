using Taska.ModelView;

namespace Taska.Views;

public partial class TaskForm : ContentPage
{
    public TaskForm(TaskFormViewModel taskFormViewModel)
    {
        InitializeComponent();

        // Set up the database path and helper
        string databasePath = Path.Combine(FileSystem.AppDataDirectory, "tasks.db");
        var databaseHelper = new DatabaseHelper(databasePath);

        BindingContext = taskFormViewModel;
    }

    public async void OnCloseModal(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("home");
    }

    protected override bool OnBackButtonPressed()
    {
        // Navigate to the home page or handle the back navigation logic
        Shell.Current.GoToAsync("home");
        return true; // Indicate that the back button press has been handled
    }

    //public async void OnCloseModal(object sender, EventArgs e)
    //{
    //    await Navigation.PopModalAsync();
    //}
}