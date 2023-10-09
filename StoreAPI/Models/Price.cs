using System;
using System.Collections.Generic;

namespace StoreAPI.Models;

public partial class Price
{
    public Guid PriceId { get; set; }

    public Guid ItemId { get; set; }

    public long Value { get; set; }

    public DateOnly DateSet { get; set; }

    public DateOnly DateUnSet { get; set; }

    public virtual Item Item { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
