using System.Text;
using System.Threading.Tasks;
using EmployeeLeaveMangApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeLeaveMangApp.Infrastructure
{
 
        public class ApplicationEmployeeContext : DbContext

        {
            public ApplicationEmployeeContext(DbContextOptions options) : base(options)
            {

            }
            DbSet<EmployeeClass> EmployeeClassDetail { get; set; }
            DbSet<LeaveDetail> LeaveDetails { get; set; }

            DbSet<ApplyPlannedLeave> ApplyPlannedLeaves { get; set; }
        }
}
