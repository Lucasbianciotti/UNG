namespace Models.Global
{
    //public class Errors
    //{
    //    public List<string> Name { get; set; }
    //    public List<string> IDcategoria { get; set; }
    //    public List<string> IDunidadDeNegocio { get; set; }
    //}

    public class GlobalResponse
    {

        public int StatusCode { get; set; }

        public string Mensaje { get; set; }

        public GlobalResponse(int statusCode)
        {
            this.StatusCode = statusCode;
        }

        public GlobalResponse(int statusCode, string mensaje)
        {
            this.StatusCode = statusCode;
            this.Mensaje = mensaje;
        }

    }


}
