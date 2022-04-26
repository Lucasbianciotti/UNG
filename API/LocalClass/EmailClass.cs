using Models.EntityFrameworks;
using Models.Enums;
using Models.Request;
using System.Net.Mail;
using System.Security.Claims;

namespace API.LocalClass
{
    public static class EmailClass
    {


        public static async Task<string> EnviarCodigo_ReestablecerContraseña(string email, string callbackUrl)
        {
            string Asunto = "UNG system - Restablecer contraseña";
            string Mensaje = "Para restablecer la contraseña, haga clic <a href=\"" + callbackUrl + "\">aquí</a>";

            return await EnviarEmail(email, Asunto, Mensaje);
        }




        public static async Task<string> EnviarContraseñaParaNuevoUsuario(ClaimsPrincipal user, User_Request usuario, string contraseña)
        {
            string Asunto = string.Empty;
            string Mensaje = string.Empty;

            using var db = new UNG_Context();
            var Email = db.Emails.Where(x => x.Type == TiposDeEmailEnum.Bienvenida).FirstOrDefault();
            if (Email == null)
            {
                Asunto = Reemplazar(Email.Subject, usuario.Name, usuario.Surname, usuario.Email, contraseña);
                Mensaje = Reemplazar(Email.Message, usuario.Name, usuario.Surname, usuario.Email, contraseña);
            }
            else
            {
                throw new Exception("No se encontró la plantilla de email");
            }

            return await EnviarEmail(usuario.Email, Asunto, Mensaje);
        }


        #region Metodos
        private static string Reemplazar(string Mensaje, string Name = "", string Surname = "", string Email = "", string Contraseña = "")
        {
            Mensaje = Mensaje.Replace("{app}", "MODDY");
            Mensaje = Mensaje.Replace("{Name}", Name);
            Mensaje = Mensaje.Replace("{Surname}", Surname);
            Mensaje = Mensaje.Replace("{usuario}", Email);
            Mensaje = Mensaje.Replace("{contrasena}", Contraseña);
            Mensaje = Mensaje.Replace("{URL}", "https://moddy.cloudnetsolutions.com.ar/");

            return Mensaje;
        }

        private static async Task<string> EnviarEmail(string Email, string Asunto, string Mensaje)
        {
            try
            {
                string EmailDelRemitente = "Administracion@CloudnetSolutions.com.ar";
                string Contraseña = "Cloud_Admin2020";
                string Proveedor = "smtp-mail.outlook.com";
                int Puerto = 587;

                SmtpClient oSmtpClient = new(Proveedor)
                {
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Port = Puerto,
                    Credentials = new System.Net.NetworkCredential(EmailDelRemitente, Contraseña)
                };

                MailMessage oMailMessage = new(EmailDelRemitente, Email, Asunto, Mensaje)
                {
                    IsBodyHtml = true
                };

                await oSmtpClient.SendMailAsync(oMailMessage);

                oSmtpClient.Dispose();

                return string.Empty;
            }
            catch (Exception e)
            {
                return e.Message.ToString();
            }
        }

        #endregion
    }

}
