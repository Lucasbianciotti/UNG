using APIClient.LocalClass;
using APIClient.LocalModels.SQLite;
using Class;
using Microsoft.IdentityModel.Tokens;
using Models;
using CommonModels.Enums;
using CommonModels.Request;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APIClient.Services
{
    public class UserService : IUserService
    {
        private readonly IEncrypterService _encrypterService;

        public UserService(IEncrypterService encrypterService)
        {
            _encrypterService = encrypterService;
        }


        public Response_Login_Request Login(Users user, string IP)
        {
            try
            {
                var Client = ClientsClass.SearchClient(new ClaimsPrincipal());
                if (Client == null) return null;

                var _userRequest = new User_Request() {
                    ID = user.ID,
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email,
                    IDrole = user.IDrole,
                    IDstatus = user.IDstatus,
                    JSONListOfPermissions = user.JSONListOfPermissions,
                    IDung = user.IDung
                };


                var respuesta = new Response_Login_Request(Client, StationsClass.SearchStation(new ClaimsPrincipal(), IP), _userRequest, true, GenerarToken(user.ID, user.Email, user.IDrole));

                return respuesta;

            }
            catch (Exception)
            {
                return null;
            }
        }

        private static string GenerarToken(long IDuser, string Email, int IDrole, long? IDclient = null)
        {
            var llave = Encoding.ASCII.GetBytes("fda4c1bf82f274bd752c38b601f3a8cd727b8a6857eae747302c0867dea17bc9");

            string idcliente = (IDclient == null) ? string.Empty : IDclient.ToString();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.PrimarySid, IDuser.ToString()),
                        new Claim(ClaimTypes.GroupSid, IDclient.ToString()),
                        new Claim(ClaimTypes.Email, Email),
                        new Claim(ClaimTypes.Role, IDrole.ToString()),
                    }),

                Expires = DateTime.UtcNow.AddDays(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(llave), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }



        public bool RestorePassword(Login_RestorePassword_Request model)
        {
            //try
            //{
            //    using var db = new Local_Context();

            //    var user = db.Users.Where(x => x.Email.ToLower() == EncodifierClass.Codify(model.Email.ToLower())
            //    && x.IDstatus == EncodifierClass.Codify(StatusEnum.Active)).FirstOrDefault();

            //    if (user == null) return false;

            //    string pin = new Guid().ToString();

            //    var callbackUrl = URLsClient._API + "actualizarpassword?pin=\"" + pin + "\"";

            //    //Task.Run(async () =>
            //    //{
            //    //    await EmailClass.SendCode_RestorePassword(user.Email, callbackUrl);
            //    //});
            //    user.PinRestorePassword = pin;
            //    db.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            //    db.SaveChanges();

            //    return true;
            //}
            //catch (Exception)
            //{

            //}
            return false;
        }

        public bool UpdatePassword(Login_UpdatePassword_Request model)
        {
            try
            {
                //ERROR DE SEGURIDAD; CUANDO SE ESTA POR REESTABLECER LA CONTRASEÑA DEBERIA SOLAMENTE PEDIR CONTASEÑA Y REPETIR CONTASEÑA
                //PIDIENDO EL EMAIL; SE VA A CAMBIAR LA CONTRASEÑA AL EMAIL QUE SE COLOQUE SIN IMPORTAR QUIEN SEA


                //using var db = new MODDY_AdministracionContext();

                //var user = db.Users.Where(x => x.Email.ToLower() == model.Email.ToLower()
                //&& x.IDstatus == EstadosDeUsersEnum.Activo).FirstOrDefault();

                //if (user == null) return false;

                //user.PasswordHash = EncrypterClass.GetSHA256(model.Password);
                //db.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                //db.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }





    }

}
