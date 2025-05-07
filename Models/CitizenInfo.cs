using System;
using System.ComponentModel.DataAnnotations;

namespace PopulationChecker.WorkerService.Models
{
    public class CitizenInfo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string NationalId { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
