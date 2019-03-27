using SMLYS.ApplicationCore.DTOs.Common;
using SMLYS.ApplicationCore.DTOs.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMLYS.ApplicationCore.Interfaces.Services.Items
{
    public interface IItemService
    {
        Result AddItem(ItemModel itemModel);

        List<ItemModel> SearchItems();
    }
}
