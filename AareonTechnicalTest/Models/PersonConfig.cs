using Microsoft.EntityFrameworkCore;

namespace AareonTechnicalTest.Models
{
    public static class PersonConfig
    {
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>(
                entity =>
                {
                    entity.HasKey(e => e.Id);
                });

            modelBuilder.Entity<Note>(
                entity =>
                {
                    entity.HasKey(e => e.Id);
                });
        }
    }
}