using CommonModels.Global;

namespace CommonModels.Enums
{
    public static class DataStatusEnum
    {
        public static readonly List<string> States = new()
        {
            "Sended",
            "Not Sended",
        };

        public static string Sended { get { return "Sended"; } }
        public static string NotSended { get { return "Not Sended"; } }

        public static List<xEditableItem> ListEditableItem_States()
        {
            var tipos = States;
            List<xEditableItem> items = new();

            foreach (var i in tipos)
            {
                items.Add(new xEditableItem
                {
                    Text = i,
                    ValueAux = i
                });
            }

            return items;
        }
    }   

    public static class UsersStatusEnum
    {
        public static string Active { get { return "Active"; } }
        public static string Deleted { get { return "Deleted"; } }
    }   
    
    public static class ClientsStatusEnum
    {
        public static string Enabled { get { return "Enabled"; } }
        public static string Disabled { get { return "Disabled"; } }
        public static string Deleted { get { return "Deleted"; } }
    }
    
    public static class StationStatusEnum
    {
        public static string Enabled { get { return "Enabled"; } }
        public static string Disabled { get { return "Disabled"; } }
        public static string Deleted { get { return "Deleted"; } }
    }
    
    public static class EquipmentStatusEnum
    {
        public static string Active { get { return "Active"; } }
        public static string Deactivated { get { return "Deactivated"; } }
        public static string Deleted { get { return "Deleted"; } }

    }



}
