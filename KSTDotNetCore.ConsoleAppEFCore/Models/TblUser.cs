using System;
using System.Collections.Generic;

namespace KSTDotNetCore.ConsoleAppEFCore.Models;

public partial class TblUser
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool IsAdmin { get; set; }
}
