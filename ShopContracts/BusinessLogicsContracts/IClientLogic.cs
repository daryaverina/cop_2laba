using ShopContracts.BindingModels;
using ShopContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopContracts.BusinessLogicsContracts
{
    public interface IClientLogic
    {
        List<ClientViewModel> Read(ClientBindingModel model);
        void CreateOrUpdate(ClientBindingModel model);
        void Delete(ClientBindingModel model);
    }
}