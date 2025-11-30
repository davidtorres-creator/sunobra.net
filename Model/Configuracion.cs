using System;
using System.Collections.Generic;

namespace sunobra.Model;

public partial class Configuracion
{
    public int Id { get; set; }

    public string Clave { get; set; } = null!;

    public string? Valor { get; set; }

    public string? Descripcion { get; set; }

    public DateTime FechaActualizacion { get; set; }
}
