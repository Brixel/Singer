using System.ComponentModel.DataAnnotations;

using Singer.Resources;

namespace Singer.Models;

public enum RegistrationStatus
{
    [Display(ResourceType = typeof(DisplayNames), Name = nameof(DisplayNames.Pending))]
    Pending = 0b001,

    [Display(ResourceType = typeof(DisplayNames), Name = nameof(DisplayNames.Accepted))]
    Accepted = 0b010,

    [Display(ResourceType = typeof(DisplayNames), Name = nameof(DisplayNames.Rejected))]
    Rejected = 0b100
}
