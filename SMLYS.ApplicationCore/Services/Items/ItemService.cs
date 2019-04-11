using SMLYS.ApplicationCore.Domain.User;
using SMLYS.ApplicationCore.DTOs.Common;
using SMLYS.ApplicationCore.DTOs.Items;
using SMLYS.ApplicationCore.Entities;
using SMLYS.ApplicationCore.Interfaces.Repository;
using SMLYS.ApplicationCore.Interfaces.Services.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMLYS.ApplicationCore.Services.Items
{
    public class ItemService : IItemService
    {
        private readonly IRepository<Item> _itemRepository;
        private readonly UserHandler _userHandler;

        public ItemService(IRepository<Item> itemRepository, UserHandler userHandler)
        {
            _itemRepository = itemRepository;
            _userHandler = userHandler;
        }

        public Result SaveItem(ItemModel itemModel)
        {
            var userContext = _userHandler.GetUserContext();
            var result = new Result();

            if (userContext == null)
            {
                result.Message = "Session expired. ";
                return result;
            }

            try
            {
                if (itemModel.ItemId > 0)
                {
                    var item = _itemRepository.GetById(itemModel.ItemId);
                    if (item != null)
                    {
                        item.Cost = itemModel.Cost;
                        item.Name = itemModel.ItemName;
                        item.Description = itemModel.Description;

                        _itemRepository.Update(item);
                        result.Message = "Update item success. ";
                        result.Success = true;
                    }
                    else {
                        result.Message = "Update item failed ";
                    }
                }
                else
                {
                    var item = new Item()
                    {
                        Active = true,
                        ClinicId = userContext.ClinicId,
                        Cost = itemModel.Cost,
                        Description = itemModel.Description,
                        Name = itemModel.ItemName,
                        UpdatedBy = userContext.SiteUserId,
                        UpdatedDateUtc = DateTime.UtcNow
                    };

                    _itemRepository.Add(item);
                    result.Message = "Add item success. ";
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Add item failed: " + ex.Message;
            }

            return result;
        }

        public ItemModel SearchItem(int id)
        {
            var item =  _itemRepository.GetById(id);

            if (item != null)
            {
                return new ItemModel { Cost = item.Cost, ItemId = item.Id, ItemName = item.Name, Description = item.Description };
            }

            return null;
        }

        public List<ItemModel> SearchItems()
        {
            var data = _itemRepository.ListAll().ToList();
            var result = data.Select(p => new ItemModel
            {
                Cost =  p.Cost,
                Description = p.Description,
                ItemId = p.Id,
                ItemName = p.Name

            }).ToList();

            return result;
        }
    }
}

