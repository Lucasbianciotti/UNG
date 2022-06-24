namespace APIClient.Services
{
    public interface IEncrypterService
    {
        string _GetSHA256(string cadena);

        string _Encrypt(string cadena);
        string _Desencrypt(string cadena);

    }
}
