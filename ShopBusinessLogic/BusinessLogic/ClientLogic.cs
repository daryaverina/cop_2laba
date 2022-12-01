using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopDatabaseImplement.Implements;
using ShopContracts;
using ShopContracts.BusinessLogicsContracts;
using ShopContracts.StorageContracts;
using ShopContracts.BindingModels;
using ShopContracts.ViewModels;

namespace ShopBusinessLogic.BusinessLogic
{
    public class ClientLogic : IClientLogic
    {
        private readonly IClientStorage _clientStorage;
        public ClientLogic(IClientStorage CLientStorage)
        {
            _clientStorage = CLientStorage;
        }
        public void CreateOrUpdate(ClientBindingModel model)
        {
            var element = _clientStorage.GetElement(
                new ClientBindingModel
                {
                    Name = model.Name,
                    PickupPoint = model.PickupPoint,
                    RegistrationDate = DateTime.Now,
                    Order1 = model.Order1,
                    Order2 = model.Order2,
                    Order3 = model.Order3,
                    Order4 = model.Order4,
                });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Клиент с таким именем уже существует");
            }
            if (model.Id.HasValue)
            {
                _clientStorage.Update(model);
            }
            else
            {
                _clientStorage.Insert(model);
            }
        }

        public void Delete(ClientBindingModel model)
        {
            var element = _clientStorage.GetElement(new ClientBindingModel { Id = model.Id });
            if (element == null)
            {
                throw new Exception("Клиент не найден");
            }
            _clientStorage.Delete(model);
        }

        public List<ClientViewModel> Read(ClientBindingModel model)
        {
            if (model == null)
            {
                return _clientStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<ClientViewModel> { _clientStorage.GetElement(model) };
            }
            return _clientStorage.GetFilteredList(model);
        }
    }
}