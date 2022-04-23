using Models.EntityFrameworks;
using System.Security.Claims;

namespace API.LocalClass
{
    public static class EmpresasClass
    {
        public static Empresas BuscarEmpresa(Usuarios user)
        {
            using var db = new UNG_Context();

            try
            {
                return db.Empresas.Find(user.IDempresa);
            }
            catch (Exception)
            {
                throw new Exception("No se pudo buscar la compañía.");
            }
        }

    }
}
