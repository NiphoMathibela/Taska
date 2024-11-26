using Taska.Views;

namespace Taska
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("taskForm", typeof(TaskForm));
            Routing.RegisterRoute("home", typeof(TasksPage));
        }
    }
}
