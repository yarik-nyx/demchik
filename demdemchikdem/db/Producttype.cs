using System;
using System.Collections.Generic;

namespace demdemchikdem.db;

public partial class Producttype
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public double Defectedpercent { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
