using System;
using System.ComponentModel;

namespace Singer.Helpers
{
   public interface IIdentifiable
   {
      [DisplayName("Id")]
      Guid Id { get; set; }
   }
}
