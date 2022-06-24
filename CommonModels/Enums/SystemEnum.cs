namespace Models.Enums
{
    public enum RolesEnum
    {
        Admin,
        ClientAdmin,
        ClientData,
    }

    public enum SystemTypesEnum
    {
        API,
        WEB
    }

    public enum SystemActionsEnum
    {
        Login,
        Read,
        Create,
        Import,
        Export,
        Modify,
        Delete,
        LoadInformation,
        SearchRegister,
        SearchList,
        POST,
        GET
    }
    
    public enum SystemErrorCodesEnum
    {
        Error
    }

    public enum SystemSectionsEnum
    {
        All,
        Dashboard,
        Data,
        Equipments,
        Configuration,
        Users,
    }

}
