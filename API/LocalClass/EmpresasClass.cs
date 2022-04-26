using Models.EntityFrameworks;

namespace API.LocalClass
{
    public static class CompanysClass
    {
        public static Companies BuscarCompany(Users user)
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
