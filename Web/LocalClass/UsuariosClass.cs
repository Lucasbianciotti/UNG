using Models.Request;

namespace Web.LocalClass
{
    public static class UsersClass
    {
        public static bool UsuarioTienePermiso(User_Request user, string seccion, string accion)
        {
            if (user == null)
                return false;
            return true;
            //return accion switch
            //{
            //    "Ver" => (user.PermisosDeUsuario.Where(x => x.Seccion == seccion && x.Ver).FirstOrDefault() != null),
            //    "Crear" => (user.PermisosDeUsuario.Where(x => x.Seccion == seccion && x.Crear).FirstOrDefault() != null),
            //    "Modificar" => (user.PermisosDeUsuario.Where(x => x.Seccion == seccion && x.Modificar).FirstOrDefault() != null),
            //    "Eliminar" => (user.PermisosDeUsuario.Where(x => x.Seccion == seccion && x.Eliminar).FirstOrDefault() != null),
            //    "Exportar" => (user.PermisosDeUsuario.Where(x => x.Seccion == seccion && x.Exportar).FirstOrDefault() != null),
            //    _ => false,
            //};
        }
    }
}
