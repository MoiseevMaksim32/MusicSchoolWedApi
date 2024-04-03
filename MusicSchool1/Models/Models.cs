namespace MusicSchool1.Models
{
    public interface IEntity {
         int ID { get; set; }
    }


    public class Speciality : IEntity
    {
        
        public int ID { get; set; }
        public string SpecialityName { get; set; }
    }

    public class Genres : IEntity
    {
        public int ID { get; set; }
        public string GenreName { get; set; }
    }

    public class Position : IEntity
    {
        public int ID { get; set; }
        public string PositionName { get; set; }
    }

    public class Employee : IEntity
    {
        public int ID { get; set; }
        public int PositionID { get; set; }
        public string Fio { get; set; }
        public string Telephone { get; set; }
        public bool Gender { get; set; }
        public byte Experience { get; set; }
        public string Email { set; get; }
    }

    public class GroupMusic : IEntity
    {
        public int ID { get; set; }
        public string GroupMusicName { get; set; }
        public int EmployeeTeacherID { get; set; }
        public int EmployeeAccompanistID { get; set; }

    }

    public class Student : IEntity
    {
        public int ID { get; set; }
        public int GroupMusicID { get; set; }
        public int SpecilityID { get; set; }
        public string Fio { get; set; }
        public string Telephone { get; set; }
        public bool Gender { get; set; }   
        public string Email { get; set; }

    }

    public class Concert : IEntity
    {
        public int ID { get; set; }
        public int GroupMusicID { get; set; }
        public int GenreID { get; set; }
        public DateTime ConcertDate { get; set; }
    }
}
