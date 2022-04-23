﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.EntityFrameworks
{
    public partial class Empresas
    {
        public Empresas()
        {
            Logs_MovimientosDelSistema = new HashSet<Logs_MovimientosDelSistema>();
            Usuarios = new HashSet<Usuarios>();
        }

        [Key]
        public long ID { get; set; }
        [Required]
        public string IDestado { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string FechaAltaDeEmpresa { get; set; }

        [InverseProperty("IDempresaNavigation")]
        public virtual ICollection<Logs_MovimientosDelSistema> Logs_MovimientosDelSistema { get; set; }
        [InverseProperty("IDempresaNavigation")]
        public virtual ICollection<Usuarios> Usuarios { get; set; }
    }
}