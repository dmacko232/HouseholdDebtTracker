using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using HouseholdDebtTrackerDAL.Models;

namespace HouseholdDebtTrackerDAL.Data
{
    public class DebtTrackerContext : DbContext
    {
        public DbSet<PersonModel> People { get; set; }

        public DbSet<DebtModel> Debts { get; set; }

        public DebtTrackerContext(DbContextOptions<DebtTrackerContext> options) : base(options) {}

    }
}