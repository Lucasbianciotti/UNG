using Models.EntityFrameworks;

namespace API.LocalClass
{
    public static class EmpresasClass
    {
        public static Companies BuscarEmpresa(Users user)
        {
            using var db = new UNG_Context();

            try
            {
                return db.Companies.Find(user.IDcompany);
            }
            catch (Exception)
            {
                throw new Exception("The company was not found.");
            }
        }

    }
}
