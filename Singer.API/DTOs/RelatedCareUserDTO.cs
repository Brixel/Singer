using System;

using Singer.Models;

namespace Singer.DTOs;

public class RelatedCareUserDTO
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public AgeGroup AgeGroup { get; set; }
}
