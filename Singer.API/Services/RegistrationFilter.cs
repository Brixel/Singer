using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Singer.Models;

namespace Singer.Services
{
   public static class RegistrationFilter
   {
      public static Expression<Func<Registration, bool>> FilterByText(string keyword) =>
         registration => (!string.IsNullOrEmpty(registration.CareUser.User.FirstName) &&
               registration.CareUser.User.FirstName.Contains(keyword, StringComparison.OrdinalIgnoreCase)) ||
              (!string.IsNullOrEmpty(registration.CareUser.User.LastName) &&
               registration.CareUser.User.LastName.Contains(keyword, StringComparison.OrdinalIgnoreCase));


      public static Expression<Func<Registration, bool>> FilterByUserIds(IReadOnlyList<Guid> careUserIds) =>
         registration => careUserIds.Contains(registration.CareUser.UserId);

      public static Expression<Func<Registration, bool>> FilterByRegistrationStatus(
         RegistrationStatus registrationStatus) =>
         registration => registrationStatus.HasFlag(registration.Status);

      public static Expression<Func<Registration, bool>> FilterByRegistrationType(RegistrationTypes registrationType) =>
         registration => registrationType.HasFlag(registration.EventRegistrationType);

      public static Expression<Func<Registration, bool>> FilterByToDate(DateTime toDate) =>
         registration => registration.StartDateTime <= toDate;

      public static Expression<Func<Registration, bool>> FilterByFromDate(DateTime fromDate) =>
         registration => registration.StartDateTime >= fromDate;
   }
}
