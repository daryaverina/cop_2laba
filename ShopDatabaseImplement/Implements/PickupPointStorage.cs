using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopContracts.BindingModels;
using ShopContracts.StorageContracts;
using ShopContracts.ViewModels;
using ShopDatabaseImplement.Models;

namespace ShopDatabaseImplement.Implements
{
    public class PickupPointStorage : IPickupPointStorage
    {
        public void Delete(PickupPointBindingModel model)
        {
            var context = new ShopDatabase();
            var points = context.PickupPoints.FirstOrDefault(rec => rec.Id == model.Id);
            if (points != null)
            {
                context.PickupPoints.Remove(points);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Пункт выдачи не найден");
            }
        }
        public PickupPointViewModel GetElement(PickupPointBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var context = new ShopDatabase();
            var point = context.PickupPoints
                .ToList()
                .FirstOrDefault(rec => rec.Id == model.Id);
            return point != null ? CreateModel(point) : null;
        }
        public List<PickupPointViewModel> GetFilteredList(PickupPointBindingModel model)
        {
            throw new NotImplementedException();
        }
        public List<PickupPointViewModel> GetFullList()
        {
            var context = new ShopDatabase();
            return context.PickupPoints
                .ToList()
                .Select(CreateModel)
                .ToList();
        }
        public void Insert(PickupPointBindingModel model)
        {
            var context = new ShopDatabase();
            var transaction = context.Database.BeginTransaction();
            try
            {
                PickupPoint point = new PickupPoint
                {
                    Name = model.Name
                };
                context.PickupPoints.Add(point);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Update(PickupPointBindingModel model)
        {
            var context = new ShopDatabase();
            var transaction = context.Database.BeginTransaction();
            try
            {
                var point = context.PickupPoints.FirstOrDefault(rec => rec.Id == model.Id);
                if (point == null)
                {
                    throw new Exception("Пункт выдачи не найден");
                }
                point.Name = model.Name;
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        private PickupPointViewModel CreateModel(PickupPoint point)
        {
            return new PickupPointViewModel
            {
                Id = point.Id,
                Name = point.Name
            };
        }
    }
}
