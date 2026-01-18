using Estatia.Models;
namespace Estatia.Models.ViewModels
{
    public class AdminDashboardViewModel
    {
        
        public int TotalUsers { get; set; }
        public int TotalAgents { get; set; }
        public int TotalProperties { get; set; }

        
        public List<Agent> Agents { get; set; }
        public List<User> Users { get; set; }
    }
}