using GraduationProject.Models.CaseProperties;
using GraduationProject.Models.Shared;
using System.Collections.Generic;

namespace GraduationProject.DTOs.Response
{
    public class CaseProperties
    {
        public IEnumerable<Gender> Genders { get; set; }
        public IEnumerable<SocialStatus> SocialStatus { get; set; }
        public IEnumerable<Relationship> Relationships { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Period> Periods { get; set; }
        public IEnumerable<Priority> Priorities { get; set; }
    }
}
