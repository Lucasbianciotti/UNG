using Models.EntityFrameworks;
using Models.Enums;
using Models.Request;
using System.Net.Mail;
using System.Security.Claims;

namespace API.LocalClass
{
    public static class EmailClass
    {


        public static async Task<string> SendCode_RestorePassword(string email, string callbackUrl)
        {
            string Asunto = "UNG system - Restore password";
            string Mensaje = "Para restablecer la password, haga clic <a href=\"" + callbackUrl + "\">aquí</a>";

            return await SendEmail(email, Asunto, Mensaje);
        }




        public static async Task<string> SendPasswordForNewUser(ClaimsPrincipal _user, User_Request user, string password)
        {
            string Asunto = string.Empty;
            string Mensaje = string.Empty;

            using var db = new UNG_Context();
            var Email = db.Emails/*.Where(x => x.Type == TypesEnum.Bienvenida)*/.FirstOrDefault();
            if (Email == null)
            {
                Asunto = Reemplazar(Email.Subject, user.Name, user.Surname, user.Email, password);
                Mensaje = Reemplazar(Email.Message, user.Name, user.Surname, user.Email, password);
            }
            else
            {
                throw new Exception("No se encontró la plantilla de email");
            }

            return await SendEmail(user.Email, Asunto, Mensaje);
        }


        #region Metodos
        private static string Reemplazar(string Mensaje, string Name = "", string Surname = "", string Email = "", string Password = "")
        {
            Mensaje = Mensaje.Replace("{app}", "MODDY");
            Mensaje = Mensaje.Replace("{Name}", Name);
            Mensaje = Mensaje.Replace("{Surname}", Surname);
            Mensaje = Mensaje.Replace("{user}", Email);
            Mensaje = Mensaje.Replace("{contrasena}", Password);
            Mensaje = Mensaje.Replace("{URL}", "https://moddy.cloudnetsolutions.com.ar/");

            return Mensaje;
        }

        private static async Task<string> SendEmail(string Email, string Asunto, string Mensaje)
        {
            try
            {
                string EmailDelRemitente = "Administracion@CloudnetSolutions.com.ar";
                string Password = "Cloud_Admin2020";
                string Proveedor = "smtp-mail.outlook.com";
                int Puerto = 587;

                SmtpClient oSmtpClient = new(Proveedor)
                {
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Port = Puerto,
                    Credentials = new System.Net.NetworkCredential(EmailDelRemitente, Password)
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
