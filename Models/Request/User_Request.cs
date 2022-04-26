﻿using System.ComponentModel.DataAnnotations;

namespace Models.Request
{
    public class PermisosDeUsuario_Request
    {
        [Key]
        public long ID { get; set; }

        public long IDuser { get; set; }

        public long IDunidadDeNegocio { get; set; }

        public long IDseccion { get; set; }

        public string Seccion { get; set; }

        public bool Ver { get; set; }
        public bool Crear { get; set; }
        public bool Modificar { get; set; }
        public bool Eliminar { get; set; }
        public bool Exportar { get; set; }
        public bool Todos { get; set; }
    }

    public class User_Request
    {
        public User_Request()
        {
            PermisosDeUsuario = new List<PermisosDeUsuario_Request>();
        }

        [Key]
        public long ID { get; set; }

        //public string IDstatus { get; set; }
        public string CompleteName { get; set; }

        [Required(ErrorMessage = "The name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The surname is required.")]
        public string Surname { get; set; }



        [Required(ErrorMessage = "The email is required.")]
        [EmailAddress(ErrorMessage = "The email is not valid.")]
        public string Email { get; set; }
        public string Contraseña { get; set; }


        public long IDcompany { get; set; }
        public string Empresa { get; set; }


        public string URL_ImagenDePerfil { get; set; }

        public List<PermisosDeUsuario_Request> PermisosDeUsuario { get; set; }

        public string Aux { get; set; }
    }

    public class EliminarUsuario_Request
    {
        [Required]
        public long ID { get; set; }
    }
    
    public class CambiarContraseñaUsuario_Request
    {

        [Required(ErrorMessage = "Ingrese la contraseña anterior")]
        [DataType(DataType.Password)]
        public string ContraseñaAnterior { get; set; }


        [Required(ErrorMessage = "Ingrese la nueva contraseña")]
        [StringLength(255, ErrorMessage = "La contraseña debe tener al menos 5 caracteres", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string NuevaContraseña { get; set; }


        [Required(ErrorMessage = "Confirme la nueva contraseña")]
        [StringLength(255, ErrorMessage = "La contraseña debe tener al menos 5 caracteres", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Compare("NuevaContraseña", ErrorMessage = "La nueva contraseña no coincide")]
        public string RepetirNuevaContraseña { get; set; }

    }
}