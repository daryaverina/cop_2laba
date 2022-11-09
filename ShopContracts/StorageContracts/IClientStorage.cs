using ShopContracts.BindingModels;
using ShopContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopContracts.StorageContracts
{
    public interface IClientStorage
    {
        List<ClientViewModel> GetFullList();
        List<ClientViewModel> GetFilteredList(ClientBindingModel model);
        ClientViewModel GetElement(ClientBindingModel model);
        void Insert(ClientBindingModel model);
        void Update(ClientBindingModel model);
        void Delete(ClientBindingModel model);
    }
}