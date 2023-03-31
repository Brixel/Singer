using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Singer.Models.Users;

public class AdminUser : IUser
{
    public Guid Id { get; set; }

    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }

    public virtual User User { get; set; }
}
