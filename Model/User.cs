using System;
using System.Collections.Generic;

namespace sunobra.Model;

public partial class User
{
    public long Id { get; set; }

    public ulong Activo { get; set; }

    public string Apellido { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string? Direccion { get; set; }

    public string Email { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Rol { get; set; } = null!;

    public string? Telefono { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
