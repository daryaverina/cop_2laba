using ShopContracts.BindingModels;
using ShopContracts.StorageContracts;
using ShopContracts.ViewModels;
using ShopDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopDatabaseImplement.Implements
{
    public class ClientStorage : IClientStorage
    {
        public void Delete(ClientBindingModel model)
        {
            var context = new ShopDatabase();
            var client = context.Clients.FirstOrDefault(rec => rec.Id == model.Id);
            if (client != null)
            {
                context.Clients.Remove(client);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Клиент не найден");
            }
        }

        public ClientViewModel GetElement(ClientBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new ShopDatabase())
            {
                var client = context.Clients
                    .ToList()
                    .FirstOrDefault(rec => rec.Id == model.Id);
                return client != null ? CreateModel(client) : null;
            }
        }

        public List<ClientViewModel> GetFilteredList(ClientBindingModel model)
        {
            var context = new ShopDatabase();
            return context.Clients
                .Where(client => client.Order1.Equals("хз"))
                .ToList()
                .Select(CreateModel)
                .ToList();
        }

        public List<ClientViewModel> GetFullList()
        {
            using (var context = new ShopDatabase())
            {
                return context.Clients
                .ToList()
                .Select(CreateModel)
                .ToList();
            }
        }

        public void Insert(ClientBindingModel model)
        {
            var context = new ShopDatabase();
            var transaction = context.Database.BeginTransaction();
            try
            {
                Client client = new Client
                {
                    Name = model.Name,
                    PickupPoint = model.PickupPoint,
                    RegistrationDate = model.RegistrationDate,
                    Order1 = model.Order1,
                    Order2 = model.Order2,
                    Order3 = model.Order3,
                    Order4 = model.Order4
                };
                context.Clients.Add(client);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Update(ClientBindingModel model)
        {
            var context = new ShopDatabase();
            var transaction = context.Database.BeginTransaction();
            try
            {
                var client = context.Clients.FirstOrDefault(rec => rec.Id == model.Id);
                if (client == null)
                {
                    throw new Exception("Заказ не найден");
                }
                client.Name = model.Name;
                client.PickupPoint = model.PickupPoint;
                client.RegistrationDate = model.RegistrationDate;
                client.Order1 = model.Order1;
                client.Order2 = model.Order2;
                client.Order3 = model.Order3;
                client.Order4 = model.Order4;

                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        private ClientViewModel CreateModel(Client client)
        {
            return new ClientViewModel
            {
                Id = client.Id,
                Name = client.Name,
                PickupPoint = client.PickupPoint,
                RegistrationDate = (DateTime)client.RegistrationDate,
                Order1 = client.Order1,
                Order2 = client.Order2,
                Order3 = client.Order3,
                Order4 = client.Order4,
            };
        }
    }
}
