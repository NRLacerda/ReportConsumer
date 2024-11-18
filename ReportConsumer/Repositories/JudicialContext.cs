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
            modelBuilder.Entity<ProcessoJudicialEntity>(entity =>
            {
                entity.ToTable("processosJudiciais", "public"); 

                entity.HasKey(p => p.Numero); 

                entity.HasOne(p => p.Task) 
                      .WithMany()
                      .HasForeignKey(p => p.IdTask)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.Property(p => p.DateProcessed)
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("NOW()");
            });


            modelBuilder.Entity<TaskEntity>(entity =>
            {
                entity.ToTable("tasks", "public");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Status).HasColumnType("CHAR(3)");
                entity.Property(e => e.Started)
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("NOW()");
            });

        }
    }
}
