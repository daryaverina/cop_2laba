using ShopContracts.BindingModels;
using ShopContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopContracts.StorageContracts
{
    public interface IPickupPointStorage
    {
        List<PickupPointViewModel> GetFullList();
        List<PickupPointViewModel> GetFilteredList(PickupPointBindingModel model);
        PickupPointViewModel GetElement(PickupPointBindingModel model);
        void Insert(PickupPointBindingModel model);
        void Update(PickupPointBindingModel model);
        void Delete(PickupPointBindingModel model);
    }
}
