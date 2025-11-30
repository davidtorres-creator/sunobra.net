using System;
using System.Collections.Generic;

namespace sunobra.Model;

public partial class Contratacione
{
    public long Id { get; set; }

    public long ClienteId { get; set; }

    public long ObreroId { get; set; }

    public long? ProyectoId { get; set; }

    public DateTime FechaContratacion { get; set; }

    public DateOnly? FechaInicio { get; set; }

    public DateOnly? FechaFin { get; set; }

    public decimal? TarifaTotal { get; set; }

    public string? Estado { get; set; }

    public string? Descripcion { get; set; }

    public int? CalificacionCliente { get; set; }

    public int? CalificacionObrero { get; set; }

    public string? ComentariosCliente { get; set; }

    public string? ComentariosObrero { get; set; }

    public virtual Usuario? Cliente { get; set; } = null!;

    public virtual Usuario? Obrero { get; set; } = null!;

    public virtual Proyecto? Proyecto { get; set; }
}
