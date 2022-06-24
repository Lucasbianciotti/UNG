using Syncfusion.Blazor.Notifications;

namespace Client.Services
{
    public interface IToast_Services
    {
        public SfToast Toast { get; set; }


        public void ShowWarning(string Mensaje);


        public void ShowSuccess(string Mensaje, string Titulo);
        public void ShowSuccess(string Mensaje);


        public void ShowError(string Mensaje);


        public void ShowInfo(string Mensaje);
        public void ShowInfo(string Mensaje, string Titulo);

    }
}
