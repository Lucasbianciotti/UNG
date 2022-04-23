using API.LocalModels;
using API.LocalClass;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models.Enums;
using Models.Request;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Models.EntityFrameworks;

namespace API.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly IEncrypterService _encrypterService;

        public UserService(IOptions<AppSettings> appSettings, IEncrypterService encrypterService)
        {
            _appSettings = appSettings.Value;
            _encrypterService = encrypterService;
        }


        public Response_Login_Request Login(Usuarios usuario)
        {
            try
            {
                var empresa = EmpresasClass.BuscarEmpresa(usuario);
                if (empresa == null) return null;


                var respuesta = new Response_Login_Request
                {
                    ID = usuario.ID,
                    Email = usuario.Email,
                    Nombre = usuario.Nombre,
                    Apellido = usuario.Apellido,
                    //URL_ImagenDePerfil = usuario.URL_ImagenDePerfil,
                    //PermisosDeUsuario = usuario.PermisosDeUsuario,
                    IsAuthSuccessful = true,
                    IDempresa = empresa.ID,
                    Empresa = empresa.Nombre,
                    Token = GenerarToken(usuario.ID, usuario.IDempresa, usuario.Email)
                };

                return respuesta;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool ReestablecerContraseña(Login_ReestablecerContraseña_Request model)
        {
            try
            {
                using var db = new UNG_Context();

                var usuario = db.Usuarios.Where(x => x.Email.ToLower() == EncrypterService.Codify(model.Email.ToLower())
                && x.IDestado == EncrypterService.Codify(EstadosEnum.Activo)).FirstOrDefault();

                if (usuario == null) return false;

                string pin = new Guid().ToString();

                var callbackUrl = "www.moddy.com.ar/actualizarcontraseña?pin=\"" + pin + "\"";

                Task.Run(async () =>
                {
                    await EmailClass.EnviarCodigo_ReestablecerContraseña(usuario.Email, callbackUrl);
                });
                usuario.PinRestaurarContraseña = pin;
                db.Entry(usuario).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool ActualizarContraseña(Login_ActualizarContraseña_Request model)
        {
            try
            {
                //ERROR DE SEGURIDAD; CUANDO SE ESTA POR REESTABLECER LA CONTRASEÑA DEBERIA SOLAMENTE PEDIR CONTASEÑA Y REPETIR CONTASEÑA
                //PIDIENDO EL EMAIL; SE VA A CAMBIAR LA CONTRASEÑA AL EMAIL QUE SE COLOQUE SIN IMPORTAR QUIEN SEA


                //using var db = new MODDY_AdministracionContext();

                //var usuario = db.Usuarios.Where(x => x.Email.ToLower() == model.Email.ToLower()
                //&& x.IDestado == EstadosDeUsuariosEnum.Activo).FirstOrDefault();

                //if (usuario == null) return false;

                //usuario.PasswordHash = EncrypterClass.GetSHA256(model.Password);
                //db.Entry(usuario).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                //db.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }




        private string GenerarToken(long IDuser, long IDempresa, string Email)
        {
            var llave = Encoding.ASCII.GetBytes("5747511683d38b9e4e53070df9b16c1bcc35796fd89c3bab0504d29a89de1e8e");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, IDuser.ToString()),
                        new Claim(ClaimTypes.Email, Email),
                        new Claim(ClaimTypes.PrimarySid, IDempresa.ToString()),
                    }),
                Expires = DateTime.UtcNow.AddDays(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(llave), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

    }

}
