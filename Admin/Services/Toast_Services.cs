using Syncfusion.Blazor.Notifications;

namespace Admin.Services
{
    public class Toast_Services : IToast_Services
    {
        public SfToast Toast { get; set; } = new SfToast();


        public void ShowWarning(string Mensaje)
        {
            if (Toast != null)
            {
                var model = new ToastModel { Title = "Attention", Content = Mensaje, CssClass = "e-toast-warning", Icon = "e-warning toast-icons" };
                Toast.Show(model);
            }
        }




        public void ShowSuccess(string Mensaje, string Titulo)
        {
            if (Toast != null)
            {
                var model = new ToastModel { Title = Titulo, Content = Mensaje, CssClass = "e-toast-success", Icon = "e-success toast-icons" };
                Toast.Show(model);
            }
        }

        public void ShowSuccess(string Mensaje)
        {
            if (Toast != null)
            {
                var model = new ToastModel { Title = "Success", Content = Mensaje, CssClass = "e-toast-success", Icon = "e-success toast-icons" };
                Toast.Show(model);
            }
        }



        public void ShowError(string Mensaje)
        {
            if (Toast != null)
            {
                var model = new ToastModel { ShowCloseButton = true, Timeout = 60000, Title = "Attention", Content = Mensaje, CssClass = "e-toast-danger", Icon = "e-error toast-icons" };
                Toast.Show(model);
            }
        }




        public void ShowInfo(string Mensaje, string Titulo)
        {
            if (Toast != null)
            {
                var model = new ToastModel { Title = Titulo, Content = Mensaje, CssClass = "e-toast-info", Icon = "e-info toast-icons" };
                Toast.Show(model);
            }
        }

        public void ShowInfo(string Mensaje)
        {
            if (Toast != null)
            {
                var model = new ToastModel { Title = "Information", Content = Mensaje, CssClass = "e-toast-info", Icon = "e-info toast-icons" };
                Toast.Show(model);
            }
        }
    }
}
