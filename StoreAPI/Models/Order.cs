using System;
using System.Collections.Generic;

namespace StoreAPI.Models;

public partial class Order
{
    public Guid OrderId { get; set; }

    public Guid EmployeeId { get; set; }

    public Guid ClientId { get; set; }

    public Guid OrderItemsId { get; set; }

    public DateOnly DateTime { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;

    public virtual OrderItem OrderItems { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItemsNavigation { get; set; } = new List<OrderItem>();
}
