using ShopContracts.BindingModels;
using ShopContracts.ViewModels;
using System.Collections.Generic;

namespace ShopContracts.BusinessLogicsContracts
{
    public interface IPickupPointLogic
    {
        List<PickupPointViewModel> Read(PickupPointBindingModel model);
        void CreateOrUpdate(PickupPointBindingModel model);
        void Delete(PickupPointBindingModel model);
    }
}