using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReportConsumer.Entities;

namespace ReportConsumer.Repositories
{
    public class JudicialContext : DbContext
    {
        public DbSet<ProcessoJudicialEntity> ProcessosJudiciais { get; set; }
        public DbSet<TaskEntity> Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=corte_pass;Username=api_user;Password=api_user");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProcessoJudicialEntity>()
                .HasKey(p => p.Numero);  

            modelBuilder.Entity<ProcessoJudicialEntity>()
                .HasOne(p => p.Task) 
                .WithMany()
                .HasForeignKey(p => p.IdTask)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<ProcessoJudicialEntity>()
                .ToTable("processosJudiciais");

            modelBuilder.Entity<TaskEntity>(entity =>
            {
                entity.ToTable("tasks");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Status).HasColumnType("CHAR(3)");
            });
        }
    }
}
