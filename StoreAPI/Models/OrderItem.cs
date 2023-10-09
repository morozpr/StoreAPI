using System;
using System.Collections.Generic;

namespace StoreAPI.Models;

public partial class OrderItem
{
    public Guid OrderItemsId { get; set; }

    public Guid OrderId { get; set; }

    public Guid PriceId { get; set; }

    public int? Amount { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Price Price { get; set; } = null!;
}
