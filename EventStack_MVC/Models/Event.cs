using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using EventStack_MVC.Interfaces;
using System.ComponentModel;

namespace EventStack_MVC.Models
{
    public class Event : IDbModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Name must be defined!")]
        [StringLength(50, ErrorMessage = "The maximum number of character is 50!")]
        public string Name { get; set; }

        public IEnumerable<string> CategoriesId { get; set; }

        [Required(ErrorMessage = "Photo must be added for event!")]
        public byte[] Photo { get; set; }

        [StringLength(1000, ErrorMessage = "The maximum number of character is 1000!")]
        [Required(ErrorMessage = "Event must have short description!")]
        public string Description { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public DateTime PublishTime { get; set; }

        [Required(ErrorMessage = "Place for event must be set")]
        public Address Place { get; set; }

        [DefaultValue(false)]
        public bool IsCanceled { get; set; }

        public string FacebookURL { get; set; }

        public string WebSiteURL { get; set; }
    }
}