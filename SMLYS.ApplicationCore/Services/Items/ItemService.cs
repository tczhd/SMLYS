using SMLYS.ApplicationCore.Domain.User;
using SMLYS.ApplicationCore.DTOs.Common;
using SMLYS.ApplicationCore.DTOs.Items;
using SMLYS.ApplicationCore.Entities;
using SMLYS.ApplicationCore.Interfaces.Repository;
using SMLYS.ApplicationCore.Interfaces.Services.Items;
using System;
using System.Collections.Generic;
using System.Text;

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

        public Result AddItem(ItemModel itemModel)
        {
            var userContext = _userHandler.GetUserContext();
            var result = new Result();
            try
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

                result.Success = true;
                result.Message = "Add itemsuccess. ";
            }
            catch (Exception ex)
            {
                result.Message = "Add item failed: " + ex.Message;
            }

            return result;
        }

        public List<ItemModel> SearchItems()
        {
            throw new NotImplementedException();
        }
    }
}
