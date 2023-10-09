using System;
using System.Collections.Generic;

namespace StoreAPI.Models;

public partial class Employee
{
    public Guid EmployeeId { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string? Patronymic { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public Guid? EmpType { get; set; }

    public virtual EmployeeType? EmpTypeNavigation { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
