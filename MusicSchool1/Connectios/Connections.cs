using Microsoft.EntityFrameworkCore;
using MusicSchool1.Models;
using Microsoft.Extensions.Configuration;

namespace MusicSchool1.Connectios
{
    // ультимативные класс подключение к базе данных MS SQL Server (P.S. - Trusted_Connection,TrustServerCertificate используються для 
    // решения проблемы с сертификатами при подключении БД
    public class BaseDbContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           /* try
            {*/
                optionsBuilder.UseSqlServer("Data Source=USER-PC;Initial Catalog=SchoolMusic;Integrated Security=True; Trusted_Connection=True; TrustServerCertificate=True");
           /* }
            catch (Exception ex) { 
                Console.WriteLine(ex.ToString());
            }*/
        }

    }

    public class SpecialityDB : BaseDbContext
    {
        public DbSet<Speciality> Speciality { get; set; }
    }

    public class GenresDB : BaseDbContext
    {
        public DbSet<Genres> Genre { get; set; }
    }

    public class PositionDB : BaseDbContext
    {
        public DbSet<Position> Position { get; set; }
    }

    public class EmployeeDB : BaseDbContext
    {
        public DbSet<Employee> Employee { get; set; }
    }

    public class GroupMusicDB : BaseDbContext
    {
        public DbSet<GroupMusic> GroupMusic { get; set; }
    }

    public class StudentDB : BaseDbContext
    {
        public DbSet<Student> Student { get; set; }
    }

    public class ConcertDB : BaseDbContext
    {
        public  DbSet<Concert> Concert { get;set; }
    }
}

 
