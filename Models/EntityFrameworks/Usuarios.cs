﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.EntityFrameworks
{
    public partial class Usuarios
    {
        public Usuarios()
        {
            Logs_MovimientosDelSistema = new HashSet<Logs_MovimientosDelSistema>();
        }

        [Key]
        public long ID { get; set; }
        public long IDempresa { get; set; }
        [Required]
        public string IDestado { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        [Required]
        public string FechaAltaDeUsuario { get; set; }
        public string FechaUltimoIngreso { get; set; }
        public string PinRestaurarContraseña { get; set; }

        [ForeignKey(nameof(IDempresa))]
        [InverseProperty(nameof(Empresas.Usuarios))]
        public virtual Empresas IDempresaNavigation { get; set; }
        [InverseProperty("IDusuarioNavigation")]
        public virtual ICollection<Logs_MovimientosDelSistema> Logs_MovimientosDelSistema { get; set; }
    }
}