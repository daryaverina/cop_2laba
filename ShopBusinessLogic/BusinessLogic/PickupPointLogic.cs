using ShopContracts.BindingModels;
using ShopContracts.BusinessLogicsContracts;
using ShopContracts.StorageContracts;
using ShopContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopBusinessLogic.BusinessLogic
{
    public class PickupPointLogic : IPickupPointLogic
    {
        private readonly IPickupPointStorage _pickupPointStorage;
        public PickupPointLogic(IPickupPointStorage pickupPointStorage)
        {
            _pickupPointStorage = pickupPointStorage;
        }
        public void CreateOrUpdate(PickupPointBindingModel model)
        {
            var element = _pickupPointStorage.GetElement(
                new PickupPointBindingModel
                {
                    Name = model.Name
                });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Рункт выдачи уже существует");
            }
            if (model.Id.HasValue)
            {
                _pickupPointStorage.Update(model);
            }
            else
            {
                _pickupPointStorage.Insert(model);
            }
        }

        public void Delete(PickupPointBindingModel model)
        {
            var element = _pickupPointStorage.GetElement(new PickupPointBindingModel { Id = model.Id });
            if (element == null)
            {
                throw new Exception("Пункт выдачи не найден");
            }
            _pickupPointStorage.Delete(model);
        }

        public List<PickupPointViewModel> Read(PickupPointBindingModel model)
        {
            if (model == null)
            {
                return _pickupPointStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<PickupPointViewModel> { _pickupPointStorage.GetElement(model) };
            }
            return _pickupPointStorage.GetFilteredList(model);
        }
    }
}
