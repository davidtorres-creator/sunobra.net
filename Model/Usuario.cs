using System;
using System.Collections.Generic;

namespace sunobra.Model;

public partial class Usuario
{
    public long Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string UserType { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? Direccion { get; set; }

    public string? PreferenciasContacto { get; set; }

    public string? Especialidades { get; set; }

    public int? Experiencia { get; set; }

    public double? TarifaHora { get; set; }

    public string? Certificaciones { get; set; }

    public string? Descripcion { get; set; }

    public DateTime FechaRegistro { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<Contratacione> ContratacioneClientes { get; set; } = new List<Contratacione>();

    public virtual ICollection<Contratacione> ContratacioneObreros { get; set; } = new List<Contratacione>();

    public virtual ICollection<Proyecto> ProyectoClientes { get; set; } = new List<Proyecto>();

    public virtual ICollection<Proyecto> ProyectoObreros { get; set; } = new List<Proyecto>();
}
