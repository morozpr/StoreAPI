using System;
using System.Collections.Generic;

namespace StoreAPI.Models;

public partial class Item
{
    public Guid ItemId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public Guid PriceId { get; set; }

    public bool IsOnStock { get; set; }

    public virtual ICollection<Price> Prices { get; set; } = new List<Price>();
}
