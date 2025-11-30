using System;
using System.Collections.Generic;

namespace sunobra.Model;

public partial class VistaProyectosCompletum
{
    public long Id { get; set; }

    public string Titulo { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string? Categoria { get; set; }

    public string? ImagenUrl { get; set; }

    public string? Ubicacion { get; set; }

    public string? Estado { get; set; }

    public decimal? Presupuesto { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateOnly? FechaInicio { get; set; }

    public DateOnly? FechaFin { get; set; }

    public string? ClienteNombre { get; set; }

    public string? ClienteEmail { get; set; }

    public string? ClienteTelefono { get; set; }
}
