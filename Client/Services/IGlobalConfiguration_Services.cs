using Models.Enums;
using Syncfusion.Blazor.Popups;

namespace Client.Services
{
    public interface IGlobalConfiguration_Services
    {
        public DialogEffect AnimationEffect { get; }
        public int AnimationTime { get; }


        event Action OnChange;
        public void Notify_ElementsDataChanged();


        //public void EnviarLogDeError(New_Error_Request model);
        public void NuevoLog(string comentario, SystemActionsEnum accion, Exception exception, SystemErrorCodesEnum codigo);


    }
}
