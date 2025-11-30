using System;
using System.Collections.Generic;

namespace sunobra.Model;

public partial class VistaClientesActivo
{
    public long Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? Direccion { get; set; }

    public string? PreferenciasContacto { get; set; }

    public DateTime FechaRegistro { get; set; }
}
