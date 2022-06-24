using Microsoft.AspNetCore.DataProtection;
using System.Security.Cryptography;
using System.Text;

namespace APIAdmin.Services
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



    }
}
