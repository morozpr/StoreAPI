using System;
using System.Collections.Generic;

namespace StoreAPI.Models;

public partial class EmployeeType
{
    public Guid EmpTypeId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
