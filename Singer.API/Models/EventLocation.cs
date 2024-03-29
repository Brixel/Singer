using System;

using Singer.Helpers;

namespace Singer.Models;

public class SingerLocation : IIdentifiable
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string PostalCode { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
}
