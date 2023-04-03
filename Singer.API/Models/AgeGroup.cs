using System;
using System.ComponentModel.DataAnnotations;

using Singer.Resources;

namespace Singer.Models;

[Flags]
public enum AgeGroup
{
    [Display(ResourceType = typeof(DisplayNames), Name = nameof(DisplayNames.Toddler))]
    Toddler = 1,

    [Display(ResourceType = typeof(DisplayNames), Name = nameof(DisplayNames.Kindergartener))]
    Kindergartener = 2,

    [Display(ResourceType = typeof(DisplayNames), Name = nameof(DisplayNames.Child))]
    Child = 4,

    [Display(ResourceType = typeof(DisplayNames), Name = nameof(DisplayNames.Youngster))]
    Youngster = 8,

    [Display(ResourceType = typeof(DisplayNames), Name = nameof(DisplayNames.Adult))]
    Adult = 16
}
