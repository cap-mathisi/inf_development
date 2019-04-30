using sspx.core.entities;
using System.Collections.Generic;

namespace sspx.web.Services
{
    public class InMemorySpecialtyRepository : ISpecialtyRepository
    {
        private List<Specialty> _specialties;

        public InMemorySpecialtyRepository()
        {
            _specialties = new List<Specialty>()
            {
                new Specialty
                {
                    SpecialtyKey = 1,
                    SpecialtyTxt = "Breast Pathology",
                    Description = "Breast Pathology"
                },
                new Specialty
                {
                    SpecialtyKey = 2,
                    SpecialtyTxt = "Cytopathology",
                    Description = "Cytopathology"
                },
                new Specialty
                {
                    SpecialtyKey = 3,
                    SpecialtyTxt = "Gastrointestinal Pathology",
                    Description = "Gastrointestinal Pathology"
                },
                new Specialty
                {
                    SpecialtyKey = 4,
                    SpecialtyTxt = "Genitourinary Pathology",
                    Description = "Genitourinary Pathology"
                },
                new Specialty
                {
                    SpecialtyKey = 5,
                    SpecialtyTxt = "Gynecologic Pathology",
                    Description = "Gynecologic Pathology"
                },
                new Specialty
                {
                    SpecialtyKey = 6,
                    SpecialtyTxt = "Neuropathology",
                    Description = "Neuropathology"
                },
                new Specialty
                {
                    SpecialtyKey = 7,
                    SpecialtyTxt = "Pediatric Pathology",
                    Description = "Pediatric Pathology"
                },
                new Specialty
                {
                    SpecialtyKey = 8,
                    SpecialtyTxt = "Perinatal Pathology",
                    Description = "Perinatal Pathology"
                },
                new Specialty
                {
                    SpecialtyKey = 9,
                    SpecialtyTxt = "Renal Pathology",
                    Description = "Renal Pathology"
                },
                new Specialty
                {
                    SpecialtyKey = 10,
                    SpecialtyTxt = "General Surgical Pathology",
                    Description = "General Surgical Pathology"
                },
                new Specialty
                {
                    SpecialtyKey = 11,
                    SpecialtyTxt = "Cytogenetics",
                    Description = "Cytogenetics"
                },
                new Specialty
                {
                    SpecialtyKey = 12,
                    SpecialtyTxt = "Hematopathology",
                    Description = "Hematopathology"
                },
                new Specialty
                {
                    SpecialtyKey = 13,
                    SpecialtyTxt = "Molecular Pathology",
                    Description = "Molecular Pathology"
                },
                new Specialty
                {
                    SpecialtyKey = 14,
                    SpecialtyTxt = "Dermatopathology ",
                    Description = "Dermatopathology"
                }
            };
        }

        public List<Specialty> List()
        {
            return _specialties;
        }
    }
}
