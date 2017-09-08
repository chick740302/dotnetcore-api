using System.ComponentModel.DataAnnotations;

namespace dotnetcore_api.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int Grade { get; set; }

        public char ClassName { get; set; }
    }
}