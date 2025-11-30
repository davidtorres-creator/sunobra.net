using System;
using System.Collections.Generic;

namespace sunobra.Model;

public partial class Proyecto
{
    public long Id { get; set; }

    public string Titulo { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string? Categoria { get; set; }

    public string? ImagenUrl { get; set; }

    public string? Ubicacion { get; set; }

    public long? ClienteId { get; set; }

    public long? ObreroId { get; set; }

    public string? Estado { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateOnly? FechaInicio { get; set; }

    public DateOnly? FechaFin { get; set; }

    public decimal? Presupuesto { get; set; }

    public virtual Usuario? Cliente { get; set; }

    public virtual ICollection<Contratacione> Contrataciones { get; set; } = new List<Contratacione>();

    public virtual Usuario? Obrero { get; set; }
}
