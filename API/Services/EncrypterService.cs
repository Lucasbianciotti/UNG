using Microsoft.AspNetCore.DataProtection;
using System.Security.Cryptography;
using System.Text;

namespace API.Services
{
    public class EncrypterService : IEncrypterService
    {
        private static IDataProtector _protector;

        public EncrypterService(IDataProtectionProvider protectorProvider)
        {
            _protector = protectorProvider.CreateProtector("PersonalProfile.Protector");
        }


        public static string Encrypt(string cadena)
        {
            try
            {
                return (string.IsNullOrEmpty(cadena)) ? string.Empty : _protector.Protect(cadena);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static string Desencrypt(string cadena)
        {
            try
            {
                return (string.IsNullOrEmpty(cadena)) ? string.Empty : _protector.Unprotect(cadena);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }


        public string _Encrypt(string cadena)
        {
            try
            {
                return (string.IsNullOrEmpty(cadena)) ? string.Empty : _protector.Protect(cadena);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public string _Desencrypt(string cadena)
        {
            try
            {
                return (string.IsNullOrEmpty(cadena)) ? string.Empty : _protector.Unprotect(cadena);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }



        public static string GetSHA256(string str)
        {
            SHA256 sha256 = SHA256.Create();
            ASCIIEncoding encoding = new();
            StringBuilder sb = new();
            byte[] stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        public string _GetSHA256(string str)
        {
            SHA256 sha256 = SHA256.Create();
            ASCIIEncoding encoding = new();
            StringBuilder sb = new();
            byte[] stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }



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
                return string.Empty;
            }
        }


        public string _Codify(string cadena)
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

        public string _Decodify(string cadena)
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
                return string.Empty;
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
