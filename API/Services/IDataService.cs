using Models.Request;

namespace API.Services
{
    public interface IDataService
    {
        Task SendDataReceivedMessage();
        //Task DataUpdated(Data_Request survey);
    }
}
