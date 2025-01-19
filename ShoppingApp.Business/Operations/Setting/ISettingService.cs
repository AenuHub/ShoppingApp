namespace ShoppingApp.Business.Operations.Setting
{
    public interface ISettingService
    {
        Task ToggleMaintenanceAsync();
        bool GetMaintenanceState();
    }
}
