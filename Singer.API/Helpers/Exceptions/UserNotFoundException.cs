using System;

namespace Singer.Helpers.Exceptions
{
   /// <summary>
   /// Exception that represents a 404 http status code. This exception is especially used to indicate that a user could not be found. 
   /// </summary>
   public class UserNotFoundException : NotFoundException
   {
      /// <summary>
      /// Constructs a new instance of the <see cref="UserNotFoundException"/>.
      /// </summary>
      public UserNotFoundException()
      {
      }

      /// <summary>
      /// Constructs a new instance of the <see cref="UserNotFoundException"/>.
      /// </summary>
      /// <param name="message">
      /// The error message. This message is only used for debugging purposes. A message for the client can be found with the <see cref="ClientMessage"/> property.
      /// </param>
      public UserNotFoundException(string message)
         : base(message)
      {
      }

      /// <summary>
      /// Constructs a new instance of the <see cref="UserNotFoundException"/>.
      /// </summary>
      /// <param name="message">
      /// The error message. This message is only used for debugging purposes. A message for the client can be found with the <see cref="ClientMessage"/> property.
      /// </param>
      /// <param name="innerException">The exception that causes this exception.</param>
      public UserNotFoundException(string message, Exception innerException)
         : base(message, innerException)
      {
      }

      /// <summary>
      /// Http status-code that indicates what is wrong (404).
      /// </summary>
      public override string ClientMessage => "There is no user with the given id.";
   }
}
