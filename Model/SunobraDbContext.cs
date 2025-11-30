using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace sunobra.Model;

public partial class SunobraDbContext : DbContext
{
    public SunobraDbContext()
    {
    }

    public SunobraDbContext(DbContextOptions<SunobraDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Configuracion> Configuracions { get; set; }

    public virtual DbSet<Contratacione> Contrataciones { get; set; }

    public virtual DbSet<Proyecto> Proyectos { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<VistaClientesActivo> VistaClientesActivos { get; set; }

    public virtual DbSet<VistaObrerosActivo> VistaObrerosActivos { get; set; }

    public virtual DbSet<VistaProyectosCompletum> VistaProyectosCompleta { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("server=localhost;user=root;database=sunobra_db", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.32-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_unicode_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Configuracion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("configuracion");

            entity.HasIndex(e => e.Clave, "clave").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Clave)
                .HasMaxLength(100)
                .HasColumnName("clave");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaActualizacion)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("fecha_actualizacion");
            entity.Property(e => e.Valor)
                .HasColumnType("text")
                .HasColumnName("valor");
        });

        modelBuilder.Entity<Contratacione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("contrataciones");

            entity.HasIndex(e => e.ClienteId, "idx_cliente");

            entity.HasIndex(e => e.Estado, "idx_estado");

            entity.HasIndex(e => e.FechaContratacion, "idx_fecha_contratacion");

            entity.HasIndex(e => e.ObreroId, "idx_obrero");

            entity.HasIndex(e => e.ProyectoId, "idx_proyecto");

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20)")
                .HasColumnName("id");
            entity.Property(e => e.CalificacionCliente)
                .HasColumnType("int(11)")
                .HasColumnName("calificacion_cliente");
            entity.Property(e => e.CalificacionObrero)
                .HasColumnType("int(11)")
                .HasColumnName("calificacion_obrero");
            entity.Property(e => e.ClienteId)
                .HasColumnType("bigint(20)")
                .HasColumnName("cliente_id");
            entity.Property(e => e.ComentariosCliente)
                .HasColumnType("text")
                .HasColumnName("comentarios_cliente");
            entity.Property(e => e.ComentariosObrero)
                .HasColumnType("text")
                .HasColumnName("comentarios_obrero");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .HasDefaultValueSql("'activa'")
                .HasColumnName("estado");
            entity.Property(e => e.FechaContratacion)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("fecha_contratacion");
            entity.Property(e => e.FechaFin).HasColumnName("fecha_fin");
            entity.Property(e => e.FechaInicio).HasColumnName("fecha_inicio");
            entity.Property(e => e.ObreroId)
                .HasColumnType("bigint(20)")
                .HasColumnName("obrero_id");
            entity.Property(e => e.ProyectoId)
                .HasColumnType("bigint(20)")
                .HasColumnName("proyecto_id");
            entity.Property(e => e.TarifaTotal)
                .HasPrecision(12, 2)
                .HasColumnName("tarifa_total");

            entity.HasOne(d => d.Cliente).WithMany(p => p.ContratacioneClientes)
                .HasForeignKey(d => d.ClienteId)
                .HasConstraintName("contrataciones_ibfk_1");

            entity.HasOne(d => d.Obrero).WithMany(p => p.ContratacioneObreros)
                .HasForeignKey(d => d.ObreroId)
                .HasConstraintName("contrataciones_ibfk_2");

            entity.HasOne(d => d.Proyecto).WithMany(p => p.Contrataciones)
                .HasForeignKey(d => d.ProyectoId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("contrataciones_ibfk_3");
        });

        modelBuilder.Entity<Proyecto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("proyectos");

            entity.HasIndex(e => e.Categoria, "idx_categoria");

            entity.HasIndex(e => e.ClienteId, "idx_cliente");

            entity.HasIndex(e => e.Estado, "idx_estado");

            entity.HasIndex(e => e.ObreroId, "idx_obrero");

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20)")
                .HasColumnName("id");
            entity.Property(e => e.Categoria)
                .HasMaxLength(100)
                .HasColumnName("categoria");
            entity.Property(e => e.ClienteId)
                .HasColumnType("bigint(20)")
                .HasColumnName("cliente_id");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .HasDefaultValueSql("'pendiente'")
                .HasColumnName("estado");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaFin).HasColumnName("fecha_fin");
            entity.Property(e => e.FechaInicio).HasColumnName("fecha_inicio");
            entity.Property(e => e.ImagenUrl)
                .HasMaxLength(500)
                .HasColumnName("imagen_url");
            entity.Property(e => e.ObreroId)
                .HasColumnType("bigint(20)")
                .HasColumnName("obrero_id");
            entity.Property(e => e.Presupuesto)
                .HasPrecision(12, 2)
                .HasColumnName("presupuesto");
            entity.Property(e => e.Titulo)
                .HasMaxLength(255)
                .HasColumnName("titulo");
            entity.Property(e => e.Ubicacion)
                .HasMaxLength(255)
                .HasColumnName("ubicacion");

            entity.HasOne(d => d.Cliente).WithMany(p => p.ProyectoClientes)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("proyectos_ibfk_1");

            entity.HasOne(d => d.Obrero).WithMany(p => p.ProyectoObreros)
                .HasForeignKey(d => d.ObreroId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("proyectos_ibfk_2");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "UK6dotkott2kjsp8vw4d0m25fb7").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20)")
                .HasColumnName("id");
            entity.Property(e => e.Activo)
                .HasColumnType("bit(1)")
                .HasColumnName("activo");
            entity.Property(e => e.Apellido)
                .HasMaxLength(100)
                .HasColumnName("apellido");
            entity.Property(e => e.CreatedAt)
                .HasMaxLength(6)
                .HasColumnName("created_at");
            entity.Property(e => e.Direccion)
                .HasMaxLength(500)
                .HasColumnName("direccion");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Rol)
                .HasMaxLength(50)
                .HasColumnName("rol");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .HasColumnName("telefono");
            entity.Property(e => e.UpdatedAt)
                .HasMaxLength(6)
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usuarios");

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.HasIndex(e => e.Activo, "idx_activo");

            entity.HasIndex(e => e.UserType, "idx_user_type");

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20)")
                .HasColumnName("id");
            entity.Property(e => e.Activo)
                .HasDefaultValueSql("'1'")
                .HasColumnName("activo");
            entity.Property(e => e.Apellido)
                .HasMaxLength(255)
                .HasColumnName("apellido");
            entity.Property(e => e.Certificaciones)
                .HasMaxLength(255)
                .HasColumnName("certificaciones");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .HasColumnName("descripcion");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .HasColumnName("direccion");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Especialidades)
                .HasMaxLength(255)
                .HasColumnName("especialidades");
            entity.Property(e => e.Experiencia)
                .HasDefaultValueSql("'0'")
                .HasColumnType("int(11)")
                .HasColumnName("experiencia");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.PreferenciasContacto)
                .HasMaxLength(255)
                .HasColumnName("preferencias_contacto");
            entity.Property(e => e.TarifaHora).HasColumnName("tarifa_hora");
            entity.Property(e => e.Telefono)
                .HasMaxLength(255)
                .HasColumnName("telefono");
            entity.Property(e => e.UserType).HasColumnName("user_type");
        });

        modelBuilder.Entity<VistaClientesActivo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vista_clientes_activos");

            entity.Property(e => e.Apellido)
                .HasMaxLength(255)
                .HasColumnName("apellido");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .HasColumnName("direccion");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.Id)
                .HasColumnType("bigint(20)")
                .HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
            entity.Property(e => e.PreferenciasContacto)
                .HasMaxLength(255)
                .HasColumnName("preferencias_contacto");
            entity.Property(e => e.Telefono)
                .HasMaxLength(255)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<VistaObrerosActivo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vista_obreros_activos");

            entity.Property(e => e.Apellido)
                .HasMaxLength(255)
                .HasColumnName("apellido");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .HasColumnName("descripcion");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .HasColumnName("direccion");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Especialidades)
                .HasMaxLength(255)
                .HasColumnName("especialidades");
            entity.Property(e => e.Experiencia)
                .HasDefaultValueSql("'0'")
                .HasColumnType("int(11)")
                .HasColumnName("experiencia");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.Id)
                .HasColumnType("bigint(20)")
                .HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
            entity.Property(e => e.TarifaHora).HasColumnName("tarifa_hora");
            entity.Property(e => e.Telefono)
                .HasMaxLength(255)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<VistaProyectosCompletum>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vista_proyectos_completa");

            entity.Property(e => e.Categoria)
                .HasMaxLength(100)
                .HasColumnName("categoria");
            entity.Property(e => e.ClienteEmail)
                .HasMaxLength(255)
                .HasColumnName("cliente_email");
            entity.Property(e => e.ClienteNombre)
                .HasMaxLength(511)
                .HasColumnName("cliente_nombre");
            entity.Property(e => e.ClienteTelefono)
                .HasMaxLength(255)
                .HasColumnName("cliente_telefono");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .HasDefaultValueSql("'pendiente'")
                .HasColumnName("estado");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaFin).HasColumnName("fecha_fin");
            entity.Property(e => e.FechaInicio).HasColumnName("fecha_inicio");
            entity.Property(e => e.Id)
                .HasColumnType("bigint(20)")
                .HasColumnName("id");
            entity.Property(e => e.ImagenUrl)
                .HasMaxLength(500)
                .HasColumnName("imagen_url");
            entity.Property(e => e.Presupuesto)
                .HasPrecision(12, 2)
                .HasColumnName("presupuesto");
            entity.Property(e => e.Titulo)
                .HasMaxLength(255)
                .HasColumnName("titulo");
            entity.Property(e => e.Ubicacion)
                .HasMaxLength(255)
                .HasColumnName("ubicacion");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
