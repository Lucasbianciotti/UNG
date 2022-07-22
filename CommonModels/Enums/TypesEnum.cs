using CommonModels.Global;

namespace CommonModels.Enums
{
    public enum ValidImagesEnum
    {
        png,
        jpg,
        jpeg
    }

    public class EmailTypesEnum
    {
        public static string Welcome { get { return "Welcome"; } }
        public static string ChangePassword { get { return "Change password"; } }
        //public static string Cumpleaños { get { return "Cumpleaños"; } }
        public static string RestorePassword { get { return "Restore password"; } }

    }

    public class SecurityWIFITypesEnum
    {
        public static string WEP { get { return "1"; } }
        public static string WAP { get { return "2"; } }
        public static string WAP2 { get { return "3"; } }

        public static readonly List<string> Types = new()
        {
            "WEP",
            "WAP",
            "WAP2"
        };

        public static List<xEditableItem> ListEditableItem_Types()
        {
            List<xEditableItem> items = new();

            foreach (var i in Types)
            {
                var editable = new xEditableItem
                {
                    Text = i
                };

                switch (i)
                {
                    case "WEP":
                        editable.Value = 1; break;
                    case "WAP":
                        editable.Value = 2; break;
                    case "WAP2":
                        editable.Value = 3; break;
                    default:
                        break;
                }
                items.Add(editable);
            }

            return items;
        }


        public static string GetType(int? type = 0)
        {

            switch (type)
            {
                case 1:
                    return "WEP";
                case 2:
                    return "WAP";
                case 3:
                    return "WAP2";
                default: break;
            }
            return "";
        }
    }

    public class FilterDashboardTimeEnum
    {
        public static string Today { get { return "1"; } }
        public static string LastWeekend { get { return "2"; } }
        public static string LastMonth { get { return "3"; } }

        public static readonly List<string> Types = new()
        {
            "Today",
            "Last Weekend",
            "Last Month"
        };

        public static List<xEditableItem> ListEditableItem_Types()
        {
            List<xEditableItem> items = new();

            foreach (var i in Types)
            {
                var editable = new xEditableItem
                {
                    Text = i
                };

                switch (i)
                {
                    case "Today":
                        editable.Value = 1; break;
                    case "Last Weekend":
                        editable.Value = 2; break;
                    case "Last Month":
                        editable.Value = 3; break;
                    default:
                        break;
                }
                items.Add(editable);
            }

            return items;
        }


        public static string GetType(int? type = 0)
        {

            switch (type)
            {
                case 1:
                    return "Today";
                case 2:
                    return "Last Weekend";
                case 3:
                    return "Last Month";
                default: break;
            }
            return "";
        }
    }

    public class EquipmentsTypesEnum
    {
        public static int Drone { get { return 1; } }
        public static int HandHel { get { return 2; } }
        public static int Notebook { get { return 3; } }
        public static int Tablet { get { return 4; } }

        public static readonly List<string> Types = new()
        {
            "Drone",
            "HandHel",
            "Notebook",
            "Tablet"
        };

        public static List<xEditableItem> ListEditableItem_Types()
        {
            List<xEditableItem> items = new();

            foreach (var i in Types)
            {
                var editable = new xEditableItem
                {
                    Text = i
                };

                switch (i)
                {
                    case "Drone":
                        editable.Value = 1; break;
                    case "Handhel":
                        editable.Value = 2; break;
                    case "Notebook":
                        editable.Value = 3; break;
                    case "Tablet":
                        editable.Value = 4; break;
                    default: break;
                }
                items.Add(editable);
            }

            return items;
        }

        public static string GetType(int type)
        {

            switch (type)
            {
                case 1:
                    return "Drone";
                case 2:
                    return "HandHel";
                case 3:
                    return "Notebook";
                case 4:
                    return "Tablet";
                default: break;
            }
            return "";
        }
    }
}
