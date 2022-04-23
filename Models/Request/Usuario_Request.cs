using System.ComponentModel.DataAnnotations;

namespace Models.Request
{
    public class PermisosDeUsuario_Request
    {
        [Key]
        public long ID { get; set; }

        public long IDusuario { get; set; }

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

    public class Usuario_Request
    {
        public Usuario_Request()
        {
            PermisosDeUsuario = new List<PermisosDeUsuario_Request>();
        }

        [Key]
        public long ID { get; set; }

        //public string IDestado { get; set; }
        public string NombreCompleto { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El apellido es obligatorio.")]
        public string Apellido { get; set; }



        [Required(ErrorMessage = "El email es obligatorio.")]
        [EmailAddress(ErrorMessage = "El email no es válido")]
        public string Email { get; set; }
        public string Contraseña { get; set; }


        public long IDempresa { get; set; }
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
