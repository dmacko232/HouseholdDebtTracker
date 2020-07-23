using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using HouseholdDebtTracker.DAL.Models;

namespace HouseholdDebtTracker.DAL.Data
{
    /// <summary>
    /// Debt tracker context that is used as database EF core context
    /// </summary>
    public class DebtTrackerContext : DbContext
    {
        public DbSet<Person> People { get; set; }

        public DbSet<Debt> Debts { get; set; }

        public DebtTrackerContext(DbContextOptions<DebtTrackerContext> options) : base(options) {}

    }
}
