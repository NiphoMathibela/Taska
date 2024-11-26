using CommunityToolkit.Maui;

namespace Taska
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

        }
    }
}
