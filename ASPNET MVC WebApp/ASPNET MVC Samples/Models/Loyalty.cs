using System;
using System.ComponentModel.DataAnnotations;

namespace ASPNET_MVC_Samples.Models
{
    public class Loyalty
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? BirthDate { get; set; }

        public string Email { get; set; }
    }
}