using Trivia2._0.Views;

namespace Trivia2._0
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("Log", typeof(Login));
            Routing.RegisterRoute("Home", typeof(HomePage));
            Routing.RegisterRoute("Leaderboard", typeof(BestScoresPage));
            Routing.RegisterRoute("Questions", typeof(UserQuestionsPage));
            Routing.RegisterRoute("Edit", typeof(EditQuestionsPage));
            Routing.RegisterRoute("Pending", typeof(ApproveQuestionsPage));
            Routing.RegisterRoute("Admin", typeof(UserAdminPage));
            Routing.RegisterRoute("AddUser", typeof(AddUserPage));
        }
    }
}