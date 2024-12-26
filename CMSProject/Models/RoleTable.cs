using System;
using System.Collections.Generic;

namespace CMSProject.Models;

public partial class RoleTable
{
    public int Id { get; set; }

    public string Role { get; set; } = null!;

    public virtual ICollection<UserTable> UserTables { get; set; } = new List<UserTable>();
}
