using ShoppingApp.Data.Entities;
using ShoppingApp.Data.Repositories;
using ShoppingApp.Data.UnitOfWork;

namespace ShoppingApp.Business.Operations.Setting
{
    public class SettingManager : ISettingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<SettingEntity> _settingRepository;

        public SettingManager(IUnitOfWork unitOfWork, IRepository<SettingEntity> settingRepository)
        {
            _unitOfWork = unitOfWork;
            _settingRepository = settingRepository;
        }

        public bool GetMaintenanceState()
        {
            return _settingRepository.GetById(1).MaintenanceMode;
        }

        public async Task ToggleMaintenanceAsync()
        {
            var setting = _settingRepository.GetById(1);
            setting.MaintenanceMode = !setting.MaintenanceMode;

            _settingRepository.Update(setting);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("An error occurred while saving maintenance mode");
            }
        }
    }
}
