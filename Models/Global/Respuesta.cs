namespace Models.Global
{
    //public class Errors
    //{
    //    public List<string> Name { get; set; }
    //    public List<string> IDcategoria { get; set; }
    //    public List<string> IDunidadDeNegocio { get; set; }
    //}

    public class Respuesta
    {

        public int StatusCode { get; set; }

        public string Mensaje { get; set; }

        public Respuesta(int statusCode)
        {
            this.StatusCode = statusCode;
        }

        public Respuesta(int statusCode, string mensaje)
        {
            this.StatusCode = statusCode;
            this.Mensaje = mensaje;
        }

    }


}
