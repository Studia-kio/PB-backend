using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DoctorTrainer.DTO;

public enum UserRole
{
    [Display(Name = "Admin")]
    Admin,
    [Display(Name = "Expert")]
    Expert,
    [Display(Name = "User")]
    User,
}