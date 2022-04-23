namespace API.Services
{
    public interface IEncrypterService
    {
        string _GetSHA256(string cadena);

        string _Encrypt(string cadena);
        string _Desencrypt(string cadena);

        string _Codify(string cadena);
        string _Decodify(string cadena);
    }
}
