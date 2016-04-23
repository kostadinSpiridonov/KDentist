using KDentist.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDentist.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<MDT> MDTs { get; set; }
        public DbSet<DentalProcedure> DentalProcedures { get; set; }
        public DbSet<ProceduresTable> ProceduresTables { get; set; }
        public DbSet<ProcedureCell> ProceduresCells { get; set; }
        public DbSet<DentistAppointment> DentistAppontments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Disease> Diseases { get; set; }
        public DbSet<Diagnose> Diagnoses { get; set; }

        /// <summary>
        /// Define the relations between tables
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>().HasMany(x => x.DentalProcedures);
            modelBuilder.Entity<Patient>().HasMany(x => x.Diseases);
            modelBuilder.Entity<ProceduresTable>().HasMany(x => x.Cells);
            base.OnModelCreating(modelBuilder);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
