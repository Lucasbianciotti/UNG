﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models.EntityFrameworks
{
    public partial class Drones
    {
        public Drones()
        {
            Datos = new HashSet<Datos>();
        }

        [Key]
        public long ID { get; set; }
        [Required]
        [StringLength(50)]
        public string IDestado { get; set; }
        [Required]
        [StringLength(50)]
        public string MAC { get; set; }

        [InverseProperty("IDdronNavigation")]
        public virtual ICollection<Datos> Datos { get; set; }
    }
}