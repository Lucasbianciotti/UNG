namespace Class
{
    public class EncodifierClass
    {
        public static string Codify(string cadena)
        {
            try
            {
                if (string.IsNullOrEmpty(cadena)) return string.Empty;

                byte[] encryted = System.Text.Encoding.Unicode.GetBytes(cadena);
                string result = Convert.ToBase64String(encryted);
                return result;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static string Decodify(string cadena)
        {
            try
            {
                if (string.IsNullOrEmpty(cadena)) return string.Empty;

                byte[] decryted = Convert.FromBase64String(cadena);
                System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
                string result = System.Text.Encoding.Unicode.GetString(decryted);
                return result;
            }
            catch (Exception)
            {
                return cadena;
            }
        }


        internal static DateTime DecodifyDateTime(string cadena)
        {
            try
            {
                if (string.IsNullOrEmpty(cadena)) return new DateTime();

                byte[] decryted = Convert.FromBase64String(cadena);
                System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
                string result = System.Text.Encoding.Unicode.GetString(decryted);

                var fecha = new DateTime();
                _ = DateTime.TryParse(result, out fecha);
                return fecha;
            }
            catch (Exception)
            {
                return new DateTime();
            }
        }

    }
}
