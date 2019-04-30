using sspx.core.entities;

namespace sspx.web.Models
{
    public class Checkbox
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Selected { get; set; }

        public static Checkbox FromUserType(UserType userType)
        {
            return new Checkbox
            {
                Id = userType.UserTypeKey,
                Title = userType.Type,
                Selected = false
            };
        }

        public static Checkbox FromQualification(Qualification qualification)
        {
            return new Checkbox
            {
                Id = qualification.QualificationKey,
                Title = qualification.QualificationTxt,
                Selected = false
            };
        }

        public static Checkbox FromSpecialty(Specialty specialty)
        {
            return new Checkbox
            {
                Id = specialty.SpecialtyKey,
                Title = specialty.SpecialtyTxt,
                Selected = false
            };
        }
    }
}
