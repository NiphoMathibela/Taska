using Taska.ModelView;
using Taska.Views;

namespace Taska.Views
{
    public partial class TasksPage : ContentPage
    {
        private TaskPageModelView _viewModel;

        public TasksPage(TaskPageModelView taskPageModelView)
        {
            InitializeComponent();
            _viewModel = taskPageModelView;
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (_viewModel != null)
            {
                try
                {
                    await _viewModel.LoadTasksAsync(); // Ensure tasks are loaded when the page appears
                }
                catch (Exception ex)
                {
                    // Handle exceptions (e.g., log them)
                }
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            // Unsubscribe from any events here if necessary
            // Example: SomeEvent -= OnSomeEvent;
        }

        public async void OnOpenModal(object sender, EventArgs e)
        {
            if (_viewModel != null)
            {
                await Shell.Current.GoToAsync("taskForm");
            }
        }
    }
}